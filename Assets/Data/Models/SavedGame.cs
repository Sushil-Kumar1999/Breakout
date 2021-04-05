using System;

namespace Assets.Data.Models
{
    [Serializable]
    public class SavedGame
    {
        public string label;
        public DateTime saveTime;
        public int score;
        public int bricksLeft;
    }
}

