using AD_Coursework.DTOs.Publisher;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<PublisherDto>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllPublishersAsync();

            var publisherDtos = publishers.Select(p => new PublisherDto
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber
            });

            return publisherDtos;
        }

        public async Task<PublisherDto?> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return null;
            }

            return new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Address = publisher.Address,
                PhoneNumber = publisher.PhoneNumber,
                Email = publisher.Email
            };
        }

        public async Task<PublisherDto> CreateAsync(PublisherCreateDto publisherCreateDto)
        {
            if (string.IsNullOrWhiteSpace(publisherCreateDto.Name) ||
                string.IsNullOrWhiteSpace(publisherCreateDto.Email) ||
                string.IsNullOrWhiteSpace(publisherCreateDto.Address) ||
                string.IsNullOrWhiteSpace(publisherCreateDto.PhoneNumber))
            {
                throw new ArgumentException("All fields are required.");
            }

            if (await _publisherRepository.EmailExistsAsync(publisherCreateDto.Email))
            {
                throw new InvalidOperationException("A publisher with this email already exists.");
            }

            if (await _publisherRepository.PhoneNumberExistsAsync(publisherCreateDto.PhoneNumber))
            {
                throw new InvalidOperationException("A publisher with this phone number already exists.");
            }

            var publisher = new Publisher
            {
                Name = publisherCreateDto.Name,
                Email = publisherCreateDto.Email,
                Address = publisherCreateDto.Address,
                PhoneNumber = publisherCreateDto.PhoneNumber
            };

            var createdPublisher = await _publisherRepository.CreatePublisherAsync(publisher);

            var dto = new PublisherDto
            {
                Id = createdPublisher.Id,
                Name = createdPublisher.Name,
                Email = createdPublisher.Email,
                Address = createdPublisher.Address,
                PhoneNumber = createdPublisher.PhoneNumber
            };

            return dto;
        }

        public async Task<PublisherDto?> UpdateAsync(Guid id, PublisherUpdateDto publisherUpdateDto)
        {
            var existingPublisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (existingPublisher == null)
                return null;

            if (existingPublisher.Email != publisherUpdateDto.Email &&
                await _publisherRepository.EmailExistsAsync(publisherUpdateDto.Email, id))
            {
                throw new InvalidOperationException("A publisher with this email already exists.");
            }

            if (existingPublisher.PhoneNumber != publisherUpdateDto.PhoneNumber &&
                await _publisherRepository.PhoneNumberExistsAsync(publisherUpdateDto.PhoneNumber, id))
            {
                throw new InvalidOperationException("A publisher with this phone number already exists.");
            }

            existingPublisher.Name = publisherUpdateDto.Name;
            existingPublisher.Email = publisherUpdateDto.Email;
            existingPublisher.Address = publisherUpdateDto.Address;
            existingPublisher.PhoneNumber = publisherUpdateDto.PhoneNumber;

            var updatedPublisher = await _publisherRepository.UpdatePublisherAsync(existingPublisher);
            if (updatedPublisher == null)
                return null;

            return new PublisherDto
            {
                Id = updatedPublisher.Id,
                Name = updatedPublisher.Name,
                Email = updatedPublisher.Email,
                Address = updatedPublisher.Address,
                PhoneNumber = updatedPublisher.PhoneNumber
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _publisherRepository.DeletePublisherAsync(id);
        }
    }
}
