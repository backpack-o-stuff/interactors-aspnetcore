using System;
using System.Collections.Generic;
using BOS.ClientLayer.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Tests.Infrastructure.TestBases
{
    public class IntegratedFor<T> : ArrangeActAssertOn
        where T : class
    {
        protected readonly List<Action<IServiceCollection>> DependencyFakes = new List<Action<IServiceCollection>>();

        protected T SUT;

        public IntegratedFor()
        {
            SharedBeforeAll();
            SUT = Resolve<T>();
        }

        protected TResolveFor Resolve<TResolveFor>()
        {
            return DependencyRegistrations.Resolve<TResolveFor>(DependencyFakes);
        }

        private void SharedBeforeAll()
        {
            DependencyFakes.Add((services) => 
            {
            });
        }
    }
}