using System.Text;

namespace SyslogSenderActionPlugin
{
    class SyslogSerializer
    {
        public byte[] Serialize(string syslogMessage)
        {
            return Encoding.ASCII.GetBytes(syslogMessage);
        }
    }
}