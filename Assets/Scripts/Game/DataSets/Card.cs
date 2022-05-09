using UnityEngine;

namespace Game.DataSets
{
    [System.Serializable]
    public struct Card
    {
        public bool held;
        public Suits suit;
        public Values value;
        public Sprite graphic;
    }
}