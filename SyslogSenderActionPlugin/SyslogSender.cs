using System;
using System.Net.Sockets;

namespace SyslogSenderActionPlugin
{
    class SyslogSender : IDisposable
    {
        UdpClient _udpClient;
        bool _disposed;

        public SyslogSender(string host, int port)
        {
            _udpClient = new UdpClient(host, port);
        }

        public void Send(byte[] data)
        {
            _udpClient.Send(data, data.Length);
        }

        public void Dispose()
        {
            _udpClient?.Close();
            _disposed = true;
        }

        ~SyslogSender()
        {
            if (!_disposed)
                Dispose();
        }
    }
}