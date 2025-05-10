import React from "react";

const Books = () => {
  // Replace with API fetch later
  const books = [
    { id: 1, title: "The Alchemist", author: "Paulo Coelho", stock: 10 },
    { id: 2, title: "Atomic Habits", author: "James Clear", stock: 5 },
  ];

  return (
    <div>
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Manage Books</h2>
      <table className="w-full table-auto bg-white rounded shadow">
        <thead className="bg-gray-100 text-gray-700 text-left">
          <tr>
            <th className="p-4">Title</th>
            <th className="p-4">Author</th>
            <th className="p-4">Stock</th>
            <th className="p-4">Actions</th>
          </tr>
        </thead>
        <tbody>
          {books.map(book => (
            <tr key={book.id} className="border-t hover:bg-gray-50">
              <td className="p-4">{book.title}</td>
              <td className="p-4">{book.author}</td>
              <td className="p-4">{book.stock}</td>
              <td className="p-4 space-x-3">
                <button className="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 text-sm">Edit</button>
                <button className="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600 text-sm">Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Books;
