﻿using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AD_Coursework.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook == null) return null;

            _context.Entry(existingBook).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
            return await _context.Books.CountAsync();
        }

        public async Task<bool> BookExistsAsync(Guid id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> ISBNExistsAsync(string isbn)
        {
            return await _context.Books.AnyAsync(b => b.ISBN == isbn);
        }

        public async Task<bool> ISBNExistsAsync(string isbn, Guid excludeId)
        {
            return await _context.Books.AnyAsync(b => b.ISBN == isbn && b.Id != excludeId);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.AuthorId == authorId)
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByPublisherAsync(Guid publisherId, int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.PublisherId == publisherId)
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(Guid genreId, int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId))
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetNewReleasesAsync(int page, int pageSize)
        {
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);

            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.PublicationDate >= threeMonthsAgo && !b.IsComingSoon)
                .OrderByDescending(b => b.PublicationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetNewArrivalsAsync(int page, int pageSize)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.CreatedAt >= oneMonthAgo)
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBestSellersAsync(int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Include(b => b.OrderItems)
                .Where(b => b.OrderItems.Any()) 
                .OrderByDescending(b => b.OrderItems.Sum(oi => (int?)oi.Quantity) ?? 0)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAwardWinnersAsync(int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.IsAwardWinner)
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetComingSoonBooksAsync(int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.IsComingSoon)
                .OrderBy(b => b.PublicationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int page, int pageSize)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b =>
                    b.Title.Contains(searchTerm) ||
                    b.ISBN.Contains(searchTerm) ||
                    b.Description.Contains(searchTerm))
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> FilterBooksAsync(
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
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchTerm) ||
                    b.ISBN.Contains(searchTerm) ||
                    b.Description.Contains(searchTerm));
            }

            if (authorIds != null && authorIds.Any())
            {
                query = query.Where(b => authorIds.Contains(b.AuthorId));
            }

            if (genreIds != null && genreIds.Any())
            {
                query = query.Where(b => b.BookGenres.Any(bg => genreIds.Contains(bg.GenreId)));
            }

            if (publisherIds != null && publisherIds.Any())
            {
                query = query.Where(b => publisherIds.Contains(b.PublisherId));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                query = query.Where(b => b.Language == language);
            }

            if (!string.IsNullOrWhiteSpace(format))
            {
                query = query.Where(b => b.Format == format);
            }

            if (isAvailable.HasValue)
            {
                query = query.Where(b => b.IsAvailable == isAvailable.Value);
            }

            if (isAwardWinner.HasValue)
            {
                query = query.Where(b => b.IsAwardWinner == isAwardWinner.Value);
            }

            if (isComingSoon.HasValue)
            {
                query = query.Where(b => b.IsComingSoon == isComingSoon.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        query = ascending
                            ? query.OrderBy(b => b.Title)
                            : query.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                        query = ascending
                            ? query.OrderBy(b => b.Price)
                            : query.OrderByDescending(b => b.Price);
                        break;
                    case "date":
                    case "publicationdate":
                        query = ascending
                            ? query.OrderBy(b => b.PublicationDate)
                            : query.OrderByDescending(b => b.PublicationDate);
                        break;
                    case "popularity":
                        query = ascending
                            ? query.OrderBy(b => b.OrderItems.Count)
                            : query.OrderByDescending(b => b.OrderItems.Count);
                        break;
                    default:
                        query = query.OrderBy(b => b.Title);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(b => b.Title);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetFilteredBooksCountAsync(
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
            bool? isComingSoon)
        {
            var query = _context.Books.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchTerm) ||
                    b.ISBN.Contains(searchTerm) ||
                    b.Description.Contains(searchTerm));
            }

            if (authorIds != null && authorIds.Any())
            {
                query = query.Where(b => authorIds.Contains(b.AuthorId));
            }

            if (genreIds != null && genreIds.Any())
            {
                query = query.Where(b => b.BookGenres.Any(bg => genreIds.Contains(bg.GenreId)));
            }

            if (publisherIds != null && publisherIds.Any())
            {
                query = query.Where(b => publisherIds.Contains(b.PublisherId));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                query = query.Where(b => b.Language == language);
            }

            if (!string.IsNullOrWhiteSpace(format))
            {
                query = query.Where(b => b.Format == format);
            }

            if (isAvailable.HasValue)
            {
                query = query.Where(b => b.IsAvailable == isAvailable.Value);
            }

            if (isAwardWinner.HasValue)
            {
                query = query.Where(b => b.IsAwardWinner == isAwardWinner.Value);
            }

            if (isComingSoon.HasValue)
            {
                query = query.Where(b => b.IsComingSoon == isComingSoon.Value);
            }

            return await query.CountAsync();
        }

        public async Task AddBookGenreAsync(BookGenre bookGenre)
        {
            await _context.BookGenres.AddAsync(bookGenre);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookGenreAsync(Guid bookId, Guid genreId)
        {
            var bookGenre = await _context.BookGenres
                .FirstOrDefaultAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);

            if (bookGenre != null)
            {
                _context.BookGenres.Remove(bookGenre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookGenre>> GetBookGenresByBookIdAsync(Guid bookId)
        {
            return await _context.BookGenres
                .Include(bg => bg.Genre)
                .Where(bg => bg.BookId == bookId)
                .ToListAsync();
        }

        public async Task<bool> BookGenreExistsAsync(Guid bookId, Guid genreId)
        {
            return await _context.BookGenres
                .AnyAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);
        }

        public async Task UpdateBookGenresAsync(Guid bookId, List<Guid> genreIds)
        {
            var existingBookGenres = await _context.BookGenres
                .Where(bg => bg.BookId == bookId)
                .ToListAsync();

            foreach (var existingBookGenre in existingBookGenres)
            {
                if (!genreIds.Contains(existingBookGenre.GenreId))
                {
                    _context.BookGenres.Remove(existingBookGenre);
                }
            }

            foreach (var genreId in genreIds)
            {
                if (!existingBookGenres.Any(bg => bg.GenreId == genreId))
                {
                    await _context.BookGenres.AddAsync(new BookGenre
                    {
                        BookId = bookId,
                        GenreId = genreId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksWithDiscountsAsync(int page, int pageSize)
        {
            var now = DateTime.UtcNow;
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Include(b => b.Discounts)
                .Where(b => b.Discounts.Any(d => d.StartDate <= now && d.EndDate >= now))
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}