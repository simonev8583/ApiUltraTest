using System;
namespace ApiUltraTest.Infrastructure.Data.Configs.Context
{
    public class GeneralSettings
    {
        public string StringConnection { get; set; } = null!;

        public string DataBaseName { get; set; } = null!;

        //Schemas

        public string HotelCollectionName { get; set; } = null!;

        public string RoomCollectionName { get; set; } = null!;

        public string BookingCollectionName { get; set; } = null!;

        public GeneralSettings()
        {
        }
    }
}

