﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using HotelListing.API.Model.Country;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICountriesRepository countriesRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController( IMapper mapper, ICountriesRepository countriesRepository, ILogger<CountriesController> logger)
        {
            _mapper = mapper;
            this.countriesRepository = countriesRepository;
            this._logger = logger;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await countriesRepository.GetDetails(id);
            if (country == null)
            {
                _logger.LogWarning($"Record not found in {nameof(GetCountry)} with Id: {id}.");
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            //_context.Entry(updateCountryDto).State = EntityState.Modified;
            var country = await countriesRepository.GetAsync(id);
            if (country == null) return NotFound();

            _mapper.Map(updateCountryDto, country);
            try
            {
                await countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);
            await countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {

            var country = await countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await countriesRepository.Exists(id);
        }
    }
}
