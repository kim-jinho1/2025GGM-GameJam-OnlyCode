using Member.Core;

namespace Member.KJH.Code.Events
{
    public struct OnUIEvent : IEvent
    {
        public bool IsUI { get; }
        
        public OnUIEvent(bool isUI)
        {
            IsUI = isUI;
        }
    }
}