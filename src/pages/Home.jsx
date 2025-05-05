import React from "react";
import Navbar from "../components/Navbar";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import Footer from "../components/footer";

const Home = () => {
  return (
    <div className="font-sans text-gray-800">
      <Navbar />
      
      {/* Hero Section */}
      <section className="bg-pink-100 py-10">
        <div className="container mx-auto px-4 text-center">
          <h2 className="text-3xl md:text-5xl font-bold mb-4">
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
              <img src="https://via.placeholder.com/400x600?text=Book+1" alt="Book 1" />
            </div>
            <div>
              <img src="https://via.placeholder.com/400x600?text=Book+2" alt="Book 2" />
            </div>
            <div>
              <img src="https://via.placeholder.com/400x600?text=Book+3" alt="Book 3" />
            </div>
          </Carousel>
          <button className="mt-6 px-6 py-2 bg-black text-white rounded">See More</button>
        </div>
      </section>

      {/* Featured Categories */}
      <section className="py-10">
        <div className="container mx-auto px-4">
          <h2 className="text-2xl font-semibold mb-6">Featured Categories</h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4">
            {['Arts & Photography', 'Food & Drink', 'Romance', 'Health', 'Biography'].map((category, idx) => (
              <div key={idx} className="bg-gray-100 p-4 rounded-md text-center">
                <div className="text-2xl mb-2">📚</div>
                <p className="font-medium">{category}</p>
                <p className="text-sm text-blue-500">Shop Now</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Bestselling Books */}
      <section className="py-10 bg-gray-50">
        <div className="container mx-auto px-4">
          <div className="flex justify-between items-center mb-6">
            <h2 className="text-2xl font-semibold">Bestselling Books</h2>
            <button className="text-blue-500 text-sm">View All</button>
          </div>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="bg-white p-4 rounded-md shadow">
                <div className="w-full h-40 bg-gray-200 rounded mb-2"></div>
                <p className="text-sm text-gray-600">Kindle</p>
                <p className="text-md font-medium">Sample Book Title</p>
                <p className="text-sm text-gray-700">$14.99</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Deals of the Week */}
      <section className="py-10">
        <div className="container mx-auto px-4">
          <div className="flex justify-between items-center mb-6">
            <h2 className="text-2xl font-semibold">Deals of the Week</h2>
            <button className="text-blue-500 text-sm">View All</button>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {[...Array(2)].map((_, i) => (
              <div key={i} className="flex items-center space-x-4 p-4 bg-pink-50 rounded-md">
                <div className="w-24 h-32 bg-gray-200 rounded"></div>
                <div>
                  <p className="text-sm text-gray-600">Paperback</p>
                  <p className="font-medium">Book Title Here</p>
                  <p className="text-red-500 font-bold">$2.00</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* New Releases */}
      <section className="py-10 bg-gray-50">
        <div className="container mx-auto px-4">
          <h2 className="text-2xl font-semibold mb-6">New Releases</h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="bg-white p-4 rounded-md shadow">
                <div className="w-full h-40 bg-gray-200 rounded mb-2"></div>
                <p className="text-sm text-gray-600">Hardcover</p>
                <p className="text-md font-medium">New Book Title</p>
                <p className="text-sm text-gray-700">$19.99</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Favorite Authors */}
      <section className="py-10">
        <div className="container mx-auto px-4">
          <h2 className="text-2xl font-semibold mb-6">Favorite Authors</h2>
          <div className="flex overflow-x-auto space-x-4 scrollbar-hide">
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