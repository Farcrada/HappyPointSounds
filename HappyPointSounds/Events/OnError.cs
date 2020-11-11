using System;

namespace HappyPointSounds.Events
{
    public class OnErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
