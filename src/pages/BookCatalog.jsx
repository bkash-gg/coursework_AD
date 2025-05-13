import React, { useState, useEffect } from 'react';
import FilterPanel from '../components/FilterPanel';
import CategoryTabs from '../components/CategoryTabs';
import { FaHeart, FaShoppingCart } from 'react-icons/fa';
import { useCart } from '../context/CartContext'; // Uncommented and using the cart context
import axios from 'axios';

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
  const [books, setBooks] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [sortBy, setSortBy] = useState('title');
  const [activeCategory, setActiveCategory] = useState('all');
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [notification, setNotification] = useState({ show: false, message: '', type: '' });
  const booksPerPage = 12;
  const { addToCart } = useCart(); // Using the cart context

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        setLoading(true);
        setError(null);
        const response = await axios.get(CATEGORY_ENDPOINTS[activeCategory]);
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

  const showNotification = (message, type = 'success') => {
    setNotification({ show: true, message, type });
    setTimeout(() => {
      setNotification({ show: false, message: '', type: '' });
    }, 3000);
  };

  const addToWishlist = async (book) => {
    try {
      const token = localStorage.getItem('token');
      if (!token) {
        showNotification('You must be logged in to save books.', 'error');
        return;
      }

      await axios.post(
        'https://localhost:7098/api/wishlist', // Fixed endpoint (changed from whitelist to wishlist)
        { bookId: book.id },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      showNotification(`${book.title} saved to your wishlist!`);
    } catch (error) {
      console.error('Error adding to wishlist:', error);
      showNotification('Failed to add to wishlist.', 'error');
    }
  };

  const handleAddToCart = (book) => {
    try {
      // Using the cart context's addToCart function
      addToCart({
        id: book.id,
        title: book.title,
        author: book.authorName,
        price: book.price,
        format: book.format || 'Paperback',
        img: book.coverImageUrl,
        quantity: 1
      });
      
      showNotification(`${book.title} added to your cart!`);
    } catch (error) {
      console.error('Error adding to cart:', error);
      showNotification('Failed to add to cart. Please try again.', 'error');
    }
  };

  const filteredBooks = books
    .filter((book) =>
      book.title?.toLowerCase().includes(searchQuery.toLowerCase())
    )
    .sort((a, b) => {
      switch (sortBy) {
        case 'price':
          return parseFloat(a.price || 0) - parseFloat(b.price || 0);
        case 'rating':
          return parseFloat(b.rating || 0) - parseFloat(a.rating || 0);
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
        <div className="flex justify-center items-center h-64">
          <p>Loading books...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gray-50 p-4 md:p-8">
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
                      <div 
                        className="cursor-pointer"
                        onClick={() => {
                          window.location.href = `/bookdetails/${book.id}`;
                        }}
                      >
                        <img
                          src={`https://localhost:7098${book.coverImageUrl}`}
                          alt={book.title}
                          className="w-full h-48 object-contain mb-3"
                          onError={(e) => {
                            e.target.src = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCI+PHBhdGggZD0iTTIxIDVIM2ExIDEgMCAwMC0xIDF2MTJhMSAxIDAgMDAxIDFoMThhMSAxIDAgMDAxLTFWNmExIDEgMCAwMC0xLTF6bS0xIDJ2Mkg0VjdoMTZ6bTAgNHY4SDR2LThoMTZ6IiBmaWxsPSIjZWVlZWVlIi8+PC9zdmc+';
                          }}
                        />
                        <h3 className="font-semibold text-lg text-gray-800 truncate">{book.title}</h3>
                        <p className="text-sm text-gray-500 mb-1">by {book.authorName}</p>
                        <p className="text-xs text-gray-500 mb-1">{book.format} - {book.publisherName}</p>
                        <p className="text-xs text-gray-500 mb-1">Language: {book.language}</p>
                        <div className="mb-2">
                          <span className="text-lg font-bold text-[#0F4C81]">${book.price}</span>
                        </div>
                      </div>

                      <div className="flex justify-between items-center mt-2">
                        <button 
                          className="flex items-center gap-1 border px-3 py-1 rounded hover:bg-gray-100 text-sm"
                          onClick={(e) => {
                            e.stopPropagation();
                            addToWishlist(book);
                          }}
                        >
                          <FaHeart /> Save
                        </button>
                        <button
                          className={`flex items-center gap-1 px-3 py-1 rounded text-sm text-white ${
                            book.isAvailable
                              ? 'bg-[#0F4C81] hover:bg-[#0d3e6a]'
                              : 'bg-gray-400 cursor-not-allowed'
                          }`}
                          disabled={!book.isAvailable}
                          onClick={(e) => {
                            e.stopPropagation();
                            if (book.isAvailable) {
                              handleAddToCart(book);
                            }
                          }}
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
          </div>
        </div>
      </div>

      {/* Notification */}
      {notification.show && (
        <div className={`fixed bottom-4 right-4 px-4 py-2 rounded-md shadow-lg text-white ${
          notification.type === 'error' ? 'bg-red-500' : 'bg-green-500'
        }`}>
          {notification.message}
        </div>
      )}
    </div>
  );
};

export default BookCatalog;