using Alarus.Events;
using Alarus.RealTimeFrameProviding;
using System;
using System.Runtime.InteropServices;

namespace SyslogSenderActionPlugin.EventProcessor
{
    [Serializable]
    [AlarusSerializable]
    [Guid("e0b53877-b23e-415a-8c6e-3118fe040b06")]
    [EventLocalizedName("Syslog event")]
    [EventTreePath("EventTree.User", "EventTree.Client")]
    [EventSaveable(EventSaveMode.SpecialAndUnifiedLog, "syslogMessage", EventGenerationFrequency.Middle)]
    [EventGeneratesAlarmByDefault]
    class SyslogEvent : RawEvent
    {
        public SyslogEvent(string syslogMessage)
        {
            base.EventTime = DateTime.Now;
            base.Comment = syslogMessage;
        }

        public override EventTextDescription GetDescription(IToStringConverter converter)
        {
            return new EventTextDescription(base.Comment);
        }
    }
}