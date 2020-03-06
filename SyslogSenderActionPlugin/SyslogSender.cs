using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SyslogSenderActionPlugin
{
    class SyslogSender
    {
        UdpClient _udpClient;
        SyslogSerializer _serializer;
        bool _disposed;

        public SyslogSender(string host, int port)
        {
            _udpClient = new UdpClient(host, port);
            _serializer = new SyslogSerializer();
        }

        public void Send(SyslogMessage message)
        {
            byte[] data = _serializer.Serialize(message);
            _udpClient.Send(data, data.Length);
        }

        public void Dispose()
        {
            _udpClient?.Close();
            _serializer = null;
            _disposed = true;
        }

        ~SyslogSender()
        {
            if(!_disposed)
                Dispose();
        }
    }
}
