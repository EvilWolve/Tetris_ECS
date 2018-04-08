using Unity.Entities;

namespace game.tags
{
    // Use this on the blocks belonging to the currently dropping piece to differentiate it from other dropping blocks, for example after a line is completed.
    // If we find any block with ActivePiece that no longer has a Move and Rotate component after their updates, we know that the piece has finished dropping.
    // We need to then remove the Changed tags to avoid applying changes, and roll back the changes made by movement (not sure about rotation, depends on how we handle collision there).
    public struct ActivePiece : IComponentData
    {
        
    }
}