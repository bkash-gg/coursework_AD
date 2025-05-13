import React from "react";
import Navbar from "../components/Navbar";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import Footer from "../components/footer";
import { Link } from "react-router-dom";
import { FaCamera, FaUtensils, FaHeart, FaHeartbeat, FaBook } from "react-icons/fa";

const Home = () => {
  return (
    <div className="font-sans text-gray-800 bg-gray-50 w-full overflow-x-hidden">
      <Navbar />
      
      {/* Hero Section */}
      <section 
        className="relative py-20 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] w-full" 
        style={{ 
          backgroundImage: "url('/background.jpg')", 
          backgroundSize: 'cover', 
          backgroundPosition: 'center', 
          backgroundRepeat: 'no-repeat' 
        }}
      >
        <div className="mx-auto text-center relative z-10">
          <h2 className="text-3xl md:text-5xl font-bold text-white mb-6">
            New Realeases & Bestsellers
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
       <section className="py-16 bg-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
          <h2 className="text-3xl font-bold mb-12 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
            Top Genere
          </h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
            {[
              { name: 'Arts & Photography', icon: <FaCamera className="text-4xl mb-2 mx-auto text-pink-500" /> },
              { name: 'Food & Drink', icon: <FaUtensils className="text-4xl mb-2 mx-auto text-yellow-500" /> },
              { name: 'Romance', icon: <FaHeart className="text-4xl mb-2 mx-auto text-red-500" /> },
              { name: 'Health', icon: <FaHeartbeat className="text-4xl mb-2 mx-auto text-green-500" /> },
              { name: 'Biography', icon: <FaBook className="text-4xl mb-2 mx-auto text-blue-500" /> },
            ].map((category, idx) => (
              <div 
                key={idx} 
                className="bg-white p-6 rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 text-center group hover:bg-gradient-to-br from-white to-[#3B6CF7]/10 cursor-pointer"
              >
                <div className="inline-block p-4 rounded-full bg-[#3B6CF7]/10 group-hover:bg-[#3B6CF7]/20 transition-colors duration-300">
                  {category.icon}
                </div>
                <p className="font-semibold text-gray-800 mt-4">{category.name}</p>
                <p className="text-sm text-[#3B6CF7] mt-2 font-medium hover:underline">Shop Now</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Bestselling Books */}
      <section className="py-16 bg-white">
        <div className="container mx-auto px-6">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
            <h2 className="text-3xl font-bold bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
              Top sellers
            </h2>
          </div>
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex justify-end">
            <button className="text-[#3B6CF7] font-semibold hover:underline flex items-center">
              View All <span className="ml-2">→</span>
            </button>
          </div>
          <br />
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
            {[...Array(5)].map((_, i) => (
              <div 
                key={i} 
                className="bg-white p-4 rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 relative group"
              >
                <div className="relative overflow-hidden rounded-lg">
                  <img 
                    src={`/book${i+1}.jpg`} 
                    alt={`Book ${i+1}`} 
                    className="w-full h-48 object-cover rounded-lg transform transition-all duration-500 group-hover:scale-110"
                  />
                  <div className="absolute top-2 right-2 bg-[#3B6CF7] text-white px-3 py-1 rounded-full text-xs font-bold">
                    Bestseller
                  </div>
                </div>
                <div className="mt-4">
                  <p className="text-sm text-gray-500">Fiction</p>
                  <p className="font-semibold text-gray-800 mt-1">The Great Novel</p>
                  <div className="flex items-center justify-between mt-3">
                    <p className="text-[#3B6CF7] font-bold">$14.99</p>
                    <button className="px-3 py-1 bg-[#3B6CF7]/10 text-[#3B6CF7] rounded-full hover:bg-[#3B6CF7]/20 transition-colors duration-300">
                      Add to Cart
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Favorite Authors */}
      <section className="py-16 bg-white">
        <div className="container mx-auto px-6">
          <h2 className="text-3xl font-bold mb-8 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
            Top Publishers
          </h2>
          <div className="flex overflow-x-auto pb-6 scrollbar-hide">
            {[...Array(5)].map((_, i) => (
              <div 
                key={i} 
                className="flex-shrink-0 w-48 mx-4 group relative cursor-pointer"
              >
                <div className="w-48 h-48 rounded-full overflow-hidden shadow-lg transform transition-all duration-500 group-hover:scale-105">
                  <img 
                    src={`/author-${i+1}.jpg`} 
                    className="w-full h-full object-cover"
                  />
                </div>
                <div className="absolute inset-0 rounded-full bg-black/50 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                  <p className="text-white font-semibold text-center">Jane Smith<br/><span className="text-sm">Bestselling Author</span></p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      <Footer />
    </div>
  );
};

export default Home;
