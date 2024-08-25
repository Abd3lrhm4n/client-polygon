using Background;
using Background.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;

        // Load general settings from appsettings.json
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

        // Load environment variables
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Retrieve the connection string from appsettings.json
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        // Configure Hangfire to use the connection string
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.AddHangfireServer();

        // Register background jobs or services
        services.AddScoped<PolygonJob>();
        services.AddHttpClient<IPolygonService, PolygonService>();

        //services.AddHostedService<Worker>();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        // Configure Kestrel and Hangfire Dashboard
        webBuilder.UseKestrel(options =>
        {
            options.ListenAnyIP(5000);
        })
        .Configure(app =>
        {
            app.UseHangfireDashboard("/hangfire");
        });
    });

var host = builder.Build();
host.Run();
