using Unity.Entities;
using Unity.Mathematics;

namespace game.transform
{
    public struct GridPositionComponent : IComponentData
    {
        public int2 position;
    }
}