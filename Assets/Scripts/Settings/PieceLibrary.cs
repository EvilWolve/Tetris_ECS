using UnityEngine;

namespace game.settings
{
    [CreateAssetMenu (fileName = "New Piece Library", menuName = "Pieces/Piece Library")]
    public class PieceLibrary : ScriptableObject
    {
        public PieceConfiguration[] pieces;
    }
}