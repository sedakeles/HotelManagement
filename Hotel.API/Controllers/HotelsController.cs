using Hotel.API.DTOs;
using Hotel.API.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public HotelsController(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet("GetAllHotel")]
        public async Task<IActionResult> GetAllHotel()
        {
            var hotels = await _context.Hotels
          .Include(o => o.ContactInformations)
          .ToListAsync();

            if (hotels == null || hotels.Count == 0)
            {
                return NotFound("Hiç otel bulunamadı.");
            }
            var hotelDtoList = hotels.Select(hotel => new HotelDto
            {
                Id = hotel.Id,
                AuthorizedName = hotel.AuthorizedName,
                AuthorizedLastName = hotel.AuthorizedLastName,
                Company=hotel.Company,
                contactInformations = hotel.ContactInformations.Select(contactInfo => new ContactInformationDto
                {
                    Id = contactInfo.Id,
                    HotelId = contactInfo.HotelId,
                    PhoneNumber = contactInfo.PhoneNumber,
                    Email = contactInfo.Email,
                    Location = contactInfo.Location
                }).ToList()
            }).ToList();

            return Ok(hotelDtoList);
        }
        [HttpGet("GetAllHotelAuthorized")]
        public async Task<IActionResult> GetAllHotelAuthorized()
        {
            var hotels = await _context.Hotels.Select(hotel=>new HotelAuthorizedDto
            {
                
                AuthorizedName=hotel.AuthorizedName,
                AuthorizedLastName=hotel.AuthorizedLastName,
                Company =hotel.Company,
            }).ToListAsync();
            if (hotels == null || hotels.Count == 0)
            {
                return NotFound("Hiç otel bulunamadı.");
            }
            return Ok(hotels);
        }


        [HttpPost]
        public async Task<IActionResult> Create(HotelCreateDto hotelCreate)
        {
            var newHotel = new Models.Hotels
            {
                Id = Guid.NewGuid(),
                AuthorizedName = hotelCreate.AuthorizedName,
                AuthorizedLastName = hotelCreate.AuthorizedLastName,
                Company = hotelCreate.Company,
            };

            hotelCreate.contactInformations.ForEach(item =>
            {
                newHotel.ContactInformations.Add(new ContactInformations() { Id = Guid.NewGuid(), PhoneNumber = item.PhoneNumber, Email = item.Email, Location = item.Location });
            });

            await _context.AddAsync(newHotel);
            await _context.SaveChangesAsync();

            var hotelCreatedEvent = new HotelCreatedEvent()
            {

                Id = newHotel.Id,


            };
            hotelCreate.contactInformations.ForEach(item =>
            {
                hotelCreatedEvent.ContactInformations.Add(new ContactInformationMessage() { Location = item.Location, Email = item.Email, PhoneNumber = item.PhoneNumber });
            });
            await _publishEndpoint.Publish(hotelCreatedEvent);
            return Ok();
        }

        [HttpDelete("{HotelId}")]
        public async Task Delete(Guid HotelId)
        {
            var hotel = await _context.Hotels.FindAsync(HotelId);
            if (hotel != null)
            {
                var contact = _context.ContactInformations.Include(x => x.HotelId == HotelId);
                if (hotel.ContactInformations.Count > 0)
                {
                    _context.ContactInformations.RemoveRange(contact);
                }
            }
            _context.Remove(hotel);

            await _context.SaveChangesAsync();
        }


        [HttpPost("AddContactInformation")]
        public async Task<IActionResult> AddContactInformation(ContactInformationDto contactInformationDto)
        {

            var hotel = await _context.Hotels.FindAsync(contactInformationDto.HotelId);
            if (hotel != null)
            {
                var newContactInformation = new Models.ContactInformations
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = contactInformationDto.PhoneNumber,
                    Email = contactInformationDto.Email,
                    Location = contactInformationDto.Location,
                    HotelId = contactInformationDto.HotelId,

                };


                await _context.AddAsync(newContactInformation);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("Belirtilen otel bulunamadı. İletişim bilgisi eklemek için önce otel kaydı oluşturun.");


        }

        [HttpDelete("DeleteContactInformationId/{ContactInformationId}")]
        public async Task DeleteContactInformationId(Guid ContactInformationId)
        {
            var contactInfo = await _context.ContactInformations.FindAsync(ContactInformationId);
            if (contactInfo != null)
            {
                _context.Remove(contactInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
