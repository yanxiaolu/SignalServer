using Microsoft.AspNetCore.ResponseCompression;
using SignalServer.APIService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

// 添加 SignalR 服务
builder.Services.AddSignalR();

// 添加响应压缩服务
builder.Services.AddResponseCompression(opt =>
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" }));

var app = builder.Build();

// 使用 CORS 策略
app.UseCors("CorsPolicy");

// 使用响应压缩中间件
app.UseResponseCompression();

// 映射 SignalR Hub
app.MapHub<ChatHub>("/chathub");

// 映射一个 GET 请求到根 URL，返回 "Hello World!"
app.MapGet("/", () => "Hello World!");

app.Run();