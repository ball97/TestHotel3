using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configuaration
{
    public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
    {
        public HotelConfigurations()
        {

        }

        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "NA Resort and Spa",
                    Address = "123 Negil",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "Geogre Town",
                    CountryId = 3,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 3,
                    Name = "GrandLine",
                    Address = "Nassua",
                    CountryId = 2,
                    Rating = 4
                }
               );
        }
    }
}
