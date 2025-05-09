import React from 'react'; 
import Button from "../components/Button"; 
import { ArrowRight } from 'lucide-react';

const BlogPost = ({ blog }) => {
    return (
        <div className="bg-white rounded-xl shadow-sm overflow-hidden flex flex-col hover:shadow-lg transition-shadow duration-300">
            <img src={blog.image} alt={blog.title} className="w-full h-48 object-cover" />
            <div className="p-6 flex flex-col flex-grow">
                <h3 className="font-bold text-xl mb-2">{blog.title}</h3>
                <p className="text-gray-600 text-sm mb-4 flex-grow">{blog.excerpt}</p>
                <Button variant="ghost" className="self-start text-blue-600 hover:text-blue-800 hover:bg-transparent">
                    Read More <ArrowRight className="ml-2 h-4 w-4" />
                </Button>
            </div>
        </div>
    );
};

export default BlogPost;
