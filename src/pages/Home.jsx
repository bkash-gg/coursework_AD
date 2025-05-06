import React from "react";
import Navbar from "../components/Navbar";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import Footer from "../components/footer";
import { Link } from "react-router-dom";
import { FaCamera, FaUtensils, FaHeart, FaHeartbeat, FaBook } from "react-icons/fa";

const Home = () => {
  return (
    <div className="font-sans text-gray-800">
      <Navbar />
      
      {/* Hero Section */}
      <section 
  className="bg-cover bg-center py-16" 
  style={{ 
    backgroundImage: "url('/background.jpg')", 
    backgroundSize: 'cover', 
    backgroundPosition: 'center', 
    backgroundRepeat: 'no-repeat' 
  }}
>
  <div className="container mx-auto px-6 text-center">
    <h2 className="text-3xl md:text-5xl font-bold text-white mb-6">
      Featured Books of the <span className="text-black">February</span>
    </h2>
    <Carousel
      showThumbs={false}
      autoPlay
      infiniteLoop
      showStatus={false}
      className="max-w-4xl mx-auto"
    >
      <div>
        <img src="/book1.jpg" alt="Book 1" className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300" />
      </div>
      <div>
        <img src="/book2.png" alt="Book 2" className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300" />
      </div>
      <div>
        <img src="/book3.jpg" alt="Book 3" className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300" />
      </div>
    </Carousel>
    <Link to="/BookCatalog">
      <button className="mt-6 px-6 py-3 bg-black text-white rounded-lg transform hover:scale-105 transition-transform duration-300">
        See More
      </button>
    </Link>
  </div>
</section>

      {/* Featured Categories */}
      <section className="bg-white py-16">
        <div className="container mx-auto px-6 text-center">
          <h2 className="text-2xl font-semibold mb-8">Featured Categories</h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
            {[
              { name: 'Arts & Photography', icon: <FaCamera className="text-4xl mb-2 mx-auto text-pink-500" /> },
              { name: 'Food & Drink', icon: <FaUtensils className="text-4xl mb-2 mx-auto text-yellow-500" /> },
              { name: 'Romance', icon: <FaHeart className="text-4xl mb-2 mx-auto text-red-500" /> },
              { name: 'Health', icon: <FaHeartbeat className="text-4xl mb-2 mx-auto text-green-500" /> },
              { name: 'Biography', icon: <FaBook className="text-4xl mb-2 mx-auto text-blue-500" /> },
            ].map((category, idx) => (
              <div key={idx} className="bg-white p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-300 text-center">
                {category.icon}
                <p className="font-medium text-gray-800">{category.name}</p>
                <p className="text-sm text-blue-500 mt-2">Shop Now</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Bestselling Books */}
      <section className="bg-white py-16">
        <div className="container mx-auto px-6">
          <div className="flex justify-between items-center mb-8">
            <h2 className="text-2xl font-semibold text-gray-800">Bestselling Books</h2>
            <button className="text-blue-500 text-sm hover:underline">View All</button>
          </div>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="bg-white p-6 rounded-md shadow-lg hover:shadow-xl transition-shadow duration-300">
                <div className="w-full h-40 bg-gray-200 rounded mb-4"></div>
                <p className="text-sm text-gray-600">Kindle</p>
                <p className="text-md font-medium text-gray-800">Sample Book Title</p>
                <p className="text-sm text-gray-700">$14.99</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Deals of the Week */}
      <section className="bg-white py-16">
        <div className="container mx-auto px-6">
          <div className="flex justify-between items-center mb-8">
            <h2 className="text-2xl font-semibold text-gray-800">Deals of the Week</h2>
            <button className="text-blue-500 text-sm hover:underline">View All</button>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {[...Array(2)].map((_, i) => (
              <div key={i} className="flex items-center space-x-6 p-6 bg-white rounded-lg shadow-lg hover:shadow-xl transition-shadow duration-300">
                <div className="w-24 h-32 bg-gray-200 rounded-lg"></div>
                <div>
                  <p className="text-sm text-gray-600">Paperback</p>
                  <p className="font-medium text-gray-800">Book Title Here</p>
                  <p className="text-red-500 font-bold">$2.00</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* New Releases */}
      <section className="bg-white py-16">
        <div className="container mx-auto px-6">
          <h2 className="text-2xl font-semibold text-gray-800 mb-8">New Releases</h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="bg-white p-6 rounded-md shadow-lg hover:shadow-xl transition-shadow duration-300">
                <div className="w-full h-40 bg-gray-200 rounded mb-4"></div>
                <p className="text-sm text-gray-600">Hardcover</p>
                <p className="text-md font-medium text-gray-800">New Book Title</p>
                <p className="text-sm text-gray-700">$19.99</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Favorite Authors */}
      <section className="py-16 bg-white">
        <div className="container mx-auto px-6">
          <h2 className="text-2xl font-semibold text-gray-800 mb-8">Favorite Authors</h2>
          <div className="flex overflow-x-auto space-x-6 scrollbar-hide">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="flex-shrink-0 w-24 h-24 rounded-full bg-gray-300"></div>
            ))}
          </div>
        </div>
      </section>

      <Footer />
    </div>
  );
};

export default Home;
