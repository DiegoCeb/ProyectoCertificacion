﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App.API.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServiceAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var Installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract).Select(Activator.CreateInstance)
            .Cast<IInstaller>().ToList();

            Installers.ForEach(installer => installer.InstallServices(services, configuration));
        }

    }
}
