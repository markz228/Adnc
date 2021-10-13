﻿using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hangfire
{
    /// <summary>
    /// Hangfire 模块注册
    /// </summary>
    public class HangfireModule : Autofac.Module
    {
        private readonly IEnumerable<Assembly> _assembliesToScan;

        public HangfireModule(IEnumerable<Assembly> assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        public HangfireModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembliesToScan.ToArray())
                .Where(t => t.IsAssignableTo<IRecurringSchedulingJobs>() && t.IsAssignableTo<IRecurringJob>() && !t.IsAbstract)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}