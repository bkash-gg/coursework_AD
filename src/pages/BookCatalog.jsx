<<<<<<< HEAD
import React, { useState, useEffect } from 'react';
import FilterPanel from '../components/FilterPanel';
import CategoryTabs from '../components/CategoryTabs';
import { FaHeart, FaShoppingCart } from 'react-icons/fa';
import Navbar from '../components/Navbar';
import { useCart } from '../context/CartContext';
import axios from 'axios';
=======
import React, { useState } from "react";
import FilterPanel from "../components/FilterPanel";
import CategoryTabs from "../components/CategoryTabs";
import { FaHeart, FaShoppingCart } from "react-icons/fa";
import { useCart } from "../context/CartContext";
const mockBooks = new Array(20).fill(0).map((_, i) => ({
  id: i + 1,
  title: `Book ${i + 1}`,
  author: `Author ${i + 1}`,
  price: (Math.random() * 40 + 10).toFixed(2),
  oldPrice: i % 3 === 0 ? (Math.random() * 70 + 60).toFixed(2) : null,
  rating: (Math.random() * 2 + 3).toFixed(1),
  format: ["Hardcover", "Deluxe", "Signed"][i % 3],
  inStock: i % 4 !== 0,
  genre: ["Fiction", "Mystery", "History", "Fantasy"][i % 4],
  publisher: ["Penguin", "HarperCollins", "Macmillan"][i % 3],
  language: ["English", "Nepali", "Sanskrit"][i % 3],
  discount: i % 3 === 0 ? `${10 + (i % 3) * 5}%` : null,
  img: "/book3.jpg",
  isBestseller: i % 5 === 0,
  isAwardWinner: i % 7 === 0,
  isNewRelease: i > 15,
  isNewArrival: i > 17,
  isComingSoon: i >= 19,
  isDeal: i % 3 === 0,
}));
>>>>>>> 8dad3c6e3063b8f61f99d56c5e0e8d355de97b7f

const CATEGORY_ENDPOINTS = {
  all: 'https://localhost:7098/api/books',
  bestsellers: 'https://localhost:7098/api/books/best-sellers',
  awardWinners: 'https://localhost:7098/api/books/award-winners',
  newReleases: 'https://localhost:7098/api/books/new-releases',
  newArrivals: 'https://localhost:7098/api/books/new-arrivals',
  comingSoon: 'https://localhost:7098/api/books/coming-soon',
  deals: 'https://localhost:7098/api/books/filter?deal=true',
};

const BookCatalog = () => {
<<<<<<< HEAD
  const [books, setBooks] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [sortBy, setSortBy] = useState('title');
  const [activeCategory, setActiveCategory] = useState('all');
=======
  const [searchQuery, setSearchQuery] = useState("");
  const [sortBy, setSortBy] = useState("title");
  const [activeCategory, setActiveCategory] = useState("all");
>>>>>>> 8dad3c6e3063b8f61f99d56c5e0e8d355de97b7f
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const booksPerPage = 12;
  const { addToCart } = useCart();

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        setLoading(true);
        setError(null);
        const response = await axios.get(CATEGORY_ENDPOINTS[activeCategory]);
        const items = response.data?.data?.items ?? [];
        setBooks(Array.isArray(items) ? items : []);
        
        // Extract books from the nested data.items structure
        const booksData = response.data.data?.items || [];
        setBooks(booksData);
      } catch (error) {
        console.error('Error fetching books:', error);
        setError('Failed to load books. Please try again later.');
        setBooks([]);
      } finally {
        setLoading(false);
      }
    };

    fetchBooks();
  }, [activeCategory]);

  const filteredBooks = books
    .filter((book) =>
      book.title?.toLowerCase().includes(searchQuery.toLowerCase())
    )
    .sort((a, b) => {
      switch (sortBy) {
<<<<<<< HEAD
        case 'price':
          return parseFloat(a.price || 0) - parseFloat(b.price || 0);
        case 'rating':
          return parseFloat(b.rating || 0) - parseFloat(a.rating || 0);
=======
        case "price":
          return a.price - b.price;
        case "rating":
          return b.rating - a.rating;
>>>>>>> 8dad3c6e3063b8f61f99d56c5e0e8d355de97b7f
        default:
          return (a.title || '').localeCompare(b.title || '');
      }
    });

  const totalPages = Math.ceil(filteredBooks.length / booksPerPage);
  const paginatedBooks = filteredBooks.slice(
    (currentPage - 1) * booksPerPage,
    currentPage * booksPerPage
  );

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 p-4 md:p-8">
        <Navbar />
        <div className="flex justify-center items-center h-64">
          <p>Loading books...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gray-50 p-4 md:p-8">
        <Navbar />
        <div className="flex justify-center items-center h-64">
          <p className="text-red-500">{error}</p>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-gray-50">
      <div className="min-h-screen p-6 container mx-auto">
        <h1 className="text-3xl font-bold mb-6">Book Catalog</h1>

        <CategoryTabs
          activeCategory={activeCategory}
          setActiveCategory={(category) => {
            setActiveCategory(category);
            setCurrentPage(1);
          }}
        />

        <div className="flex flex-col lg:flex-row gap-6">
          <FilterPanel />

