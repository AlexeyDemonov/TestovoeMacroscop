using Alarus;

namespace SyslogSenderActionPlugin.EventProcessor
{
    [PluginGUIName("Syslog listener")]
    class SyslogEventProcessor : Alarus.RealTimeFrameProviding.EventProcessor
    {
        int _port;
        SyslogListener _listener;

        public override void Initialize(PluginSettings settings)
        {
            _port = 514;
            _listener = new SyslogListener();
            _listener.MessageArrived += SendEvent;
            _listener.StartListening(_port);
        }

        public override void Dispose()
        {
            if (_listener != null)
            {
                _listener.MessageArrived -= SendEvent;
                _listener.StopListening();
            }
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