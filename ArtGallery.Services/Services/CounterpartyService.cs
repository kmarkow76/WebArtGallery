using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.DTO.Counterparties;
using ArtGallery.Interfaces.Repositories;
using ArtGallery.Interfaces.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services;

public class CounterpartyService : ICounterpartyService
    {
        private readonly ICounterpartyRepository _repository;
        private readonly GalleryDbContext _context;

        public CounterpartyService(ICounterpartyRepository repository, GalleryDbContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CounterpartyResponseDto> CreateCounterpartyAsync(CreateCounterpartyDto counterpartyDto)
        {

            var counterparty = new Counterparty
            {
                Name = counterpartyDto.Name,
                PhoneNumber = counterpartyDto.PhoneNumber,
                Address = counterpartyDto.Address,
                Email = counterpartyDto.Email,
                ContactInfo = counterpartyDto.ContactInfo,
                MoneyExpenses = new List<MoneyExpense>(),
                MoneyIncomes = new List<MoneyIncome>(),
                Rentals = new List<Rental>()
            };

            await _repository.AddAsync(counterparty);
            await _context.SaveChangesAsync();

            return new CounterpartyResponseDto
            {
                Id = counterparty.Id,
                Name = counterparty.Name,
                PhoneNumber = counterparty.PhoneNumber,
                Address = counterparty.Address,
                Email = counterparty.Email,
                ContactInfo = counterparty.ContactInfo
            };
        }

        public async Task<CounterpartyResponseDto> GetCounterpartyByIdAsync(int id)
        {
            var counterparty = await _repository.GetByIdAsync(id);
            if (counterparty == null) throw new KeyNotFoundException("Counterparty not found.");
            return new CounterpartyResponseDto
            {
                Id = counterparty.Id,
                Name = counterparty.Name,
                PhoneNumber = counterparty.PhoneNumber,
                Address = counterparty.Address,
                Email = counterparty.Email,
                ContactInfo = counterparty.ContactInfo
            };
        }

        public async Task<IEnumerable<CounterpartyResponseDto>> GetAllCounterpartiesAsync()
        {
            var counterparties = await _repository.GetAllAsync();
            return counterparties.Select(c => new CounterpartyResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Email = c.Email,
                ContactInfo = c.ContactInfo
            });
        }

        public async Task UpdateCounterpartyAsync(int id, UpdateCounterpartyDto counterpartyDto)
        {
            var counterparty = await _repository.GetByIdAsync(id);
            if (counterparty == null) throw new KeyNotFoundException("Counterparty not found.");

            counterparty.Name = counterpartyDto.Name ?? counterparty.Name;
            counterparty.PhoneNumber = counterpartyDto.PhoneNumber ?? counterparty.PhoneNumber;
            counterparty.Address = counterpartyDto.Address ?? counterparty.Address;
            counterparty.Email = counterpartyDto.Email ?? counterparty.Email;
            counterparty.ContactInfo = counterpartyDto.ContactInfo ?? counterparty.ContactInfo;

            await _repository.UpdateAsync(counterparty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCounterpartyAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalIncomeAboveAsync(decimal threshold)
        {
            var counterparties = await _context.Counterparties
                .Include(c => c.MoneyIncomes)
                .ToListAsync();

            var result = counterparties
                .Where(c => c.MoneyIncomes.Sum(mi => mi.Amount) > threshold)
                .Select(c => new CounterpartyResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Email = c.Email,
                    ContactInfo = c.ContactInfo
                });

            return result;
        }

        public async Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalRentalCostAboveAsync(decimal threshold)
        {
            var counterparties = await _context.Counterparties
                .Include(c => c.Rentals)
                .ToListAsync();

            var result = counterparties
                .Where(c => c.Rentals.Sum(r => r.Price) > threshold)
                .Select(c => new CounterpartyResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Email = c.Email,
                    ContactInfo = c.ContactInfo
                });

            return result;
        }
    }