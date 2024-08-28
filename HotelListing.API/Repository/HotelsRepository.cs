using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDBContext hotelListingDBContext;

        public HotelsRepository(HotelListingDBContext hotelListingDBContext) : base(hotelListingDBContext)
        {
            this.hotelListingDBContext = hotelListingDBContext;
        }
    }
}
