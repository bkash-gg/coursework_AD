import React from "react";

const Navbar = () => {
  return (
    <nav className="bg-white shadow-md">
      <div className="container mx-auto px-4 py-4 flex justify-between items-center">
        <h1 className="text-xl font-bold text-red-600">BOOKWORM</h1>
        <ul className="hidden md:flex space-x-6 text-gray-700">
          <li>Home</li>
          <li>Book Categories</li>
          <li>Shop</li>
          <li>Blog</li>
          <li>Pages</li>
        </ul>
        <input
          type="text"
          placeholder="Search"
          className="hidden md:block px-2 py-1 border rounded"
        />
      </div>
    </nav>
  );
};

export default Navbar;
