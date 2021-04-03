using System;

namespace Assets.Models
{
    [Serializable]
    public class PlayerSettings
    {
        public bool backgroundMusic;
        public bool paddleSfx; // ball hit paddle sound
        public bool brickSfx; // ball hit brick sound
    }
}
