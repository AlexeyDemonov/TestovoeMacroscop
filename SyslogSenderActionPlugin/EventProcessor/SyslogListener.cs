using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SyslogSenderActionPlugin.EventProcessor
{
    class SyslogListener
    {
        UdpClient _client;

        public event Action<string> MessageArrived;

        public void StartListening(int port)
        {
            if (_client != null)
            {
                StopListening();
            }

            _client = new UdpClient(port);
            AcceptSyslogAsync().ContinueWith(KeepListening);
        }

        public void StopListening()
        {
            _client.Close();
            _client.Dispose();
        }

        async Task<bool> AcceptSyslogAsync()
        {
            string message = null;

            try
            {
                var task = await _client.ReceiveAsync();
                byte[] data = task.Buffer;
                message = Encoding.ASCII.GetString(data);
            }
            catch (ObjectDisposedException)//Client closed
            {
                return false;
            }
            catch (Exception)
            {
                //Rethrow
                throw;
            }

            MessageArrived?.Invoke(message);
            return true;
        }

        void KeepListening(Task<bool> task)
        {
            if (task != null && !task.IsFaulted && task.Result == true)
                AcceptSyslogAsync().ContinueWith(KeepListening);
        }
    }
}