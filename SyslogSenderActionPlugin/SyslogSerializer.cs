using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogSenderActionPlugin
{
    class SyslogSerializer
    {
        public byte[] Serialize(SyslogMessage message)
        {
            var priorityValue = ((int)message.Facility * 8) + message.Severity;

            string timestamp = null;
            var date = message.DateTimeOffset;
            var day = date.Day.ToString("D2");
            timestamp = $"{date.ToString("MMM")} {day} {date.ToString("HH:mm:ss")}";

            var builder = new StringBuilder();
            builder.Append("<").Append(priorityValue).Append(">");
            builder.Append(timestamp).Append(" ");
            builder.Append(message.MachineName).Append(" ");
            builder.Append(message.AppName).Append(" ");
            builder.Append(message.Comment ?? "");

            return Encoding.ASCII.GetBytes(builder.ToString());
        }
    }
}