<<<<<<< HEAD
        <div className="flex-1">
          <div className="flex flex-col sm:flex-row justify-between items-center gap-4 mb-4">
            <input
              type="text"
              placeholder="Search books..."
              className="border px-3 py-2 rounded-md w-full sm:w-1/2"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
            <select
              className="border px-2 py-1 rounded text-sm"
              value={sortBy}
              onChange={(e) => setSortBy(e.target.value)}
            >
              <option value="title">Sort: Title (A-Z)</option>
              <option value="price">Sort: Price (Low → High)</option>
              <option value="rating">Sort: Rating (High → Low)</option>
            </select>
          </div>

          <p className="text-gray-700 mb-4">{filteredBooks.length} books found</p>

          {filteredBooks.length === 0 ? (
            <div className="text-center py-12">
              <p>No books found matching your criteria.</p>
            </div>
          ) : (
            <>
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {paginatedBooks.map((book) => (
                  <div
                    key={book.id}
                    className="group bg-white border rounded-lg shadow hover:shadow-lg transition p-4 relative overflow-hidden"
                  >
                    <img
                      src={`https://localhost:7098${book.coverImageUrl}`}
                      alt={book.title}
                      className="w-full h-48 object-contain mb-3"
                    />
                    <h3 className="font-semibold text-lg text-gray-800 truncate">{book.title}</h3>
                    <p className="text-sm text-gray-500 mb-1">by {book.authorName}</p>
                    <p className="text-xs text-gray-500 mb-1">{book.format} - {book.publisherName}</p>
                    <p className="text-xs text-gray-500 mb-1">Language: {book.language}</p>

                    <div className="mb-2">
                      <span className="text-lg font-bold text-[#0F4C81]">${book.price}</span>
                    </div>

                    <div className="flex justify-between items-center mt-2">
                      <button className="flex items-center gap-1 border px-3 py-1 rounded hover:bg-gray-100 text-sm">
                        <FaHeart /> Save
                      </button>
                      <button
                        className={`flex items-center gap-1 px-3 py-1 rounded text-sm text-white ${
                          book.isAvailable
                            ? 'bg-[#0F4C81] hover:bg-[#0d3e6a]'
                            : 'bg-gray-400 cursor-not-allowed'
                        }`}
                        disabled={!book.isAvailable}
                        onClick={() => book.isAvailable && addToCart(book)}
                      >
                        <FaShoppingCart /> Add
                      </button>
                    </div>
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
                <span className="text-sm">
                  Page {currentPage} of {totalPages}
                </span>
                <button
                  disabled={currentPage === totalPages}
                  onClick={() => setCurrentPage((p) => Math.min(totalPages, p + 1))}
                  className="text-sm px-3 py-1 border rounded disabled:opacity-50"
                >
                  Next
                </button>
              </div>
            </>
          )}
=======
          <div className="flex-1">
            <div className="flex flex-col sm:flex-row justify-between items-center gap-4 mb-4">
              <input
                type="text"
                placeholder="Search books..."
                className="border px-3 py-2 rounded-md w-full sm:w-1/2"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
              />
              <select
                className="border px-2 py-1 rounded text-sm"
                value={sortBy}
                onChange={(e) => setSortBy(e.target.value)}
              >
                <option value="title">Sort: Title (A-Z)</option>
                <option value="price">Sort: Price (Low → High)</option>
                <option value="rating">Sort: Rating (High → Low)</option>
              </select>
            </div>

            <p className="text-gray-700 mb-4">
              {filteredBooks.length} books found
            </p>

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
              {paginatedBooks.map((book) => (
                <div
                  key={book.id}
                  className="group bg-white border rounded-lg shadow hover:shadow-lg transition p-4 relative overflow-hidden"
                >
                  {book.discount && (
                    <div className="absolute top-2 left-2 bg-red-600 text-white text-xs px-2 py-1 rounded-full opacity-0 group-hover:opacity-100 transition">
                      {book.discount} OFF
                    </div>
                  )}

                  <img
                    src={book.img}
                    alt={book.title}
                    className="w-full h-48 object-contain mb-3"
                  />
                  <h3 className="font-semibold text-lg text-gray-800 truncate">
                    {book.title}
                  </h3>
                  <p className="text-sm text-gray-500 mb-1">by {book.author}</p>
                  <p className="text-xs text-gray-500 mb-1">
                    {book.format} - {book.publisher}
                  </p>
                  <p className="text-xs text-gray-500 mb-1">
                    Language: {book.language}
                  </p>
                  <p className="text-sm text-yellow-600 mb-1">
                    ⭐ {book.rating}
                  </p>

                  <div className="mb-2">
                    <span className="text-lg font-bold text-[#0F4C81]">
                      ${book.price}
                    </span>
                    {book.oldPrice && (
                      <span className="text-sm line-through ml-2 text-gray-400">
                        ${book.oldPrice}
                      </span>
                    )}
                  </div>

                  <div className="flex justify-between items-center mt-2">
                    <button className="flex items-center gap-1 border px-3 py-1 rounded hover:bg-gray-100 text-sm">
                      <FaHeart /> Save
                    </button>
                    <button
                      className={`flex items-center gap-1 px-3 py-1 rounded text-sm text-white ${
                        book.inStock
                          ? "bg-[#0F4C81] hover:bg-[#0d3e6a]"
                          : "bg-gray-400 cursor-not-allowed"
                      }`}
                      disabled={!book.inStock}
                      onClick={() => book.inStock && addToCart(book)}
                    >
                      <FaShoppingCart /> Add
                    </button>
                  </div>
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
              <span className="text-sm">
                Page {currentPage} of {totalPages}
              </span>
              <button
                disabled={currentPage === totalPages}
                onClick={() =>
                  setCurrentPage((p) => Math.min(totalPages, p + 1))
                }
                className="text-sm px-3 py-1 border rounded disabled:opacity-50"
              >
                Next
              </button>
            </div>
          </div>
>>>>>>> 8dad3c6e3063b8f61f99d56c5e0e8d355de97b7f
        </div>
      </div>
    </div>
    </div>
  );
};

export default BookCatalog;