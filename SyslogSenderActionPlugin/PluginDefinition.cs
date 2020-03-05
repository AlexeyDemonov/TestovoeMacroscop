using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarus;

namespace SyslogSenderActionPlugin
{
    class PluginDefinition : IPlugin
    {
        public Guid Id => new Guid("2e1e88e2-f400-4819-8e7b-5b339c245164");

        public string Name => "Syslog sender";

        public string Manufacturer => "Alexey Demonov";

        public void Initialize(IPluginHost host)
        {
            host.RegisterExternalAction(typeof(SyslogAction));
        }
    }
}
