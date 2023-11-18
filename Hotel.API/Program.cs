using Hotel.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Hotels.Add(new Models.Hotels() { Id = Guid.Parse("B9BC19FC-8F70-48DB-94DB-9DFFB2413408"), Company = "Lara Barut Collection", AuthorizedName="Ahmet",AuthorizedLastName="Barut" });
                context.Hotels.Add(new Models.Hotels() { Id = Guid.Parse("F7E74F60-F768-4470-BC7E-5DB96B52ECD4"), Company = "Conrad Istanbul Bosphorus", AuthorizedName = "Aksoy", AuthorizedLastName = "Holding" });
                context.Hotels.Add(new Models.Hotels() { Id = Guid.Parse("103AA7C5-A459-4000-9724-A8C790D36A3B"), Company = "Hilton Istanbul Bomonti Hotel", AuthorizedName = "Rainer", AuthorizedLastName = "Gieringer" });
                context.ContactInformations.Add(new Models.ContactInformations() { Id = Guid.Parse("D593CB2C-E05D-42AC-A4A1-C6289BEC6E48"), HotelId= Guid.Parse("B9BC19FC-8F70-48DB-94DB-9DFFB2413408"), PhoneNumber = "444 9 600", Email = "larabarut@barut.com", Location = "Güzeloba Mah Yaþar Sobutay Bulvarý No:30, VV44+J6, 07235 Muratpaþa/Antalya" });
                context.ContactInformations.Add(new Models.ContactInformations() { Id = Guid.Parse("903C1A20-819D-4AB1-A85E-2DA91ADA29F2"), HotelId= Guid.Parse("F7E74F60-F768-4470-BC7E-5DB96B52ECD4"), PhoneNumber = "(0212) 310 25 25", Email = "conrad@conrad.com", Location = "Cihannüma, Saray Cd No:5, 34353 Beþiktaþ/Ýstanbul" });
                context.ContactInformations.Add(new Models.ContactInformations() { Id = Guid.Parse("DBED0BB8-52FE-4CF7-9EE3-2A4BB830E1B7"), HotelId = Guid.Parse("103AA7C5-A459-4000-9724-A8C790D36A3B"), PhoneNumber = "(0212) 375 30 00", Email = "hiltonistanbul@hiltonistanbul.com", Location = " Merkez, Silahþör Cd. No:42, 34381 Þiþli/Ýstanbul" });

                context.SaveChanges();
            }
            host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
