import React from "react";
import { Link } from "react-router-dom";

const Navbar = () => {
  return (
    <nav className="bg-white shadow-md">
      <div className="container mx-auto px-4 py-4 flex justify-between items-center">
        <h1 className="text-xl font-bold text-red-600">BOOKWORM</h1>

        <ul className="hidden md:flex space-x-6 text-gray-700">
          <li><Link to="/">Home</Link></li>
          <li><Link to="/book-catalog">Book Catalog</Link></li>
          <li><Link to="/shop">Shop</Link></li>
          <li><Link to="/blog">Blog</Link></li>
          <li><Link to="/pages">Pages</Link></li>
        </ul>

        <div className="hidden md:flex items-center space-x-4">
          <input
            type="text"
            placeholder="Search"
            className="px-2 py-1 border rounded"
          />
          <Link
            to="/login"
            className="px-3 py-1 border border-red-600 text-red-600 rounded hover:bg-red-100"
          >
            Login
          </Link>
          <Link
            to="/signup"
            className="px-3 py-1 bg-red-600 text-white rounded hover:bg-red-700"
          >
            Sign Up
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
