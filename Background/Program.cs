using Application.Services.Client;
using Application.Services.EmailQueue;
using Application.Services.Polygon;
using Background;
using Background.Services;
using Domain.IRepositories;
using Domain.IRepositories.Common;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Context;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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

        services.AddHttpClient<IPolygonJobService, PolygonJobService>();

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<PolygonJob>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IClientRepository), typeof(ClientRepository));
        services.AddScoped<IClientService, ClientService>();
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        services.AddLogging(builder => builder.AddConsole());
        services.AddScoped<IEmailQueueService, EmailQueueService>();
        services.AddScoped<IEmailQueueRepository, EmailQueueRepository>();
        services.AddScoped<IPolygonService, PolygonService>();
        services.AddScoped<IPolygonRepository, PolygonRepository>();

        //services.AddHostedService<Worker>();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        // Configure Kestrel and Hangfire Dashboard
        webBuilder.UseKestrel(options =>
        {
            options.ListenAnyIP(5000); // HTTP port
            options.ListenAnyIP(5001, listenOptions =>
            {
                listenOptions.UseHttps(); // HTTPS port
            });
        })
        .Configure(app =>
        {
            app.UseHangfireDashboard("/hangfire");
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            // Schedule the recurring job
            //RecurringJob.AddOrUpdate<PolygonJob>(job => job.FetchStockDataAsync("AAPL"), Cron.Hourly(6));
            // Define the recurring job options if needed
            var serviceProvider = app.ApplicationServices;
            // Schedule the recurring job

            RecurringJob.AddOrUpdate<PolygonJob>(
                "polygon-job",
                (e) => e.FetchStockDataAsync("AAPL"),
                Cron.Minutely()
            );
            
            // Optionally, run the job immediately on startup
            BackgroundJob.Enqueue<PolygonJob>(job => job.FetchStockDataAsync("AAPL"));
        });
    });

var host = builder.Build();
host.Run();
