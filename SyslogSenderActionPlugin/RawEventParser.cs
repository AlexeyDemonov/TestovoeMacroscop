using Alarus.RealTimeFrameProviding;
using System;
using System.Text;

namespace SyslogSenderActionPlugin
{
    class RawEventParser
    {
        public SyslogMessage ParseToSyslogMessage(RawEvent rawEvent)
        {
            var time = rawEvent.EventTime;
            var eventName = GetEventName(rawEvent);
            var eventInfos = GetEventInfos(rawEvent);
            var comment = FormComment(eventName, eventInfos);

            return new SyslogMessage(time, Facility.UserLevelMessages, Severity.Informational,
            Environment.MachineName, "Macroscop", comment);
        }

        public string ParseToString(RawEvent rawEvent)
        {
            var message = ParseToSyslogMessage(rawEvent);

            var priorityValue = ((int)message.Facility * 8) + message.Severity;

            var date = message.DateTimeOffset;
            var day = date.Day.ToString("D2");
            string timestamp = $"{date.ToString("MMM")} {day} {date.ToString("HH:mm:ss")}";

            var builder = new StringBuilder();
            builder.Append("<").Append(priorityValue).Append(">");
            builder.Append(timestamp).Append(" ");
            builder.Append(message.MachineName).Append(" ");
            builder.Append(message.AppName).Append(" ");
            builder.Append(message.Comment ?? "");

            return builder.ToString();
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