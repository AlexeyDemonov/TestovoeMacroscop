using Alarus.Action_;
using Alarus.RealTimeFrameProviding;
using System;
using System.Runtime.InteropServices;

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
            if (rawEvent == null)
                throw new ArgumentNullException(nameof(rawEvent));

            var message = new RawEventParser().ParseToString(rawEvent);
            var data = new SyslogSerializer().Serialize(message);

            using (var sender = new SyslogSender("localhost", 514))
            {
                sender.Send(data);
            }
        }
    }
}