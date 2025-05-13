import React, { useState } from "react";
import axios from "axios";

const AddBookForm = ({ onClose }) => {
  const [form, setForm] = useState({
    title: "",
    isbn: "",
    publicationDate: "",
    language: "",
    description: "",
    price: "",
    stockQuantity: "",
    isAvailable: false,
    format: "",
    coverImageUrl: "",
    isAwardWinner: false,
    isComingSoon: false,
    publisherId: "",
    authorId: "",
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm({
      ...form,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Submitted book:", form);

    try {
      // Send the POST request to your backend API
      const response = await axios.post("https://localhost:7098/api/books/add", form); // Use your backend URL here
      console.log("Book added:", response.data);
      onClose();
    } catch (error) {
      console.error("Error adding book:", error);
    }
  };

  return (
    <div className="bg-white p-6 rounded shadow mb-6 border">
      <h3 className="text-lg font-bold mb-4">Add New Book</h3>
      <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <input className="p-2 border rounded" placeholder="Title" name="title" value={form.title} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="ISBN" name="isbn" value={form.isbn} onChange={handleChange} required />
        <input className="p-2 border rounded" type="date" name="publicationDate" value={form.publicationDate} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="Language" name="language" value={form.language} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="Price" type="number" name="price" value={form.price} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="Stock Quantity" type="number" name="stockQuantity" value={form.stockQuantity} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="Format" name="format" value={form.format} onChange={handleChange} required />
        <input className="p-2 border rounded" type="file" accept="image/*" placeholder="Cover Image URL" name="coverImageUrl" value={form.coverImageUrl} onChange={handleChange} />
        <input className="p-2 border rounded" placeholder="Publisher ID" name="publisherId" value={form.publisherId} onChange={handleChange} required />
        <input className="p-2 border rounded" placeholder="Author ID" name="authorId" value={form.authorId} onChange={handleChange} required />

        <textarea
          className="p-2 border rounded md:col-span-2"
          placeholder="Description"
          name="description"
          value={form.description}
          onChange={handleChange}
          required
        />

        <label className="flex items-center space-x-2">
          <input type="checkbox" name="isAvailable" checked={form.isAvailable} onChange={handleChange} />
          <span>Available</span>
        </label>

        <label className="flex items-center space-x-2">
          <input type="checkbox" name="isAwardWinner" checked={form.isAwardWinner} onChange={handleChange} />
          <span>Award Winner</span>
        </label>

        <label className="flex items-center space-x-2">
          <input type="checkbox" name="isComingSoon" checked={form.isComingSoon} onChange={handleChange} />
          <span>Coming Soon</span>
        </label>

        <div className="md:col-span-2 flex justify-end gap-3 mt-4">
          <button
            type="button"
            onClick={onClose}
            className="px-4 py-2 border rounded text-gray-700 hover:bg-gray-100"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
          >
            Add Book
          </button>
        </div>
      </form>
    </div>
  );
};

export default AddBookForm;
