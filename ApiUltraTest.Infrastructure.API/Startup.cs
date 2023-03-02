using ApiUltraTest.Infrastructure.Data.Repositories.HotelRepository;
using ApiUltraTest.Infrastructure.Data.Repositories.RoomRepository;
using ApiUltraTest.Infrastructure.Providers.MailService;
using ApiUltraTest.Infrastructure.Data.Configs.Context;
using ApiUltraTest.Infrastructure.Providers.Configs;
using ApiUltraTest.Infrastructure.Data.Repositories;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Services;
using ApiUltraTest.Application.Dtos;
using ApiUltraTest.Domain;

namespace ApiUltraTest.Infrastructure.API
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureContainer(IServiceCollection services, ConfigurationManager configuration)
        {
            ConfigureSettings(services, configuration);
            ConfigureDbContext(services);
            ConfigureRepositories(services);
            ConfigureServices(services);
            ConfigureProviders(services);

        }

        protected void ConfigureProviders(IServiceCollection services)
        {

            services.AddSingleton<IMailNotificationService, MailKitService>();

        }

        protected void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IHotelService<HotelDto>, HotelService>();
            services.AddSingleton<IRoomService<RoomDto>, RoomService>();
            services.AddSingleton<IBookingService<BookingDto>, BookingService>();

        }

        protected void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IHotelRepository<Hotel>, HotelRepository>(x =>
            {
                var db = x.GetRequiredService<Context>();
                return new HotelRepository(db);
            });

            services.AddSingleton<IRoomRepository<Room>, RoomRepository>(x =>
            {
                var db = x.GetRequiredService<Context>();
                return new RoomRepository(db);
            });

            services.AddSingleton<IBookingRepository<Booking>, BookingRepository>(x =>
            {
                var db = x.GetRequiredService<Context>();
                return new BookingRepository(db);
            });
        }

        protected void ConfigureDbContext(IServiceCollection services)
        {

            //Scope in repo || singleton in the context
            services.AddTransient<Context>();
        }

        protected void ConfigureSettings(IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<GeneralSettings>(configuration.GetSection("SettingsMongoDatabase"));

            services.Configure<MailSettings>(configuration.GetSection("SettingsMailKitService"));

        }


    }
}

