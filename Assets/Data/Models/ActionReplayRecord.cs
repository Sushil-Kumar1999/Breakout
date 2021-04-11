using System;

namespace Assets.Data.Models
{
    [Serializable]
    public class ActionReplayRecord
    {
        public SerializableVector2 position;
        public SerializableQuaternion rotation;

        public ActionReplayRecord(SerializableVector2 pos, SerializableQuaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
}
