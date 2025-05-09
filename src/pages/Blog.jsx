import React, { useState } from "react";
import Navbar from "../components/Navbar";
import { motion } from 'framer-motion';
import { ChevronLeft, ChevronRight, Search } from "lucide-react";
import BlogPost from "./BlogPost";
import Button from "../components/Button"; // ✅ Default import

const Blog = () => {
    const [searchQuery, setSearchQuery] = useState("");
    const [currentPage, setCurrentPage] = useState(1);

    const blogs = Array.from({ length: 15 }, (_, i) => ({
        id: i + 1,
        title: `Blog Post ${i + 1}`,
        excerpt: "This is a short excerpt from the blog content...",
        date: "2025-05-01",
        image: "/book3.jpg",
    }));

    const blogsPerPage = 6;
    const totalPages = Math.ceil(blogs.length / blogsPerPage);

    const filteredBlogs = blogs.filter(blog =>
        blog.title.toLowerCase().includes(searchQuery.toLowerCase())
    );

    const displayedBlogs = filteredBlogs.slice(
        (currentPage - 1) * blogsPerPage,
        currentPage * blogsPerPage
    );

    const handlePageChange = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
        }
    };

    return (
        <div className="bg-gray-50 min-h-screen">
            <Navbar />
            <div className='max-w-7xl mx-auto px-4 py-8'>
                {/* Header */}
                <div className="bg-white p-6 rounded-xl shadow-sm mb-8">
                    <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                        <h1 className="text-3xl font-bold text-gray-900">Blog Posts</h1>
                        <div className="relative w-full md:w-96">
                            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
                            <input
                                type="text"
                                className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                                placeholder="Search blogs..."
                                value={searchQuery}
                                onChange={(e) => setSearchQuery(e.target.value)}
                            />
                        </div>
                    </div>
                </div>

                {/* Blog Grid */}
                {displayedBlogs.length === 0 ? (
                    <div className="text-center py-10">
                        <h2 className="text-lg text-gray-600">No blogs found.</h2>
                    </div>
                ) : (
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                        {displayedBlogs.map(blog => (
                            <motion.div
                                key={blog.id}
                                initial={{ opacity: 0, y: 20 }}
                                animate={{ opacity: 1, y: 0 }}
                                transition={{ duration: 0.3 }}
                            >
                                <BlogPost blog={blog} />
                            </motion.div>
                        ))}
                    </div>
                )}

                {/* Pagination */}
                <div className="flex justify-center gap-14 mt-12">
                    <Button variant="outline" onClick={() => handlePageChange(currentPage - 1)} disabled={currentPage === 1}>
                        <ChevronLeft className="h-4 w-4" /> Prev
                    </Button>

                    {[...Array(totalPages)].map((_, i) => (
                        <Button
                            key={i}
                            variant={currentPage === i + 1 ? "default" : "outline"}
                            onClick={() => handlePageChange(i + 1)}
                        >
                            {i + 1}
                        </Button>
                    ))}

                    <Button variant="outline" onClick={() => handlePageChange(currentPage + 1)} disabled={currentPage === totalPages}>
                        Next <ChevronRight className="h-4 w-4" />
                    </Button>
                </div>
            </div>
        </div>
    );
};

export default Blog;
