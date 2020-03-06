using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarus;

namespace SyslogSenderActionPlugin.EventProcessor
{
    [PluginGUIName("Syslog listener")]
    class SyslogEventProcessor : Alarus.RealTimeFrameProviding.EventProcessor
    {
        public override void Dispose()
        {
            //Do nothing
        }

        public override void Initialize(PluginSettings settings)
        {
            //Do nothing
        }

        public override PluginSettings SetSettings(PluginSettings settings)
        {
            //Do nothing
            return settings;
        }

        void SendEvent(string syslogMessage)
        {
            base.GenerateEvent?.Invoke(new SyslogEvent(syslogMessage), generateAlarm:false);
        }
    }
}
