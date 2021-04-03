using System;

namespace Assets.Models
{
    [Serializable]
    public class PlayerProfile
    {
        public string playerName;
        public int highScore;
        public PlayerSettings playerSettings;
    }
}

