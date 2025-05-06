import React from "react";

const Footer = () => {
  return (
    <footer className="bg-gray-200 mt-auto py-10">
      <div className="container mx-auto px-4 text-center text-gray-700">
        <h4 className="text-xl font-semibold mb-2">BOOKWORM</h4>
        <div className="flex flex-col sm:flex-row justify-center gap-4 mb-4 text-sm">
          <a href="#" className="hover:underline">About Us</a>
          <a href="#" className="hover:underline">Contact</a>
          <a href="#" className="hover:underline">Privacy Policy</a>
          <a href="#" className="hover:underline">Terms of Service</a>
        </div>
        <p className="text-sm">&copy; 2025 BOOKWORM. All rights reserved.</p>
      </div>
    </footer>
  );
};

export default Footer;
