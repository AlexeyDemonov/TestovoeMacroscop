using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Alarus;
using Alarus.Action_;
using Alarus.RealTimeFrameProviding;

namespace SyslogSenderActionPlugin
{
    [Serializable]
    [AlarusSerializable]
    [ActionGUIName("Send syslog")]
    [Guid("2e1e88e2-f400-4819-8e7b-5b339c245164")]
    class SyslogAction : ExternalAction
    {
        public override bool IsConfigurated => true;

        public override string Description => "Sends syslog message to localhost";

        public override void Run(RawEvent rawEvent)
        {
            if(rawEvent == null)
                throw new ArgumentNullException(nameof(rawEvent));

            var time = rawEvent.EventTime;
            var eventName = GetEventName(rawEvent);
            var eventInfos = GetEventInfos(rawEvent);
            var comment = FormComment(eventName, eventInfos);

            var message = new SyslogMessage(time, Facility.UserLevelMessages, Severity.Informational,
                Environment.MachineName, "Macroscop", comment);

            var sender = new SyslogSender("localhost", 514);
            sender.Send(message);
        }

        string GetEventName(RawEvent rawEvent)
        {
            return $"[{rawEvent.GetType().Name}]";
        }

        string[] GetEventInfos(RawEvent rawEvent)
        {
            var type = rawEvent.GetType();
            var fields = type.GetFields();
            var length = fields.Length;
            var infos = new string[length];

            for (int i = 0; i < length; i++)
            {
                var field = fields[i];
                infos[i] = $"*{field.Name}: {field.GetValue(rawEvent)}*";
            }

            return infos;
        }

        string FormComment(string eventName, string[] eventInfos)
        {
            var builder = new StringBuilder(eventName);

            for (int i = 0; i < eventInfos.Length; i++)
            {
                builder.Append(eventInfos[i]);
            }

            return builder.ToString();
        }

    }
}
