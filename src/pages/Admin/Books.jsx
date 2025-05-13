
import { useState, useEffect } from "react"
import axios from "axios"

const Books = () => {
  const [books, setBooks] = useState([])
  const [showForm, setShowForm] = useState(false)
  const [editingBook, setEditingBook] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
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
    coverImageUrl: null,
    isAwardWinner: false,
    isComingSoon: false,
    publisherId: "",
    authorId: "",
  })

  // Fetch all books
  const fetchBooks = async () => {
    try {
      setLoading(true)
      const response = await axios.get("https://localhost:7098/api/books/all")
      setBooks(response.data.data?.items || [])
      setError(null)
    } catch (error) {
      console.error("Error fetching books:", error)
      setError("Failed to load books. Please try again later.")
      setBooks([])
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchBooks()
  }, [])

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target
    setForm({
      ...form,
      [name]: type === "checkbox" ? checked : value,
    })
  }

  const handleFileChange = (e) => {
    setForm({
      ...form,
      coverImageUrl: e.target.files[0],
    })
  }

  // Add new book
  const handleSubmit = async (e) => {
    e.preventDefault()

    try {
      const token = localStorage.getItem("token")
      if (!token) {
        alert("You must be logged in to add a book")
        return
      }

      const formData = new FormData()
      for (const key in form) {
        if (key === "coverImageUrl" && form[key]) {
          formData.append("file", form[key])
        } else {
          formData.append(key, form[key])
        }
      }

      const response = await axios.post("https://localhost:7098/api/books/add", formData, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "multipart/form-data",
        },
      })

      setBooks([...books, response.data])
      setShowForm(false)
      resetForm()
    } catch (error) {
      console.error("Error adding book:", error)
      alert(`Error adding book: ${error.response?.data?.message || error.message}`)
    }
  }

  // Edit book
  const handleEdit = async (e) => {
    e.preventDefault()

    try {
      const token = localStorage.getItem("token")
      const formData = new FormData()
      for (const key in form) {
        if (key === "coverImageUrl" && form[key] instanceof File) {
          formData.append("file", form[key])
        } else {
          formData.append(key, form[key])
        }
      }

      await axios.put(`https://localhost:7098/api/books/${editingBook.id}/update`, formData, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "multipart/form-data",
        },
      })

      setBooks(books.map((book) => (book.id === editingBook.id ? form : book)))
      setEditingBook(null)
      resetForm()
    } catch (error) {
      console.error("Error updating book:", error)
      alert(`Error updating book: ${error.response?.data?.message || error.message}`)
    }
  }

  // Delete book
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this book?")) return

    try {
      const token = localStorage.getItem("token")
      await axios.delete(`https://localhost:7098/api/books/${id}/delete`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      setBooks(books.filter((book) => book.id !== id))
    } catch (error) {
      console.error("Error deleting book:", error)
      alert(`Error deleting book: ${error.response?.data?.message || error.message}`)
    }
  }

  const resetForm = () => {
    setForm({
      title: "",
      isbn: "",
      publicationDate: "",
      language: "",
      description: "",
      price: "",
      stockQuantity: "",
      isAvailable: false,
      format: "",
      coverImageUrl: null,
      isAwardWinner: false,
      isComingSoon: false,
      publisherId: "",
      authorId: "",
    })
  }

  const startEditing = (book) => {
    setEditingBook(book)
    setForm({
      title: book.title,
      isbn: book.isbn,
      publicationDate: book.publicationDate,
      language: book.language,
      description: book.description,
      price: book.price,
      stockQuantity: book.stockQuantity,
      isAvailable: book.isAvailable,
      format: book.format,
      coverImageUrl: book.coverImageUrl,
      isAwardWinner: book.isAwardWinner,
      isComingSoon: book.isComingSoon,
      publisherId: book.publisherId,
      authorId: book.authorId,
    })
  }

  if (loading) {
    return (
      <div className="p-8 bg-gray-50 min-h-screen">
        <h2 className="text-3xl font-bold mb-6 text-gray-800 border-b pb-4">Manage Books</h2>
        <div className="flex justify-center items-center h-64 bg-white rounded-lg shadow-sm">
          <div className="animate-pulse flex space-x-4">
            <div className="rounded-full bg-gray-200 h-12 w-12"></div>
            <div className="flex-1 space-y-4 py-1">
              <div className="h-4 bg-gray-200 rounded w-3/4"></div>
              <div className="space-y-2">
                <div className="h-4 bg-gray-200 rounded"></div>
                <div className="h-4 bg-gray-200 rounded w-5/6"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }

  if (error) {
    return (
      <div className="p-8 bg-gray-50 min-h-screen">
        <h2 className="text-3xl font-bold mb-6 text-gray-800 border-b pb-4">Manage Books</h2>
        <div className="flex justify-center items-center h-64 bg-white rounded-lg shadow-sm">
          <div className="text-center">
            <p className="text-red-500 font-medium mb-4">{error}</p>
            <button
              onClick={fetchBooks}
              className="px-4 py-2 bg-gray-600 text-white rounded-md hover:bg-gray-700 transition-colors duration-200"
            >
              Try Again
            </button>
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="p-8 bg-gray-50 min-h-screen">
      <h2 className="text-3xl font-bold mb-2 text-gray-800">Manage Books</h2>
      <p className="text-gray-600 mb-6 pb-4 border-b">Add, edit, and delete books from the catalog.</p>

      <div className="flex justify-between items-center mb-6">
        <div className="text-sm text-gray-500">
          {books.length} {books.length === 1 ? "book" : "books"} in catalog
        </div>
        {!showForm && !editingBook && (
          <button
            className="px-5 py-2.5 bg-emerald-600 text-white rounded-md hover:bg-emerald-700 transition-colors duration-200 shadow-sm flex items-center font-medium"
            onClick={() => setShowForm(true)}
          >
            <span className="mr-1 text-lg">+</span> Add Book
          </button>
        )}
      </div>

      {showForm || editingBook ? (
        <div className="bg-white p-8 rounded-lg shadow-md mb-8 border border-gray-100">
          <h3 className="text-xl font-bold mb-6 text-gray-800 pb-2 border-b">
            {editingBook ? "Edit Book" : "Add New Book"}
          </h3>
          <form onSubmit={editingBook ? handleEdit : handleSubmit} className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Title</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Book title"
                name="title"
                value={form.title}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">ISBN</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="ISBN number"
                name="isbn"
                value={form.isbn}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Publication Date</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                type="date"
                name="publicationDate"
                value={form.publicationDate}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Language</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Book language"
                name="language"
                value={form.language}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Price ($)</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Book price"
                type="number"
                step="0.01"
                name="price"
                value={form.price}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Stock Quantity</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Available stock"
                type="number"
                name="stockQuantity"
                value={form.stockQuantity}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Format</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Book format (e.g., Hardcover, Paperback)"
                name="format"
                value={form.format}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Cover Image</label>
              <input
                className="w-full p-2.5 border border-gray-300 rounded-md file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm file:font-medium file:bg-emerald-50 file:text-emerald-700 hover:file:bg-emerald-100 focus:outline-none"
                type="file"
                accept="image/*"
                name="coverImageUrl"
                onChange={handleFileChange}
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Publisher ID</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Publisher ID"
                name="publisherId"
                value={form.publisherId}
                onChange={handleChange}
                required
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Author ID</label>
              <input
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all"
                placeholder="Author ID"
                name="authorId"
                value={form.authorId}
                onChange={handleChange}
                required
              />
            </div>

            <div className="md:col-span-2">
              <label className="block text-sm font-medium text-gray-700 mb-1">Description</label>
              <textarea
                className="w-full p-3 border border-gray-300 rounded-md focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 outline-none transition-all min-h-[120px]"
                placeholder="Book description"
                name="description"
                value={form.description}
                onChange={handleChange}
                required
              />
            </div>

            <div className="md:col-span-2 grid grid-cols-1 md:grid-cols-3 gap-4">
              <label className="flex items-center space-x-3 p-3 border border-gray-200 rounded-md hover:bg-gray-50 cursor-pointer">
                <input
                  type="checkbox"
                  name="isAvailable"
                  checked={form.isAvailable}
                  onChange={handleChange}
                  className="h-5 w-5 text-emerald-600 rounded focus:ring-emerald-500 border-gray-300"
                />
                <span className="text-gray-700 font-medium">Available</span>
              </label>

              <label className="flex items-center space-x-3 p-3 border border-gray-200 rounded-md hover:bg-gray-50 cursor-pointer">
                <input
                  type="checkbox"
                  name="isAwardWinner"
                  checked={form.isAwardWinner}
                  onChange={handleChange}
                  className="h-5 w-5 text-emerald-600 rounded focus:ring-emerald-500 border-gray-300"
                />
                <span className="text-gray-700 font-medium">Award Winner</span>
              </label>

              <label className="flex items-center space-x-3 p-3 border border-gray-200 rounded-md hover:bg-gray-50 cursor-pointer">
                <input
                  type="checkbox"
                  name="isComingSoon"
                  checked={form.isComingSoon}
                  onChange={handleChange}
                  className="h-5 w-5 text-emerald-600 rounded focus:ring-emerald-500 border-gray-300"
                />
                <span className="text-gray-700 font-medium">Coming Soon</span>
              </label>
            </div>

            <div className="md:col-span-2 flex justify-end gap-4 mt-6 pt-4 border-t">
              <button
                type="button"
                onClick={() => {
                  if (editingBook) {
                    setEditingBook(null)
                  } else {
                    setShowForm(false)
                  }
                  resetForm()
                }}
                className="px-5 py-2.5 border border-gray-300 rounded-md text-gray-700 hover:bg-gray-50 transition-colors duration-200 font-medium"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="px-5 py-2.5 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors duration-200 shadow-sm font-medium"
              >
                {editingBook ? "Update Book" : "Add Book"}
              </button>
            </div>
          </form>
        </div>
      ) : (
        <div className="overflow-x-auto bg-white rounded-lg shadow-md">
          <table className="w-full table-auto">
            <thead className="bg-gray-100 text-gray-700 text-left">
              <tr>
                <th className="px-6 text-center py-4 font-semibold">Title</th>
                <th className="px-6 text-center py-4 font-semibold">Author</th>
                <th className="px-6 text-center py-4 font-semibold">Price</th>
                <th className="px-6 text-center py-4 font-semibold">Stock</th>
                <th className="px-6 text-center py-4 font-semibold">Status</th>
                <th className="px-6 text-center py-4 font-semibold">Actions</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {books.length === 0 ? (
                <tr>
                  <td colSpan={6} className="px-6 py-12 text-center text-gray-500">
                    No books found in the catalog. Add your first book to get started.
                  </td>
                </tr>
              ) : (
                books.map((book) => (
                  <tr key={book.id} className="hover:bg-gray-50 transition-colors duration-150">
                    <td className="px-6 py-4 font-medium">{book.title}</td>
                    <td className="px-6 py-4 text-gray-600">{book.authorName || book.author || "Unknown"}</td>
                    <td className="px-6 py-4 text-gray-600">${book.price?.toFixed(2)}</td>
                    <td className="px-6 py-4 text-gray-600">{book.stockQuantity || book.stock || 0}</td>
                    <td className="px-6 py-4">
                      <span
                        className={`px-3 py-1.5 rounded-full text-xs font-medium ${
                          book.isAvailable
                            ? "bg-green-100 text-green-800 border border-green-200"
                            : "bg-red-100 text-red-800 border border-red-200"
                        }`}
                      >
                        {book.isAvailable ? "Available" : "Unavailable"}
                      </span>
                      {book.isAwardWinner && (
                        <span className="ml-2 px-3 py-1.5 rounded-full text-xs font-medium bg-amber-100 text-amber-800 border border-amber-200">
                          Award Winner
                        </span>
                      )}
                      {book.isComingSoon && (
                        <span className="ml-2 px-3 py-1.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800 border border-blue-200">
                          Coming Soon
                        </span>
                      )}
                    </td>
                    <td className="px-6 py-4 text-center">
                      <div className="flex justify-center space-x-3">
                        <button
                          className="px-3.5 py-1.5 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors duration-200 text-sm font-medium shadow-sm"
                          onClick={() => startEditing(book)}
                        >
                          Edit
                        </button>
                        <button
                          className="px-3.5 py-1.5 bg-red-600 text-white rounded-md hover:bg-red-700 transition-colors duration-200 text-sm font-medium shadow-sm"
                          onClick={() => handleDelete(book.id)}
                        >
                          Delete
                        </button>
                      </div>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}

export default Books
