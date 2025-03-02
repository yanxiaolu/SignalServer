using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<SignalServer_APIService>("apiserver");

builder.Build().Run();