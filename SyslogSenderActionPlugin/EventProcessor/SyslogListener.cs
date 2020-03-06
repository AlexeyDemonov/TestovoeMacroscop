using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SyslogSenderActionPlugin.EventProcessor
{
    class SyslogListener
    {
        UdpClient _client;

        public event Action<string> MessageArrived;

        public void StartListening(int port)
        {
            if(_client != null)
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
            try
            {
                var task = await _client.ReceiveAsync();
                byte[] data = task.Buffer;
                var message = Encoding.ASCII.GetString(data);
                MessageArrived?.Invoke(message);
                return true;
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
        }

        void KeepListening(Task<bool> task)
        {
            if(task != null && !task.IsFaulted && task.Result == true)
                AcceptSyslogAsync().ContinueWith(KeepListening);
        }
    }
}
