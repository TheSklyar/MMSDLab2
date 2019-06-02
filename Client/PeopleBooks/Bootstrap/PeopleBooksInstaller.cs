using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Helpers;
using Helpers.Common;
using Helpers.Interfaces;

namespace PeopleBooks.Bootstrap
{
    public class PeopleBooksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Guard.CheckNotNull(container, "container");
            container.Register<PeopleBooksWindow>();
            container.RegisterInterfacesFromAssembly<IService>(Classes.FromAssemblyInThisApplication());
        }
    }
}
