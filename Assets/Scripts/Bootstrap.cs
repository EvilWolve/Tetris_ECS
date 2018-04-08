using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms2D;

namespace game.bootstrap
{
    using transform;
    using settings;
    using tags;

    public class Bootstrap
    {
        public static EntityArchetype blockArchetype;

        public static MeshInstanceRenderer blockLook;

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void CreateArchetypes()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager> ();
            
            blockArchetype = entityManager.CreateArchetype (
                typeof (Position2D),
                typeof (Heading2D),
                typeof(GridPositionComponent),
                typeof(ActivePiece));
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeWithScene()
        {
            blockLook = Settings.Instance.blockPrototype.GetComponent<MeshInstanceRendererComponent>().Value;
        }
    }
}
