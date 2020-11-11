using System;

namespace HappyPointSounds.Events
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public bool IsConnected;
        public bool WasConnected;
    }
}
