using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Author;
using AD_Coursework.Models;

namespace AD_Coursework.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            var authorDtos = authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                Bio = a.Bio
            });

            return authorDtos;
        }

        public async Task<AuthorDto?> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return null;
            }

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Bio = author.Bio
            };
        }

        public async Task<AuthorDto> CreateAsync(AuthorCreateDto authorCreateDto)
        {
            if (string.IsNullOrWhiteSpace(authorCreateDto.Name) ||
                string.IsNullOrWhiteSpace(authorCreateDto.Email))
            {
                throw new ArgumentException("Name and Email are required.");
            }

            if (await _authorRepository.EmailExistsAsync(authorCreateDto.Email))
            {
                throw new InvalidOperationException("An author with this email already exists.");
            }

            var author = new Author
            {
                Name = authorCreateDto.Name,
                Email = authorCreateDto.Email,
                Bio = authorCreateDto.Bio
            };

            var createdAuthor = await _authorRepository.CreateAuthorAsync(author);

            var dto = new AuthorDto
            {
                Id = createdAuthor.Id,
                Name = createdAuthor.Name,
                Email = createdAuthor.Email,
                Bio = createdAuthor.Bio
            };

            return dto;
        }

        public async Task<AuthorDto?> UpdateAsync(Guid id, AuthorUpdateDto authorUpdateDto)
        {
            var existingAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (existingAuthor == null)
                return null;

            if (existingAuthor.Email != authorUpdateDto.Email &&
                await _authorRepository.EmailExistsAsync(authorUpdateDto.Email, id))
            {
                throw new InvalidOperationException("An author with this email already exists.");
            }

            existingAuthor.Name = authorUpdateDto.Name;
            existingAuthor.Email = authorUpdateDto.Email;
            existingAuthor.Bio = authorUpdateDto.Bio;

            var updatedAuthor = await _authorRepository.UpdateAuthorAsync(existingAuthor);
            if (updatedAuthor == null)
                return null;

            return new AuthorDto
            {
                Id = updatedAuthor.Id,
                Name = updatedAuthor.Name,
                Email = updatedAuthor.Email,
                Bio = updatedAuthor.Bio
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}
