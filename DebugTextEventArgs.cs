using System;

namespace GardenaApi
{
    public class DebugTextEventArgs : EventArgs
    {
        public DebugTextEventArgs(string debugText)
        {
            DebugText = debugText;
        }

        public string DebugText { get; private set; }
    }
}