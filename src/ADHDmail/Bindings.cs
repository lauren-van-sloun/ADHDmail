using ADHDmail.API;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmailApi>().To<GmailApi>();
        }
    }
}
