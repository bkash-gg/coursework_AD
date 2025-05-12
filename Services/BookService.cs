﻿using AD_Coursework.DTOs.Book;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;
using AD_Coursework.Utils;

namespace AD_Coursework.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetAllAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetAllBooksAsync(page, pageSize);
            var totalCount = await _bookRepository.GetTotalBooksCountAsync();

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(), 
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<BookDto?> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return null;

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            };
            return bookDto;
        }

        public async Task<BookDto> CreateAsync(BookCreateDto bookCreateDto)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(bookCreateDto.PublisherId);
            if (publisher == null)
            {
                throw new ArgumentException("Invalid Publisher ID.");
            }

            var author = await _authorRepository.GetAuthorByIdAsync(bookCreateDto.AuthorId);
            if(author == null)
            {
                throw new ArgumentException("Invalid Author ID.");
            }

            if (bookCreateDto.GenreIds != null && bookCreateDto.GenreIds.Any())
            {
                foreach (var genreId in bookCreateDto.GenreIds)
                {
                    var genreExists = await _genreRepository.GenreExistsAsync(genreId);
                    if (!genreExists)
                    {
                        throw new Exception($"Genre with ID {genreId} does not exist.");
                    }
                }
            }

            string? coverImageUrl = null;
            if (bookCreateDto.ImageFile != null)
            {
                coverImageUrl = await ImageHelper.SaveImageAsync(bookCreateDto.ImageFile);
            }

            var book = new Book
            {
                Title = bookCreateDto.Title,
                ISBN = bookCreateDto.ISBN,
                PublicationDate = DateTime.SpecifyKind(bookCreateDto.PublicationDate, DateTimeKind.Utc),
                Language = bookCreateDto.Language,
                Description = bookCreateDto.Description,
                Price = bookCreateDto.Price,
                StockQuantity = bookCreateDto.StockQuantity,
                IsAvailable = bookCreateDto.IsAvailable,
                Format = bookCreateDto.Format,
                CoverImageUrl = coverImageUrl,
                IsAwardWinner = bookCreateDto.IsAwardWinner,
                IsComingSoon = bookCreateDto.IsComingSoon,
                PublisherId = bookCreateDto.PublisherId,
                AuthorId = bookCreateDto.AuthorId,
            };

            var createdBook = await _bookRepository.CreateBookAsync(book);

            if (bookCreateDto.GenreIds != null && bookCreateDto.GenreIds.Any())
            {
                await _bookRepository.UpdateBookGenresAsync(createdBook.Id, bookCreateDto.GenreIds);
            }

            var bookDto = new BookDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                ISBN = createdBook.ISBN,
                PublicationDate = createdBook.PublicationDate,
                Language = createdBook.Language,
                Description = createdBook.Description,
                Price = createdBook.Price,
                StockQuantity = createdBook.StockQuantity,
                IsAvailable = createdBook.IsAvailable,
                Format = createdBook.Format,
                CoverImageUrl = createdBook.CoverImageUrl,
                IsAwardWinner = createdBook.IsAwardWinner,
                IsComingSoon = createdBook.IsComingSoon,
                PublisherName = createdBook.Publisher?.Name ?? string.Empty,
                AuthorName = createdBook.Author?.Name ?? string.Empty,
                CreatedAt = createdBook.CreatedAt
            };

            return bookDto;
        }

        public async Task<BookDto?> UpdateAsync(Guid id, BookUpdateDto bookUpdateDto)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(bookUpdateDto.PublisherId);
            if (publisher == null)
            {
                throw new ArgumentException("Invalid Publisher ID.");
            }

            var author = await _authorRepository.GetAuthorByIdAsync(bookUpdateDto.AuthorId);
            if (author == null)
            {
                throw new ArgumentException("Invalid Author ID.");
            }

            if (bookUpdateDto.GenreIds != null && bookUpdateDto.GenreIds.Any())
            {
                foreach (var genreId in bookUpdateDto.GenreIds)
                {
                    var genreExists = await _genreRepository.GenreExistsAsync(genreId);
                    if (!genreExists)
                    {
                        throw new Exception($"Genre with ID {genreId} does not exist.");
                    }
                }
            }

            string? coverImageUrl = null;
            if (bookUpdateDto.ImageFile != null)
            {
                coverImageUrl = await ImageHelper.SaveImageAsync(bookUpdateDto.ImageFile);
            }
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null) return null;

            existingBook.Title = bookUpdateDto.Title;
            existingBook.ISBN = bookUpdateDto.ISBN;
            existingBook.PublicationDate = bookUpdateDto.PublicationDate;
            existingBook.Language = bookUpdateDto.Language;
            existingBook.Description = bookUpdateDto.Description;
            existingBook.Price = bookUpdateDto.Price;
            existingBook.StockQuantity = bookUpdateDto.StockQuantity;
            existingBook.IsAvailable = bookUpdateDto.IsAvailable;
            existingBook.Format = bookUpdateDto.Format;
            existingBook.CoverImageUrl = coverImageUrl ?? existingBook.CoverImageUrl;
            existingBook.IsAwardWinner = bookUpdateDto.IsAwardWinner;
            existingBook.IsComingSoon = bookUpdateDto.IsComingSoon;
            existingBook.PublisherId = bookUpdateDto.PublisherId;
            existingBook.AuthorId = bookUpdateDto.AuthorId;

            var updatedBook = await _bookRepository.UpdateBookAsync(existingBook);

            if (bookUpdateDto.GenreIds != null)
            {
                await _bookRepository.UpdateBookGenresAsync(id, bookUpdateDto.GenreIds);
            }

            var bookDto = new BookDto
            {
                Id = updatedBook.Id,
                Title = updatedBook.Title,
                ISBN = updatedBook.ISBN,
                PublicationDate = updatedBook.PublicationDate,
                Language = updatedBook.Language,
                Description = updatedBook.Description,
                Price = updatedBook.Price,
                StockQuantity = updatedBook.StockQuantity,
                IsAvailable = updatedBook.IsAvailable,
                Format = updatedBook.Format,
                CoverImageUrl = updatedBook.CoverImageUrl,
                IsAwardWinner = updatedBook.IsAwardWinner,
                IsComingSoon = updatedBook.IsComingSoon,
                PublisherName = updatedBook.Publisher?.Name ?? string.Empty,
                AuthorName = updatedBook.Author?.Name ?? string.Empty,
                CreatedAt = updatedBook.CreatedAt
            };

            return bookDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetNewReleasesAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetNewReleasesAsync(page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetNewArrivalsAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetNewArrivalsAsync(page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBestSellersAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetBestSellersAsync(page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetAwardWinnersAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetAwardWinnersAsync(page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetComingSoonBooksAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetComingSoonBooksAsync(page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByAuthorAsync(Guid authorId, int page, int pageSize)
        {
            var books = await _bookRepository.GetBooksByAuthorAsync(authorId, page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByPublisherAsync(Guid publisherId, int page, int pageSize)
        {
            var books = await _bookRepository.GetBooksByPublisherAsync(publisherId, page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByGenreAsync(Guid genreId, int page, int pageSize)
        {
            var books = await _bookRepository.GetBooksByGenreAsync(genreId, page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> SearchBooksAsync(string searchTerm, int page, int pageSize)
        {
            var books = await _bookRepository.SearchBooksAsync(searchTerm, page, pageSize);
            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                null, null, null, null, null, null, null, null, null, null, true);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }

        public async Task<(IEnumerable<BookDto> Books, int TotalCount)> FilterBooksAsync(
            string? searchTerm,
            List<Guid>? authorIds,
            List<Guid>? genreIds,
            List<Guid>? publisherIds,
            decimal? minPrice,
            decimal? maxPrice,
            string? language,
            string? format,
            bool? isAvailable,
            bool? isAwardWinner,
            bool? isComingSoon,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize)
        {
            var books = await _bookRepository.FilterBooksAsync(
                searchTerm,
                authorIds,
                genreIds,
                publisherIds,
                minPrice,
                maxPrice,
                language,
                format,
                isAvailable,
                isAwardWinner,
                isComingSoon,
                sortBy,
                ascending,
                page,
                pageSize);

            var totalCount = await _bookRepository.GetFilteredBooksCountAsync(
                searchTerm,
                authorIds,
                genreIds,
                publisherIds,
                minPrice,
                maxPrice,
                language,
                format,
                isAvailable,
                isAwardWinner,
                isComingSoon);

            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                IsAvailable = book.IsAvailable,
                Format = book.Format,
                CoverImageUrl = book.CoverImageUrl,
                IsAwardWinner = book.IsAwardWinner,
                IsComingSoon = book.IsComingSoon,
                PublisherName = book.Publisher?.Name ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                Genres = book.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                CreatedAt = book.CreatedAt
            });

            return (bookDtos, totalCount);
        }
    }
}