﻿using System.Threading.Tasks;
using Microsoft.Azure.Devices.Edge.Hub.Service;
using TypeEdge.Enums;
using TypeEdge.Modules;
using TypeEdge.Modules.Enums;
using Microsoft.Extensions.Configuration;
using Agent = Microsoft.Azure.Devices.Edge.Agent.Core;

namespace TypeEdge.Host.Hub
{
    public class EdgeHub : EdgeModule
    {
        internal override string Name => Agent.Constants.EdgeHubModuleIdentityName;
        private IConfigurationRoot HubServiceConfiguration { get; set; }

        public override CreationResult Configure(IConfigurationRoot configuration)
        {
            HubServiceConfiguration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            return CreationResult.Ok;
        }

        public override async Task<ExecutionResult> RunAsync()
        {
            var res = await Program.MainAsync(HubServiceConfiguration);
            if (res == 0)
                return ExecutionResult.Ok;
            return ExecutionResult.Error;
        }
    }
}