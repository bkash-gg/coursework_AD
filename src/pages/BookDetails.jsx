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

  const handleQuantityChange = (value) => {
    const newQuantity = quantity + value;
    if (newQuantity >= 1 && newQuantity <= 10) {
      setQuantity(newQuantity);
    }
  };

  const toggleWishlist = () => {
    setIsWishlisted(!isWishlisted);
  };

  const handleAddToCart = () => {
    if (book.stockQuantity <= 0) return;
    
    addToCart({
      id: book.id,
      title: book.title,
      author: book.authorName,
      price: book.price,
      format: book.format || 'Paperback',
      img: book.coverImageUrl,
      quantity: quantity
    });
    
    // You can replace this with a toast notification if you prefer
    alert(`${quantity} ${quantity > 1 ? 'copies' : 'copy'} of "${book.title}" added to cart!`);
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
        <div className="bg-white rounded-xl shadow-2xl overflow-hidden">
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
                  <button 
                    onClick={toggleWishlist}
                    className={`p-3 rounded-full shadow-md ${isWishlisted ? 'bg-red-100 text-red-500' : 'bg-white text-gray-400'} hover:shadow-lg transition-all`}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill={isWishlisted ? 'currentColor' : 'none'} viewBox="0 0 24 24" stroke="currentColor">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                    </svg>
                  </button>
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
                <span className="bg-green-100 text-green-800 text-xs font-semibold px-2.5 py-0.5 rounded-full">
                  {book.stockQuantity > 0 ? 'In Stock' : 'Out of Stock'}
                </span>
              </div>

              <div className="my-6">
                <span className="text-3xl font-bold text-gray-900">
                  {book.price ? `$${book.price.toFixed(2)}` : 'Price not available'}
                </span>
              </div>

              <p className="text-gray-700 leading-relaxed mb-6">{book.description}</p>

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
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.genre}</p>
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
                    disabled={quantity <= 1}
                  >
                    -
                  </button>
                  <span className="px-4 py-1 text-gray-900 font-medium">{quantity}</span>
                  <button 
                    onClick={() => handleQuantityChange(1)}
                    className="px-3 py-1 text-gray-600 hover:bg-gray-100 rounded-r-lg"
                    disabled={quantity >= 10}
                  >
                    +
                  </button>
                </div>

                <button 
                  className={`px-6 py-3 rounded-lg font-medium transition-all ${book.stockQuantity > 0 ? 
                    'bg-indigo-600 text-white hover:bg-indigo-700 shadow-md hover:shadow-lg' : 
                    'bg-gray-300 text-gray-600 cursor-not-allowed'}`}
                  disabled={book.stockQuantity <= 0}
                  onClick={handleAddToCart}
                >
                  {book.stockQuantity > 0 ? 'Add to Cart' : 'Out of Stock'}
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookDetails;