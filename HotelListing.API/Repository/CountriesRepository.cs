using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository: GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDBContext context;

        public CountriesRepository(HotelListingDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Country> GetDetails(int Id)
        {
            return await context.Set<Country>().Include(p => p.Hotels).FirstOrDefaultAsync();
        }
    }
}
