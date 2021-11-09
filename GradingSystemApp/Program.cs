using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofact;
using GradingSystemApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        //outofact config�rasyonu yap�ld�. .Net default tan�ml� konfig�rasyonu kullanma diyoruz.
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //tan�mlad���m�z businessmodule u burada verdik.
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutofacBusinessModule());
        })

        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });


