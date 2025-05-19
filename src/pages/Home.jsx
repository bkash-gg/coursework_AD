import React, { useState, useEffect } from "react";
import Navbar from "../components/Navbar";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import Footer from "../components/footer";
import { Link, useLocation } from "react-router-dom";
import {
  FaCamera,
  FaUtensils,
  FaHeart,
  FaHeartbeat,
  FaBook,
} from "react-icons/fa";

const Home = () => {
  const location = useLocation();
  const [successMessage, setSuccessMessage] = useState(null);

  useEffect(() => {
    if (location.state?.message && location.state?.type === 'success') {
      setSuccessMessage(location.state.message);
      // Clear the location state to prevent showing message on refresh
      window.history.replaceState({}, document.title);
      
      // Clear success message after 5 seconds
      const timer = setTimeout(() => {
        setSuccessMessage(null);
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [location]);

  return (
    <div className="font-sans text-gray-800 bg-gray-50 w-full overflow-x-hidden">
      {/* Success message display */}
      {successMessage && (
        <div className="fixed top-4 right-4 z-50 p-4 bg-green-100 text-green-700 rounded-lg shadow-lg animate-fade-in">
          {successMessage}
        </div>
      )}
      
      {/* Hero Section */}
      <section
        className="relative py-20 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] w-full"
        style={{
          backgroundImage: "url('/background.jpg')",
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
      >
        <div className="mx-auto text-center relative z-10">
          <h2 className="text-3xl md:text-5xl font-bold text-white mb-6">
            New Releases & Bestsellers
          </h2>
          <Carousel
            showThumbs={false}
            autoPlay
            infiniteLoop
            showStatus={false}
            className="max-w-4xl mx-auto"
          >
            <div>
              <img
                src="/book1.jpg"
                alt="Book 1"
                className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300"
              />
            </div>
            <div>
              <img
                src="/book2.png"
                alt="Book 2"
                className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300"
              />
            </div>
            <div>
              <img
                src="/book3.jpg"
                alt="Book 3"
                className="mx-auto max-h-72 w-auto object-contain rounded-lg shadow-lg transform hover:scale-105 transition-transform duration-300"
              />
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
      Featured Genres
    </h2>
    <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
      {[
        {
          name: "Arts & Photography",
          icon: <span className="text-4xl mb-2 mx-auto text-pink-500">📸</span>, // Camera Emoji
        },
        {
          name: "Food & Drink",
          icon: <span className="text-4xl mb-2 mx-auto text-yellow-500">🍽️</span>, // Food Emoji
        },
        {
          name: "Romance",
          icon: <span className="text-4xl mb-2 mx-auto text-red-500">❤️</span>, // Heart Emoji
        },
        {
          name: "Health",
          icon: <span className="text-4xl mb-2 mx-auto text-green-500">💪</span>, // Health Emoji
        },
        {
          name: "Biography",
          icon: <span className="text-4xl mb-2 mx-auto text-blue-500">📚</span>, // Book Emoji
        },
      ].map((category, idx) => (
        <div
          key={idx}
          className="bg-white p-6 rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 text-center group hover:bg-gradient-to-br from-white to-[#3B6CF7]/10 cursor-pointer"
        >
          <div className="inline-block p-4 rounded-full bg-[#3B6CF7]/10 group-hover:bg-[#3B6CF7]/20 transition-colors duration-300">
            {category.icon}
          </div>
          <p className="font-semibold text-gray-800 mt-4">
            {category.name}
          </p>
          <Link to="/BookCatalog">
            <p className="text-sm text-[#3B6CF7] mt-2 font-medium hover:underline">
              Shop Now
            </p>
          </Link>
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
      <Link to="/BookCatalog">
        <button className="text-[#3B6CF7] font-semibold hover:underline flex items-center">
          View All <span className="ml-2">→</span>
        </button>
      </Link>
    </div>
    <br />
    <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-6">
      {[
        { image: "book1.jpg", title: "The Lost Legend", genre: "Adventure" },
        { image: "bestseller1.jpg", title: "Echoes of Silence", genre: "Mystery" },
        { image: "bestseller2.jpg", title: "Whispers of the Past", genre: "Historical" },
        { image: "bestseller3.jpg", title: "Dreamwalker", genre: "Fantasy" },
        { image: "bestseller4.jpeg", title: "The Final Chapter", genre: "Thriller" }
      ].map((book, i) => (
        <div
          key={i}
          className="bg-white p-4 rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 relative group"
        >
          <div className="relative overflow-hidden rounded-lg">
            <img
              src={`/${book.image}`}
              alt={book.title}
              className="w-full h-48 object-cover rounded-lg transform transition-all duration-500 group-hover:scale-110"
            />
            <div className="absolute top-2 right-2 bg-[#3B6CF7] text-white px-3 py-1 rounded-full text-xs font-bold">
              Bestseller
            </div>
          </div>
          <div className="mt-4">
            <p className="text-sm text-gray-500">{book.genre}</p>
            <p className="font-semibold text-gray-800 mt-1">{book.title}</p>
            <div className="flex items-center justify-between mt-3">
              <p className="text-[#3B6CF7] font-bold">{book.price}</p>
              
            </div>
          </div>
        </div>
      ))}
    </div>
  </div>
</section>

{/* Top Publishers */}
<section className="py-16 bg-white">
  <div className="container mx-auto px-6">
    <h2 className="text-3xl font-bold mb-8 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
      Top Publishers
    </h2>
    <div className="flex overflow-x-auto pb-6 scrollbar-hide space-x-6">
      {[
        {
          name: "HarperCollins",
          img: "https://images.unsplash.com/photo-1605379399642-870262d3d051?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Penguin Random House", 
          img: "https://images.unsplash.com/photo-1553729459-efe14ef6055d?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Simon & Schuster",
          img: "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Macmillan Publishers",
          img: "https://images.unsplash.com/photo-1507842217343-583bb7270b66?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Hachette Book Group",
          img: "https://images.unsplash.com/photo-1553729459-efe14ef6055d?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Sandesh Hari",
          img: "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Bloomsbury",
          img: "https://images.unsplash.com/photo-1605379399642-870262d3d051?auto=format&fit=crop&w=600&q=60"
        },
        {
          name: "Scholastic Inc.",
          img: "https://images.unsplash.com/photo-1507842217343-583bb7270b66?auto=format&fit=crop&w=600&q=60"
        }
      ].map((publisher, i) => (
        <div
          key={i}
          className="flex-shrink-0 w-40 h-40 rounded-full shadow-lg hover:shadow-2xl transition-all duration-300 relative overflow-hidden cursor-pointer group"
          style={{
            backgroundImage: `url('${publisher.img}')`,
            backgroundSize: 'cover',
            backgroundPosition: 'center',
          }}
        >
          <div className="absolute inset-0 bg-black/60 rounded-full flex items-center justify-center">
            <p className="text-white font-semibold text-center text-sm px-4 group-hover:scale-105 group-hover:text-[#3B6CF7] transition-all duration-300">
              {publisher.name}
            </p>
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