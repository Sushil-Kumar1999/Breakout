using System;

namespace Assets.Data.Models
{
    [Serializable]
    public class PlayerProfile : IData
    {
        public string playerName;
        public int currentHighScore;
        public int previousHighScore;
    }
}

