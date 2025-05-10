import React from "react";

const Footer = () => {
  return (
    <footer className="bg-gray-200 w-full py-10">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center text-gray-700">
        <h4 className="text-xl font-semibold mb-4">BOOKWORM</h4>
        <div className="flex flex-col sm:flex-row justify-center gap-6 mb-6 text-sm">
          <a href="#" className="hover:text-red-600 hover:underline transition-colors">About Us</a>
          <a href="#" className="hover:text-red-600 hover:underline transition-colors">Contact</a>
          <a href="#" className="hover:text-red-600 hover:underline transition-colors">Privacy Policy</a>
          <a href="#" className="hover:text-red-600 hover:underline transition-colors">Terms of Service</a>
        </div>
        <p className="text-sm">&copy; 2025 BOOKWORM. All rights reserved.</p>
      </div>
    </footer>
  );
};

export default Footer;