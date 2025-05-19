import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { useCart } from '../context/CartContext';

const BookDetails = () => {
  const { id } = useParams();
  const [book, setBook] = useState(null);
  const [quantity, setQuantity] = useState(1);
  const [isWishlisted, setIsWishlisted] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isAddingToCart, setIsAddingToCart] = useState(false);
  const [cartError, setCartError] = useState(null);
  const { addToCart } = useCart();

  useEffect(() => {
    const fetchBookDetails = async () => {
      if (!id) {
        setError("No book ID found in URL");
        setLoading(false);
        return;
      }

      try {
        const response = await axios.get(`https://localhost:7098/api/books/${id}`);
        if (response.data.success) {
          setBook(response.data.data);
          // Check if book is in wishlist
          checkWishlistStatus(response.data.data.id);
        } else {
          setError(response.data.message || 'Failed to fetch book details');
        }
      } catch (error) {
        console.error('Error fetching book details:', error);
        setError(error.message || 'Failed to connect to the server.');
      } finally {
        setLoading(false);
      }
    };

    fetchBookDetails();
  }, [id]);

  const checkWishlistStatus = async (bookId) => {
    try {
      const token = localStorage.getItem('token');
      if (!token) return;

      const response = await axios.get('https://localhost:7098/api/whitelist', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      if (response.data.success) {
        const isInWishlist = response.data.data.some(item => item.bookId === bookId);
        setIsWishlisted(isInWishlist);
      }
    } catch (error) {
      console.error('Error checking wishlist status:', error);
    }
  };

  const handleQuantityChange = (value) => {
    const newQuantity = quantity + value;
    if (newQuantity >= 1 && newQuantity <= 10) {
      setQuantity(newQuantity);
    }
  };

  const toggleWishlist = async () => {
    try {
      const token = localStorage.getItem('token');
      if (!token) {
        showNotification('You must be logged in to save books.', 'error');
        return;
      }

      const userId = localStorage.getItem('userId');
      if (!userId) {
        showNotification('User information not found. Please log in again.', 'error');
        return;
      }

      if (!isWishlisted) {
        await axios.post(
          'https://localhost:7098/api/whitelist',
          {
            userId: userId,
            bookId: book.id
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setIsWishlisted(true);
        showNotification(`${book.title} added to your wishlist!`);
      } else {
        // If you have an endpoint to remove from wishlist, implement it here
        // For now, we'll just toggle the UI state
        setIsWishlisted(false);
        showNotification(`${book.title} removed from your wishlist!`);
      }
    } catch (error) {
      console.error('Error toggling wishlist:', error);
      if (error.response?.data?.message === "This book is already in your whitelist.") {
        showNotification('This book is already in your wishlist.', 'error');
        setIsWishlisted(true); // Update UI to reflect that the book is in wishlist
      } else if (error.response?.data?.message === "User not found.") {
        showNotification('User not found. Please log in again.', 'error');
      } else if (error.response?.data?.message === "Book not found.") {
        showNotification('Book not found.', 'error');
      } else {
        showNotification('Failed to update wishlist.', 'error');
      }
    }
  };

  const showNotification = (message, type = 'success') => {
    // You can implement a notification system here
    console.log(message);
  };

  const handleAddToCart = async () => {
    if (book.stockQuantity <= 0) return;
    
    try {
      setIsAddingToCart(true);
      setCartError(null);
      
      await addToCart({
        id: book.id,
        title: book.title,
        author: book.authorName,
        price: book.price,
        format: book.format || 'Paperback',
        img: book.coverImageUrl,
        quantity: quantity,
        discountPercentage: book.discountPercentage || 0,
        originalPrice: book.price
      });
      
      // Show success message
    } catch (error) {
      console.error('Error adding to cart:', error);
      setCartError(error.message || 'Failed to add to cart. Please try again.');
    } finally {
      setIsAddingToCart(false);
    }
  };

  const calculateDiscountedPrice = () => {
    if (!book.discountPercentage) return book.price;
    return book.price * (1 - book.discountPercentage / 100);
  };

  if (loading) {
    return <div className="min-h-screen flex items-center justify-center bg-gray-50">Loading...</div>;
  }

  if (error) {
    return <div className="min-h-screen flex items-center justify-center bg-gray-50">Error: {error}</div>;
  }

  if (!book) {
    return <div className="min-h-screen flex items-center justify-center bg-gray-50">No book found</div>;
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 to-blue-50 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <div className="bg-white rounded-xl shadow-2xl overflow-hidden mb-8">
          <div className="flex flex-col lg:flex-row">
            {/* Book Cover Section */}
            <div className="lg:w-2/5 p-6 md:p-8 bg-gradient-to-br from-blue-100 to-indigo-100 flex flex-col items-center justify-center">
              <div className="relative w-full max-w-md">
                <img 
                  src={`https://localhost:7098${book.coverImageUrl}`} 
                  alt={book.title} 
                  className="w-full h-auto rounded-lg shadow-xl transform hover:scale-105 transition-transform duration-300"
                  onError={(e) => {
                    e.target.src = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCI+PHBhdGggZD0iTTIxIDVIM2ExIDEgMCAwMC0xIDF2MTJhMSAxIDAgMDAxIDFoMThhMSAxIDAgMDAxLTFWNmExIDEgMCAwMC0xLTF6bS0xIDJ2Mkg0VjdoMTZ6bTAgNHY4SDR2LThoMTZ6IiBmaWxsPSIjZWVlZWVlIi8+PC9zdmc+';
                  }}
                />
                <div className="absolute -top-3 -right-3">
                  <div 
                    className={`p-3 rounded-full shadow-md ${isWishlisted ? 'bg-red-100 text-red-500' : 'bg-white text-gray-400'}`}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill={isWishlisted ? 'currentColor' : 'none'} viewBox="0 0 24 24" stroke="currentColor">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                    </svg>
                  </div>
                </div>
                
                {/* Special Badges */}
                <div className="mt-4 flex flex-wrap gap-2">
                  {book.isAwardWinner && (
                    <div className="bg-yellow-100 text-yellow-800 text-xs font-medium px-2.5 py-0.5 rounded-full flex items-center">
                      <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 3v4M3 5h4M6 17v4m-2-2h4m5-16l2.286 6.857L21 12l-5.714 2.143L13 21l-2.286-6.857L5 12l5.714-2.143L13 3z" />
                      </svg>
                      Award Winner
                    </div>
                  )}
                  {book.isComingSoon && (
                    <div className="bg-purple-100 text-purple-800 text-xs font-medium px-2.5 py-0.5 rounded-full flex items-center">
                      <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                      </svg>
                      Coming Soon
                    </div>
                  )}
                  {book.discountPercentage > 0 && (
                    <div className="bg-red-100 text-red-800 text-xs font-medium px-2.5 py-0.5 rounded-full flex items-center">
                      <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                      </svg>
                      {book.discountPercentage}% Off
                    </div>
                  )}
                </div>
              </div>
            </div>

            {/* Book Details Section */}
            <div className="lg:w-3/5 p-6 md:p-8">
              <div className="flex justify-between items-start">
                <div>
                  <h1 className="text-3xl md:text-4xl font-bold text-gray-900 mb-2">{book.title}</h1>
                  <p className="text-xl text-indigo-600 font-medium mb-4">by {book.authorName}</p>
                </div>
                <span className={`text-xs font-semibold px-2.5 py-0.5 rounded-full ${book.stockQuantity > 0 ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'}`}>
                  {book.stockQuantity > 0 ? 'In Stock' : 'Out of Stock'}
                </span>
              </div>

              <div className="my-6 flex items-center">
                {book.discountPercentage > 0 ? (
                  <>
                    <span className="text-3xl font-bold text-gray-900 mr-2">
                      ${calculateDiscountedPrice().toFixed(2)}
                    </span>
                    <span className="text-xl text-gray-500 line-through">
                      ${book.price.toFixed(2)}
                    </span>
                  </>
                ) : (
                  <span className="text-3xl font-bold text-gray-900">
                    {book.price ? `$${book.price.toFixed(2)}` : 'Price not available'}
                  </span>
                )}
              </div>

              <p className="text-gray-700 leading-relaxed mb-6">{book.description}</p>

              {/* ISBN and Format */}
              <div className="flex flex-wrap gap-4 mb-4">
                <div className="bg-blue-50 px-3 py-1 rounded-full text-sm font-medium text-blue-700">
                  ISBN: {book.isbn}
                </div>
                <div className="bg-indigo-50 px-3 py-1 rounded-full text-sm font-medium text-indigo-700">
                  Format: {book.format}
                </div>
              </div>

              {/* Book Metadata */}
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 mb-8">
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Publisher</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.publisherName}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Published Date</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">
                    {new Date(book.publicationDate).toLocaleDateString()}
                  </p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Genre</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">
                    {book.genres && book.genres.join(', ')}
                  </p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Language</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.language}</p>
                </div>
              </div>

              {/* Action Buttons */}
              <div className="flex flex-col sm:flex-row items-start sm:items-center space-y-4 sm:space-y-0 sm:space-x-4">
                <div className="flex items-center border border-gray-300 rounded-lg">
                  <button 
                    onClick={() => handleQuantityChange(-1)}
                    className="px-3 py-1 text-gray-600 hover:bg-gray-100 rounded-l-lg"
                    disabled={quantity <= 1 || isAddingToCart}
                  >
                    -
                  </button>
                  <span className="px-4 py-1 text-gray-900 font-medium">{quantity}</span>
                  <button 
                    onClick={() => handleQuantityChange(1)}
                    className="px-3 py-1 text-gray-600 hover:bg-gray-100 rounded-r-lg"
                    disabled={quantity >= 10 || quantity >= book.stockQuantity || isAddingToCart}
                  >
                    +
                  </button>
                </div>

                <button 
                  className={`px-6 py-3 rounded-lg font-medium transition-all ${
                    book.stockQuantity > 0 
                      ? isAddingToCart
                        ? 'bg-gray-400 cursor-wait'
                        : 'bg-indigo-600 text-white hover:bg-indigo-700 shadow-md hover:shadow-lg'
                      : 'bg-gray-300 text-gray-600 cursor-not-allowed'
                  }`}
                  disabled={book.stockQuantity <= 0 || isAddingToCart}
                  onClick={handleAddToCart}
                >
                  {isAddingToCart ? 'Adding...' : book.stockQuantity > 0 ? 'Add to Cart' : 'Out of Stock'}
                </button>
              </div>

              {cartError && (
                <div className="mt-4 p-3 bg-red-100 text-red-700 rounded-lg">
                  {cartError}
                </div>
              )}
            </div>
          </div>
        </div>

        {/* Additional Details Section */}
        <div className="bg-white rounded-xl shadow-lg overflow-hidden mb-8">
          <div className="border-b border-gray-200">
            <div className="p-6">
              <h2 className="text-2xl font-bold text-gray-900 mb-6">Additional Details</h2>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <h3 className="text-lg font-semibold text-gray-800 mb-2">Book Information</h3>
                  <ul className="space-y-2 text-gray-600">
                    <li><span className="font-medium">ID:</span> {book.id}</li>
                    <li><span className="font-medium">ISBN:</span> {book.isbn}</li>
                    <li><span className="font-medium">Format:</span> {book.format}</li>
                    <li><span className="font-medium">Language:</span> {book.language}</li>
                    <li><span className="font-medium">Stock:</span> {book.stockQuantity} copies</li>
                    <li><span className="font-medium">Available:</span> {book.isAvailable ? 'Yes' : 'No'}</li>
                  </ul>
                </div>
                <div>
                  <h3 className="text-lg font-semibold text-gray-800 mb-2">Publication Details</h3>
                  <ul className="space-y-2 text-gray-600">
                    <li><span className="font-medium">Publisher:</span> {book.publisherName}</li>
                    <li><span className="font-medium">Author:</span> {book.authorName}</li>
                    <li><span className="font-medium">Publication Date:</span> {new Date(book.publicationDate).toLocaleDateString()}</li>
                    <li><span className="font-medium">Genre:</span> {book.genres && book.genres.join(', ')}</li>
                    <li><span className="font-medium">Award Winner:</span> {book.isAwardWinner ? 'Yes' : 'No'}</li>
                    <li><span className="font-medium">Added to System:</span> {new Date(book.createdAt).toLocaleDateString()}</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Reviews Section */}
        <div className="bg-white rounded-xl shadow-lg overflow-hidden">
          <div className="p-6">
            <div className="mb-6">
              <h2 className="text-2xl font-bold text-gray-900">Reviews</h2>
            </div>
            
            {book.reviews && book.reviews.length > 0 ? (
              <div className="space-y-6">
                {book.reviews.map((review, index) => (
                  <div key={index} className="border-b border-gray-200 pb-6 last:border-b-0">
                    <div className="flex items-center mb-2">
                      <div className="flex mr-2">
                        {[...Array(5)].map((_, i) => (
                          <svg 
                            key={i} 
                            xmlns="http://www.w3.org/2000/svg" 
                            className="h-5 w-5" 
                            fill={i < review.rating ? 'gold' : 'none'} 
                            viewBox="0 0 24 24" 
                            stroke="currentColor"
                          >
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M11.049 2.927c.3-.921 1.603-.921 1.902 0l1.519 4.674a1 1 0 00.95.69h4.915c.969 0 1.371 1.24.588 1.81l-3.976 2.888a1 1 0 00-.363 1.118l1.518 4.674c.3.922-.755 1.688-1.538 1.118l-3.976-2.888a1 1 0 00-1.176 0l-3.976 2.888c-.783.57-1.838-.197-1.538-1.118l1.518-4.674a1 1 0 00-.363-1.118l-3.976-2.888c-.784-.57-.38-1.81.588-1.81h4.914a1 1 0 00.951-.69l1.519-4.674z" />
                          </svg>
                        ))}
                      </div>
                      <span className="text-gray-600">{review.userName}</span>
                    </div>
                    <p className="text-gray-700">{review.text}</p>
                    <p className="text-gray-500 text-sm mt-2">{new Date(review.createdAt).toLocaleDateString()}</p>
                  </div>
                ))}
              </div>
            ) : (
              <div className="flex flex-col items-center justify-center py-12 bg-gray-50 rounded-lg">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-12 w-12 text-gray-400 mb-3" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 8h10M7 12h4m1 8l-4-4H5a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v8a2 2 0 01-2 2h-3l-4 4z" />
                </svg>
                <p className="text-gray-600 text-lg mb-1">No reviews yet</p>
                <p className="text-gray-500">Be the first to share your thoughts on this book!</p>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookDetails;