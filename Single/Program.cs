using Roc.Framework;
using Roc.Middleware;
using Single.Entities;

namespace Single;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        RocSupportConfig.Init(configuration);
        // 添加NLog日志支持
        builder.AddRocNLog();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services
            .AddEndpointsApiExplorer()
            // 注入sqlsugar
            .AddRocSqlsugar(configuration)
            // 注入swagger依赖
            .AddRocSwagger(configuration)
            // 路由小写
            .AddRouting(options => options.LowercaseUrls = true)
            // 添加跨域
            .AddRocCors()
            // 注入服务
            .AddRocIocServices();

        // 添加接口
        // 作用域(Scoped):在应用程序启动时创建，并在应用程序关闭时销毁。这种类型的服务实例会被容器管理，但是只会被当前请求使用。当请求结束时，该服务实例会被销毁。
        // 单例(Singleton):在应用程序启动时创建，并在整个应用程序运行期间保持不变。这种类型的服务实例会被容器管理，并且可以被多个请求共享。
        // 瞬时(Transient):在应用程序启动时创建，并在应用程序关闭时销毁。这种类型的服务实例不会被容器管理，也不会被其他服务引用。
        // builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseRocSwagger(configuration);
        }

        app.UseAuthorization();

        app.UseRocCors();

        app.MapControllers();

        app.Run();
    }
}
