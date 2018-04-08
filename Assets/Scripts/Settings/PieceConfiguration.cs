using Unity.Mathematics;
using UnityEngine;

namespace game.settings
{
    [CreateAssetMenu (fileName = "New Piece Config", menuName = "Pieces/Piece Config")]
    public class PieceConfiguration : ScriptableObject
    {
        public int2[] layout;

        public Color color;
    }
}