using System;

namespace Assets.Data.Models
{
    [Serializable]
    public class Settings : IData
    {
        public bool backgroundMusic;
        public bool paddleSfx; // ball hit paddle sound
        public bool brickSfx; // ball hit brick sound
    }
}
