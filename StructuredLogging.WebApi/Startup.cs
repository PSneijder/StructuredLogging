﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(StructuredLogging.WebApi.Startup))]

namespace StructuredLogging.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
