using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms2D;

namespace game.transform
{
    using tags;

    public class UpdateTransformSystem : JobComponentSystem
    {
        struct MovementData
        {
            public int Length;
            public ComponentDataArray<Position2D> Position2D;
            [ReadOnly] public ComponentDataArray<GridPositionComponent> GridPosition;
            [ReadOnly] public ComponentDataArray<Changed> Changed;
        }

        [Inject] private MovementData movementData;

        [ComputeJobOptimization]
        struct ApplyMovementJob : IJobParallelFor
        {
            public ComponentDataArray<Position2D> Position2D;
            [ReadOnly] public ComponentDataArray<GridPositionComponent> GridPosition;

            public void Execute(int index)
            {
                var pos2D = this.Position2D[index];
                pos2D.Value = this.GridPosition[index].position;
                this.Position2D[index] = pos2D;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var applyMovement = new ApplyMovementJob
            {
                Position2D = this.movementData.Position2D,
                GridPosition = this.movementData.GridPosition,
            }.Schedule (this.movementData.Length, 1, inputDeps);

            return applyMovement;
        }
    }
}