using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarus.RealTimeFrameProviding;

namespace SyslogSenderActionPlugin.EventProcessor
{
    class SyslogEvent : RawEvent
    {
        public SyslogEvent(string syslogMessage)
        {
            base.EventTime = DateTime.Now;
            base.Comment = syslogMessage;
        }
    }
}
