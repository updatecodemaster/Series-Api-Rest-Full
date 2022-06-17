using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using DocumentacaoSwagger.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentacaoSwagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<Contexto>(conexao=> conexao.UseMySql(Configuration.GetConnectionString("ConexaoBD"),ServerVersion.Parse("8.0.29")
    ));


            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api de Series",
                    Description = "Api feita para a aula do professor Paulo Fonseca, Faculdade UNIRON !<br>" +
                    "Api desenvolvida no intuito de criar uma base de dados de series.<br>" +
                    "O exemplos de como utilizar estão abaixo com as instruções e limite de requisições",
                    Contact = new OpenApiContact()
                    {
                        Name = "Rafael dos Santos",
                        Email = "Rafaelsantos4002@gmail.com",
                        Url = new Uri("https://github.com/updatecodemaster")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Open Source",
                        Url = new Uri("https://pt.wikipedia.org/wiki/C%C3%B3digo_aberto")
                    }
                });

                var arquivoSwaggerXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var diretorioArquivoXML = Path.Combine(AppContext.BaseDirectory, arquivoSwaggerXML);
                swagger.IncludeXmlComments(diretorioArquivoXML);
            });

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("./v1/swagger.json", "API de Series V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
