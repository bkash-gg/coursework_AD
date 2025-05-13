import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import axios from 'axios';

const Wishlist = () => {
  const [wishlistItems, setWishlistItems] = useState([]);
  const [sortBy, setSortBy] = useState('default');
  const [showNotification, setShowNotification] = useState(false);

  // Fetch wishlist items
  useEffect(() => {
    axios.get('https://localhost:7098/api/wishlist')
      .then((response) => {
        setWishlistItems(response.data);
      })
      .catch((error) => {
        console.error('Error fetching wishlist items:', error);
      });
  }, []);

  const removeFromWishlist = (id) => {
    axios.delete(`https://localhost:7098/api/wishlist/${id}`)
      .then(() => {
        setWishlistItems((prevItems) => prevItems.filter(item => item.id !== id));
        setShowNotification(true);
        setTimeout(() => setShowNotification(false), 3000);
      })
      .catch((error) => {
        console.error('Error removing item from wishlist:', error);
      });
  };

  const sortedItems = [...wishlistItems].sort((a, b) => {
    if (sortBy === 'price-asc') return a.price - b.price;
    if (sortBy === 'price-desc') return b.price - a.price;
    if (sortBy === 'title') return a.title.localeCompare(b.title);
    if (sortBy === 'rating') return (b.rating || 0) - (a.rating || 0);
    return 0;
  });

  return (
    <div className="min-h-screen bg-gray-50 py-8 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-8">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">Your Wishlist</h1>
            <p className="text-gray-600 mt-2">
              {wishlistItems.length} {wishlistItems.length === 1 ? 'item' : 'items'}
            </p>
          </div>

          <div className="mt-4 md:mt-0">
            <select
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
              value={sortBy}
              onChange={(e) => setSortBy(e.target.value)}
            >
              <option value="default">Sort by: Default</option>
              <option value="price-asc">Price: Low to High</option>
              <option value="price-desc">Price: High to Low</option>
              <option value="title">Title</option>
              <option value="rating">Rating</option>
            </select>
          </div>
        </div>

        {wishlistItems.length === 0 ? (
          <div className="text-center py-12">
            <svg className="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2}
                d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
            </svg>
            <h3 className="mt-2 text-lg font-medium text-gray-900">Your wishlist is empty</h3>
            <p className="mt-1 text-gray-500">Start adding books you love!</p>
          </div>
        ) : (
          <AnimatePresence>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
              {sortedItems.map((item) => (
                <motion.div
                  key={item.id}
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, scale: 0.9 }}
                  transition={{ duration: 0.3 }}
                  className="group bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-300"
                >
                  <div className="relative">
                    <img
                      src={`https://localhost:7098${item.imageUrl}`}
                      alt={item.title}
                      className="w-full h-64 object-contain bg-gray-100"
                    />
                    <div
                      className="absolute top-2 right-2 bg-white rounded-full p-1 shadow cursor-pointer"
                      onClick={() => removeFromWishlist(item.id)}
                    >
                      <svg xmlns="http://www.w3.org/2000/svg"
                        className="h-6 w-6 text-red-500"
                        viewBox="0 0 20 20"
                        fill="currentColor">
                        <path fillRule="evenodd"
                          d="M3.172 5.172a4 4 0 015.656 0L10 6.343l1.172-1.171a4 4 0 115.656 5.656L10 17.657l-6.828-6.829a4 4 0 010-5.656z"
                          clipRule="evenodd" />
                      </svg>
                    </div>
                  </div>
                  <div className="p-4">
                    <h3 className="text-lg font-semibold text-gray-900 mb-1">{item.title}</h3>
                    <p className="text-sm text-gray-600 mb-1">by {item.author}</p>
                    <p className="text-sm text-gray-500">Rating: {item.rating || 0}/5</p>
                    <p className="text-md text-[#0F4C81] font-bold mt-2">${item.price}</p>
                  </div>
                </motion.div>
              ))}
            </div>
          </AnimatePresence>
        )}

        {/* Notification */}
        {showNotification && (
          <div className="fixed bottom-4 right-4 px-4 py-2 rounded-md bg-green-500 text-white shadow-lg">
            Removed from wishlist!
          </div>
        )}
      </div>
    </div>
  );
};

export default Wishlist;