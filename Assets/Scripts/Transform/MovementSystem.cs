using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace game.transform
{
    using tags;

    public class MovementSystem : JobComponentSystem
    {
        public class MovementUpdateBarrier : BarrierSystem
        {
        }

        public struct TickerData
        {
            [ReadOnly] public ComponentDataArray<Ticker> ticker;
        }

        public struct ActiveBlockData
        {
            public int length;

            [ReadOnly] public EntityArray entity;

            [ReadOnly] public ComponentDataArray<MoveComponent> move;
            [ReadOnly] public ComponentDataArray<ActivePiece> active;
            [ReadOnly] public ComponentDataArray<GridPositionComponent> position;
        }

        public struct InactiveBlockData
        {
            public int length;
            
            [ReadOnly] public ComponentDataArray<GridPositionComponent> position;

            [ReadOnly] public SubtractiveComponent<MoveComponent> move;
        }

        private MovementUpdateBarrier movementUpdateBarrier;

        [Inject] private TickerData tickerData;

        [Inject] private ActiveBlockData activeBlocks;
        [Inject] private InactiveBlockData inactiveBlocks;

        [ComputeJobOptimization]
        struct MovementJob : IJob
        {
            public int bottomPos;

            [ReadOnly] public EntityArray entity;
            public ComponentDataArray<GridPositionComponent> active;
            [ReadOnly] public ComponentDataArray<GridPositionComponent> inactive;
            public EntityCommandBuffer commands;

            public void Execute()
            {
                for (int i = 0; i < this.active.Length; ++i)
                {
                    int2 currentPos = this.active[i].position;
                    int2 nextPos = new int2(currentPos.x, currentPos.y - 1);

                    bool doesCollide = nextPos.y < this.bottomPos;

                    if (!doesCollide)
                    {
                        for (int j = 0; j < this.inactive.Length; ++j)
                        {
                            if (this.inactive[j].position.Equals (nextPos))
                            {
                                doesCollide = true;
                                break;
                            }
                        }
                    }

                    if (doesCollide)
                    {
                        this.commands.RemoveComponent<MoveComponent> (this.entity[i]);
                    }
                    else
                    {
                        var gridPos = this.active[i];
                        gridPos.position = nextPos;
                        this.active[i] = gridPos;
                    }
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            if (this.tickerData.ticker.Length < 1)
                return inputDeps;

            Ticker ticker = this.tickerData.ticker[0];
            if (ticker.timeToNextTick > 0f)
                return inputDeps;

            return new MovementJob
            {
                bottomPos = 0,
                entity = this.activeBlocks.entity,
                active = this.activeBlocks.position,
                inactive = this.inactiveBlocks.position,
                commands = this.movementUpdateBarrier.CreateCommandBuffer(),
            }.Schedule (inputDeps);
        }
    }
}