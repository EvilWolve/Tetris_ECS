using UnityEngine;
using Unity.Mathematics;

namespace game.settings
{
    public class Settings : ScriptableObject
    {
        public GameObject blockPrototype;

        public int2 playfieldDimensions;

        public float activeDropRate;
        public float fillDropSpeed;

        private static Settings instance;

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<Settings> ("Settings");
                }

                return instance;
            }
        }
    }
}
