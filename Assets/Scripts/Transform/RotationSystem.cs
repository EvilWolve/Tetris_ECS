using Unity.Entities;
using Unity.Collections;

namespace game.transform
{
    public class RotationSystem : ComponentSystem
    {
        public struct ActiveBlockData
        {
            public int Length;

            [ReadOnly] public ComponentDataArray<MoveComponent> move;
            [ReadOnly] public ComponentDataArray<RotateComponent> rotate;
        }

        [Inject] private ActiveBlockData activeBlocks;

        protected override void OnUpdate()
        {

        }
    }
}