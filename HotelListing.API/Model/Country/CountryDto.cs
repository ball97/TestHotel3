﻿using HotelListing.API.Model.Hotel;

namespace HotelListing.API.Model.Country
{
    public class CountryDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }
}
