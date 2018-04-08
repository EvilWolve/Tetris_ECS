using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms2D;
using UnityEngine;

namespace game.utils.rendering
{
    [ExecuteInEditMode]
    public class SpriteInstanceRendererSystem : ComponentSystem
    {
        List<SpriteInstanceRenderer> cachedUniqueRendererTypes = new List<SpriteInstanceRenderer> (10);
        ComponentGroup instanceRendererGroup;

        protected override void OnCreateManager(int capacity)
        {
            this.instanceRendererGroup = GetComponentGroup (ComponentType.Create<SpriteInstanceRenderer> (), ComponentType.Create<Position2D> ());
        }

        protected override void OnUpdate()
        {
            Camera.onPostRender = null;

            this.EntityManager.GetAllUniqueSharedComponentDatas (this.cachedUniqueRendererTypes);

            Camera.onPostRender += (Camera camera) =>
            {
                GL.PushMatrix ();
                GL.LoadPixelMatrix (0, Screen.width, 0, Screen.height);
            };

            for (int i = 0; i < this.cachedUniqueRendererTypes.Count; i++)
            {
                var renderer = this.cachedUniqueRendererTypes[i];
                this.instanceRendererGroup.SetFilter (renderer);
                var positions = this.instanceRendererGroup.GetComponentDataArray<Position2D> ();
                for (int j = 0; j != positions.Length; j++)
                {
                    float2 position = positions[j].Value;
                    Camera.onPostRender += (Camera camera) =>
                    {
                        Graphics.DrawTexture (
                        new Rect (position.x,
                                position.y + renderer.sprite.height,
                                renderer.sprite.width,
                                -renderer.sprite.height),
                        renderer.sprite,
                        renderer.material);
                    };
                }
            }
            Camera.onPostRender += (Camera camera) =>
            {
                GL.PopMatrix ();
            };

            this.cachedUniqueRendererTypes.Clear ();
        }
    }
}