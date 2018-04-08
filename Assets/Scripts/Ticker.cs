using Unity.Entities;

namespace game
{
    // TODO: Update ticker based on delta-time and current tick-rate.
    // Tick rate is flexible because it scales with game difficulty and can also be increased via player input.
    public struct Ticker : IComponentData
    {
        public float timeToNextTick;
    }
}