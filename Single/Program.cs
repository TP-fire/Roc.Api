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
        // ���NLog��־֧��
        builder.AddRocNLog();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services
            .AddEndpointsApiExplorer()
            // ע��sqlsugar
            .AddRocSqlsugar(configuration)
            // ע��swagger����
            .AddRocSwagger(configuration)
            // ·��Сд
            .AddRouting(options => options.LowercaseUrls = true)
            // ��ӿ���
            .AddRocCors()
            // ע�����
            .AddRocIocServices();

        // ��ӽӿ�
        // ������(Scoped):��Ӧ�ó�������ʱ����������Ӧ�ó���ر�ʱ���١��������͵ķ���ʵ���ᱻ������������ֻ�ᱻ��ǰ����ʹ�á����������ʱ���÷���ʵ���ᱻ���١�
        // ����(Singleton):��Ӧ�ó�������ʱ��������������Ӧ�ó��������ڼ䱣�ֲ��䡣�������͵ķ���ʵ���ᱻ�����������ҿ��Ա����������
        // ˲ʱ(Transient):��Ӧ�ó�������ʱ����������Ӧ�ó���ر�ʱ���١��������͵ķ���ʵ�����ᱻ��������Ҳ���ᱻ�����������á�
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
