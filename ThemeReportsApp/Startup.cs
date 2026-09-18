using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ThemeReportsApp
{
    public class Startup
    {

        // ランタイムから呼び出され、アプリケーションで使用するサービスを登録します。
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services
                .AddLogging(config =>
                {
                    // 既定のログ出力プロバイダーを無効にします。
                    config.ClearProviders();

                    // 開発環境でのみコンソールログを有効にします。
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    {
                        config.AddConsole();
                    }
                })
                .AddReportViewer()
                .AddMvc(option => option.EnableEndpointRouting = false);
        }

        // ランタイムから呼び出され、HTTPリクエストの処理パイプラインを構成します。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseReportViewer(settings =>
            {
                var reportsFolder = Path.Combine(env.ContentRootPath, "Reports");
                settings.UseFileStore(new DirectoryInfo(reportsFolder));
                settings.UseConfig(Path.Combine(env.ContentRootPath, "ActiveReports.config"));
            });

            app.UseMvc();
        }
    }
}
