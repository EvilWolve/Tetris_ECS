using Unity.Entities;

namespace game.tags
{
    // If any sort of transform operation is completed succesfully, add the Changed tag so that the transform can then be applied in the final step.
    public struct Changed : IComponentData
    {

    }
}