using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogSenderActionPlugin
{
    class SyslogMessage
    {
        public readonly DateTimeOffset DateTimeOffset;
        public readonly Facility Facility;
        public readonly Severity Severity;
        public readonly string MachineName;
        public readonly string AppName;
        public readonly string Comment;

        public SyslogMessage(DateTimeOffset time, Facility facility, Severity severity, string machineName, string appName, string comment)
        {
            this.DateTimeOffset = time;
            this.Facility = facility;
            this.Severity = severity;
            this.MachineName = machineName;
            this.AppName = appName;
            this.Comment = comment;
        }
    }
}
