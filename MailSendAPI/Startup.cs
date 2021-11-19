using Application.IServices;
using Application.Services;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Infrastructure;
using Infrastructure.Logger;
using MailSendAPI.Configurations;

namespace MailSendAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      Logger = LoggerConfigurationService.CreateLogger(Configuration);
      Logger.Information("Logger configured");
    }

    public IConfiguration Configuration { get; }

    protected readonly ILogger Logger;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddScoped<IFileHeaderService, FileHeaderService>();
      services.AddScoped<IFileAttachmentService, FileAttachmentService>();
      services.AddScoped<IFileService, FileService>();
      services.AddScoped<ITemplateService, TemplateService>();
      //services.AddScoped<IMessageService, MessageService>();
      //services.AddScoped<IMailService, MailService>();
      services.AddControllers(opt =>
      {
        opt.Filters.Add(new BusinessExceptionFilter(Logger));
      });

      services.AddEmail(Configuration);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MailSendAPI", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailSendAPI v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
