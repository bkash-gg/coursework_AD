import React from "react";
import { Link } from "react-router-dom";
import { FaShoppingCart } from "react-icons/fa"; // Import the cart icon
import { useCart } from "../context/CartContext";

const Navbar = () => {
  const { getCartCount } = useCart();
  const cartCount = getCartCount();

  return (
    <nav className="bg-white shadow-md w-full">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex justify-between items-center">
        {/* Logo and Brand Name */}
        <h1 className="text-xl font-bold text-red-600 hover:text-red-700 transition-colors cursor-pointer">
        Quill and Byte
        </h1>

        {/* Desktop Navigation Menu */}
        <ul className="hidden md:flex space-x-8 text-gray-700">
          <li>
            <Link
              to="/"
              className="hover:text-red-600 text-lg font-medium transition-colors duration-300"
            >
              Home
            </Link>
          </li>
          <li>
            <Link
              to="/BookCatalog"
              className="hover:text-red-600 text-lg font-medium transition-colors duration-300"
            >
              Book Catalog
            </Link>
          </li>
          <li>
            <Link
              to="/wishlist"
              className="hover:text-red-600 text-lg font-medium transition-colors duration-300"
            >
              Wishlist
            </Link>
          </li>
          <li>
          </li>
        </ul>

        {/* Mobile and Desktop Cart, Login, and Signup Buttons */}
        <div className="hidden md:flex items-center space-x-4">
          <input
            type="text"
            placeholder="Search"
            className="px-3 py-1.5 border rounded-md focus:outline-none focus:ring-2 focus:ring-red-500 text-sm"
          />
          <Link
            to="/login"
            className="px-4 py-1.5 border border-red-600 text-red-600 rounded-md hover:bg-red-50 transition-colors text-sm font-medium"
          >
            Login
          </Link>
          <Link
            to="/signup"
            className="px-4 py-1.5 bg-red-600 text-white rounded-md hover:bg-red-700 transition-colors text-sm font-medium"
          >
            Sign Up
          </Link>

          {/* 🛒 Cart Icon */}
          <Link to="/Cart" className="text-red-600 hover:text-red-800 text-xl relative">
            <FaShoppingCart />
            {/* Cart count badge */}
            {cartCount > 0 && (
              <span className="absolute -top-2 -right-2 bg-red-500 text-white text-xs rounded-full h-5 w-5 flex items-center justify-center">
                {cartCount}
              </span>
            )}
          </Link>
        </div>
      </div>

      {/* Mobile Navigation (Hamburger Menu) */}
      <div className="md:hidden flex items-center justify-between">
        <div className="text-xl text-red-600">
          <Link to="/" className="font-bold">Book Hub</Link>
        </div>

        {/* Add mobile menu button logic if needed */}
      </div>
    </nav>
  );
};

export default Navbar;