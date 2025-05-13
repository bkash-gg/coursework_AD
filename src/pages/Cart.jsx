import React from 'react';
import { Link } from 'react-router-dom';
import { FaTrash, FaArrowLeft, FaCreditCard } from 'react-icons/fa';
import Navbar from '../components/Navbar';
import { useCart } from '../context/CartContext';

const Cart = () => {
  const { 
    cartItems, 
    removeFromCart, 
    updateQuantity, 
    clearCart, 
    getTotalPrice,
    getCartCount 
  } = useCart();

  if (cartItems.length === 0) {
    return (
      <div className="min-h-screen bg-gray-50">
        <div className="max-w-5xl mx-auto py-12 px-4">
          <h1 className="text-3xl font-bold text-gray-800 mb-8">Your Cart</h1>
          <div className="bg-white rounded-lg shadow-md p-8 text-center">
            <img 
              src="/empty-cart.svg" 
              alt="Empty Cart" 
              className="w-64 h-64 mx-auto mb-6"
              onError={(e) => e.target.src = "https://via.placeholder.com/250x250?text=Empty+Cart"}
            />
            <h2 className="text-2xl font-semibold text-gray-700 mb-4">Your cart is empty</h2>
            <p className="text-gray-500 mb-8">Looks like you haven't added any books to your cart yet.</p>
            <Link 
              to="/BookCatalog" 
              className="inline-flex items-center px-6 py-3 bg-red-600 text-white font-medium rounded-md hover:bg-red-700"
            >
              <FaArrowLeft className="mr-2" /> Continue Shopping
            </Link>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <div className="max-w-6xl mx-auto py-8 px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-8">Your Cart ({getCartCount()} items)</h1>
        
        <div className="flex flex-col lg:flex-row gap-8">
          {/* Cart Items */}
          <div className="lg:w-2/3">
            <div className="bg-white rounded-lg shadow-md overflow-hidden">
              <div className="p-6 border-b">
                <div className="flex justify-between items-center">
                  <h2 className="text-xl font-semibold text-gray-800">Shopping Cart</h2>
                  <button 
                    onClick={clearCart}
                    className="text-sm text-red-600 hover:text-red-800 flex items-center"
                  >
                    <FaTrash className="mr-1" /> Clear Cart
                  </button>
                </div>
              </div>
              
              <div className="divide-y">
                {cartItems.map((item) => (
                  <div key={item.id} className="p-6 flex flex-col sm:flex-row items-start sm:items-center">
                    <img 
                      src={item.img} 
                      alt={item.title} 
                      className="w-24 h-24 object-contain rounded mb-4 sm:mb-0 sm:mr-6"
                      onError={(e) => e.target.src = "https://via.placeholder.com/150"}
                    />
                    
                    <div className="flex-1">
                      <h3 className="font-semibold text-gray-800">{item.title}</h3>
                      <p className="text-sm text-gray-500 mb-1">by {item.author}</p>
                      <p className="text-sm text-gray-500">{item.format}</p>
                      <div className="flex items-center justify-between mt-4">
                        <div className="flex items-center border rounded">
                          <button 
                            onClick={() => updateQuantity(item.id, item.quantity - 1)}
                            className="px-3 py-1 text-gray-600 hover:bg-gray-100"
                          >
                            -
                          </button>
                          <span className="px-3 py-1 border-x">{item.quantity}</span>
                          <button 
                            onClick={() => updateQuantity(item.id, item.quantity + 1)}
                            className="px-3 py-1 text-gray-600 hover:bg-gray-100"
                          >
                            +
                          </button>
                        </div>
                        <div>
                          <span className="font-medium text-lg text-gray-800">${(item.price * item.quantity).toFixed(2)}</span>
                          <button 
                            onClick={() => removeFromCart(item.id)}
                            className="ml-4 text-red-600 hover:text-red-800"
                          >
                            <FaTrash />
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
            
            <div className="mt-6">
              <Link 
                to="/BookCatalog" 
                className="inline-flex items-center text-red-600 hover:text-red-800"
              >
                <FaArrowLeft className="mr-2" /> Continue Shopping
              </Link>
            </div>
          </div>
          
          {/* Order Summary */}
          <div className="lg:w-1/3">
            <div className="bg-white rounded-lg shadow-md p-6">
              <h2 className="text-xl font-semibold text-gray-800 mb-6">Order Summary</h2>
              
              <div className="flex justify-between mb-2">
                <span className="text-gray-600">Subtotal</span>
                <span className="font-medium">${getTotalPrice()}</span>
              </div>
              <div className="flex justify-between mb-2">
                <span className="text-gray-600">Shipping</span>
                <span className="font-medium">$0.00</span>
              </div>
              <div className="flex justify-between mb-2">
                <span className="text-gray-600">Tax</span>
                <span className="font-medium">$0.00</span>
              </div>
              
              <div className="border-t my-4"></div>
              
              <div className="flex justify-between mb-6">
                <span className="text-lg font-semibold">Total</span>
                <span className="text-lg text-red-600 font-bold">${getTotalPrice()}</span>
              </div>
              
              <button className="w-full bg-red-600 text-white py-3 px-4 rounded-md hover:bg-red-700 flex items-center justify-center font-medium">
                <FaCreditCard className="mr-2" /> Proceed to Checkout
              </button>
              
              <div className="mt-4 text-xs text-gray-500 text-center">
                Secure checkout powered by Stripe
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Cart; 