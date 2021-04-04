using System;

namespace Assets.Models
{
    [Serializable]
    public class Settings
    {
        public bool backgroundMusic;
        public bool paddleSfx; // ball hit paddle sound
        public bool brickSfx; // ball hit brick sound
    }
}
