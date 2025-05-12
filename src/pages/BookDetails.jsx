import React, { useState } from 'react';

const BookDetails = () => {
  const [quantity, setQuantity] = useState(1);
  const [isWishlisted, setIsWishlisted] = useState(false);
  
  // Static book data with enhanced details
  const book = {
    id: 1,
    title: 'The Great Gatsby',
    author: 'F. Scott Fitzgerald',
    price: 19.99,
    image: 'https://m.media-amazon.com/images/I/71FTb9X6wsL._AC_UF1000,1000_QL80_.jpg',
    description: 'The Great Gatsby is a 1925 novel by American writer F. Scott Fitzgerald. Set in the Jazz Age on Long Island, the novel depicts narrator Nick Carraway\'s interactions with mysterious millionaire Jay Gatsby and Gatsby\'s obsession to reunite with his former lover, Daisy Buchanan.',
    publishedDate: 'April 10, 1925',
    genre: 'Classic Fiction, Literary Fiction',
    isbn: '978-0743273565',
    pages: 180,
    language: 'English',
    rating: 4.5,
    reviews: 12543,
    publisher: 'Scribner',
    dimensions: '5.5 x 0.5 x 8.2 inches',
    stock: 15,
  };

  const handleQuantityChange = (value) => {
    const newQuantity = quantity + value;
    if (newQuantity >= 1 && newQuantity <= 10) {
      setQuantity(newQuantity);
    }
  };

  const toggleWishlist = () => {
    setIsWishlisted(!isWishlisted);
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 to-blue-50 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <div className="bg-white rounded-xl shadow-2xl overflow-hidden">
          <div className="flex flex-col lg:flex-row">
            {/* Book Cover Section */}
            <div className="lg:w-2/5 p-6 md:p-8 bg-gradient-to-br from-blue-100 to-indigo-100 flex flex-col items-center justify-center">
              <div className="relative w-full max-w-md">
                <img 
                  src={book.image} 
                  alt={book.title} 
                  className="w-full h-auto rounded-lg shadow-xl transform hover:scale-105 transition-transform duration-300"
                />
                <div className="absolute -top-3 -right-3">
                  <button 
                    onClick={toggleWishlist}
                    className={`p-3 rounded-full shadow-md ${isWishlisted ? 'bg-red-100 text-red-500' : 'bg-white text-gray-400'} hover:shadow-lg transition-all`}
                  >
                    <svg 
                      xmlns="http://www.w3.org/2000/svg" 
                      className="h-6 w-6" 
                      fill={isWishlisted ? "currentColor" : "none"} 
                      viewBox="0 0 24 24" 
                      stroke="currentColor"
                    >
                      <path 
                        strokeLinecap="round" 
                        strokeLinejoin="round" 
                        strokeWidth={2} 
                        d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" 
                      />
                    </svg>
                  </button>
                </div>
              </div>
              
              <div className="mt-6 flex items-center justify-center space-x-4">
                <div className="flex items-center bg-yellow-100 px-3 py-1 rounded-full">
                  {[...Array(5)].map((_, i) => (
                    <svg
                      key={i}
                      className={`w-5 h-5 ${i < Math.floor(book.rating) ? 'text-yellow-400' : 'text-gray-300'}`}
                      fill="currentColor"
                      viewBox="0 0 20 20"
                    >
                      <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z" />
                    </svg>
                  ))}
                  <span className="ml-1 text-sm font-medium text-gray-700">{book.rating} ({book.reviews.toLocaleString()} reviews)</span>
                </div>
              </div>
            </div>

            {/* Book Details Section */}
            <div className="lg:w-3/5 p-6 md:p-8">
              <div className="flex justify-between items-start">
                <div>
                  <h1 className="text-3xl md:text-4xl font-bold text-gray-900 mb-2">{book.title}</h1>
                  <p className="text-xl text-indigo-600 font-medium mb-4">by {book.author}</p>
                </div>
                <span className="bg-green-100 text-green-800 text-xs font-semibold px-2.5 py-0.5 rounded-full">
                  {book.stock > 0 ? 'In Stock' : 'Out of Stock'}
                </span>
              </div>

              <div className="my-6">
                <span className="text-3xl font-bold text-gray-900">${book.price.toFixed(2)}</span>
                {book.stock > 0 && (
                  <span className="ml-2 text-sm text-green-600">+ ${(book.price * 0.1).toFixed(2)} shipping</span>
                )}
              </div>

              <p className="text-gray-700 leading-relaxed mb-6">{book.description}</p>

              {/* Book Metadata */}
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 mb-8">
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Publisher</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.publisher}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Published Date</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.publishedDate}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Genre</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.genre}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Pages</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.pages}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">Language</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.language}</p>
                </div>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h3 className="text-sm font-medium text-gray-500">ISBN</h3>
                  <p className="mt-1 text-sm font-medium text-gray-900">{book.isbn}</p>
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
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                      <path fillRule="evenodd" d="M5 10a1 1 0 011-1h8a1 1 0 110 2H6a1 1 0 01-1-1z" clipRule="evenodd" />
                    </svg>
                  </button>
                  <span className="px-4 py-1 text-gray-900 font-medium">{quantity}</span>
                  <button 
                    onClick={() => handleQuantityChange(1)}
                    className="px-3 py-1 text-gray-600 hover:bg-gray-100 rounded-r-lg"
                    disabled={quantity >= 10}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                      <path fillRule="evenodd" d="M10 5a1 1 0 011 1v3h3a1 1 0 110 2h-3v3a1 1 0 11-2 0v-3H6a1 1 0 110-2h3V6a1 1 0 011-1z" clipRule="evenodd" />
                    </svg>
                  </button>
                </div>

                <button 
                  className={`px-6 py-3 rounded-lg font-medium transition-all ${book.stock > 0 ? 
                    'bg-indigo-600 text-white hover:bg-indigo-700 shadow-md hover:shadow-lg' : 
                    'bg-gray-300 text-gray-600 cursor-not-allowed'}`}
                  disabled={book.stock <= 0}
                >
                  {book.stock > 0 ? 'Add to Cart' : 'Out of Stock'}
                </button>

                <button className="px-6 py-3 border border-indigo-600 text-indigo-600 rounded-lg font-medium hover:bg-indigo-50 transition-colors">
                  Buy Now
                </button>
              </div>
            </div>
          </div>
        </div>

        {/* Additional Sections */}
        <div className="mt-12 bg-white rounded-xl shadow-2xl overflow-hidden p-6 md:p-8">
          <h2 className="text-2xl font-bold text-gray-900 mb-6">About the Author</h2>
          <div className="flex flex-col md:flex-row items-start gap-6">
            <img 
              src="https://upload.wikimedia.org/wikipedia/commons/5/5c/F._Scott_Fitzgerald_1921.jpg" 
              alt="F. Scott Fitzgerald" 
              className="w-32 h-32 rounded-full object-cover border-4 border-indigo-100"
            />
            <div>
              <h3 className="text-xl font-semibold text-gray-800">F. Scott Fitzgerald</h3>
              <p className="text-gray-600 mt-1">American novelist and short story writer</p>
              <p className="text-gray-700 mt-4 leading-relaxed">
                Francis Scott Key Fitzgerald (September 24, 1896 – December 21, 1940) was an American novelist, 
                essayist, and short story writer. He is best known for his novels depicting the flamboyance and 
                excess of the Jazz Age—a term he popularized. During his lifetime, he published four novels, 
                four story collections, and 164 short stories.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookDetails;