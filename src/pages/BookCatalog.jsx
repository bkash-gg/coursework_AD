import React, { useState } from 'react';
import FilterPanel from '../components/FilterPanel';

const mockBooks = new Array(23).fill(0).map((_, i) => ({
  id: i + 1,
  title: `Book Title ${i + 1}`,
  author: `Author ${i + 1}`,
  price: (Math.random() * 90 + 10).toFixed(2),
  rating: (Math.random() * 2 + 3).toFixed(1),
  format: ['Hardcover', 'Deluxe', 'Signed'][i % 3],
  inStock: i % 3 !== 0,
  img: `https://via.placeholder.com/150?text=Book+${i + 1}`,
}));

const sortBooks = (books, sortBy) => {
  switch (sortBy) {
    case 'title':
      return [...books].sort((a, b) => a.title.localeCompare(b.title));
    case 'price':
      return [...books].sort((a, b) => a.price - b.price);
    case 'rating':
      return [...books].sort((a, b) => b.rating - a.rating);
    default:
      return books;
  }
};

const BookCatalog = () => {
  const [sortBy, setSortBy] = useState('title');
  const [currentPage, setCurrentPage] = useState(1);
  const booksPerPage = 8;

  const sortedBooks = sortBooks(mockBooks, sortBy);
  const totalPages = Math.ceil(sortedBooks.length / booksPerPage);
  const paginatedBooks = sortedBooks.slice(
    (currentPage - 1) * booksPerPage,
    currentPage * booksPerPage
  );

  return (
    <div className="min-h-screen bg-gray-50 p-4 md:p-8">
      <h1 className="text-3xl font-bold mb-6">Book Catalog</h1>
      <div className="flex flex-col lg:flex-row gap-6">
        <FilterPanel />

        <div className="flex-1">
          <div className="flex justify-between items-center mb-4">
            <p className="text-gray-700">{mockBooks.length} books found</p>
            <select
              className="border border-gray-300 rounded-md px-2 py-1 text-sm"
              value={sortBy}
              onChange={(e) => setSortBy(e.target.value)}
            >
              <option value="title">Sort: Title (A-Z)</option>
              <option value="price">Sort: Price (Low → High)</option>
              <option value="rating">Sort: Rating (High → Low)</option>
            </select>
          </div>

          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-6">
            {paginatedBooks.map((book) => (
              <div
                key={book.id}
                className="bg-white border rounded-lg shadow-sm hover:shadow-md transition-all p-4 flex flex-col"
              >
                <img
                  src={book.img}
                  alt={book.title}
                  className="w-full h-40 object-contain mb-3"
                />
                <h3 className="font-semibold text-gray-800 line-clamp-2">{book.title}</h3>
                <p className="text-sm text-gray-500 mb-1">by {book.author}</p>
                <p className="text-xs text-gray-500 mb-1">{book.format}</p>
                <p className="text-sm mb-1">⭐ {book.rating}</p>
                <p className="font-bold text-[#0F4C81] mb-3">${book.price}</p>
                <button
                  className={`mt-auto py-2 rounded-md text-sm font-medium ${
                    book.inStock
                      ? 'bg-[#0F4C81] text-white hover:bg-[#0d3e6a]'
                      : 'bg-gray-300 text-gray-600 cursor-not-allowed'
                  }`}
                  disabled={!book.inStock}
                >
                  {book.inStock ? 'Add to Cart' : 'Out of Stock'}
                </button>
              </div>
            ))}
          </div>

          {/* Pagination */}
          <div className="flex justify-between items-center mt-6">
            <button
              disabled={currentPage === 1}
              onClick={() => setCurrentPage((p) => Math.max(1, p - 1))}
              className="text-sm px-3 py-1 border rounded disabled:opacity-50"
            >
              Previous
            </button>
            <div className="text-sm">
              Page {currentPage} of {totalPages}
            </div>
            <button
              disabled={currentPage === totalPages}
              onClick={() => setCurrentPage((p) => Math.min(totalPages, p + 1))}
              className="text-sm px-3 py-1 border rounded disabled:opacity-50"
            >
              Next
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookCatalog; 