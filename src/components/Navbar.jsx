import React from "react";
import { Link } from "react-router-dom";

const Navbar = () => {
  return (
    <nav className="bg-white shadow-md w-full">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex justify-between items-center">
        <h1 className="text-xl font-bold text-red-600">Book Hub</h1>

        <ul className="hidden md:flex space-x-6 text-gray-700">
          <li><Link to="/" className="hover:text-red-600">Home</Link></li>
          <li><Link to="/BookCatalog" className="hover:text-red-600">Book Catalog</Link></li>
          <li><Link to="/shop" className="hover:text-red-600">Shop</Link></li>
          <li><Link to="/blog" className="hover:text-red-600">Blog</Link></li>
          <li><Link to="/pages" className="hover:text-red-600">Pages</Link></li>
        </ul>

        <div className="hidden md:flex items-center space-x-4">
          <input
            type="text"
            placeholder="Search"
            className="px-3 py-1.5 border rounded-md focus:outline-none focus:ring-1 focus:ring-red-500"
          />
          <Link
            to="/login"
            className="px-4 py-1.5 border border-red-600 text-red-600 rounded-md hover:bg-red-50 transition-colors"
          >
            Login
          </Link>
          <Link
            to="/signup"
            className="px-4 py-1.5 bg-red-600 text-white rounded-md hover:bg-red-700 transition-colors"
          >
            Sign Up
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;