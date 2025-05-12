import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import BookCatalog from './pages/BookCatalog';
import Home from './pages/Home';
import './App.css';
import Login from './pages/Login';
import SignUp from './pages/SignUp';
import Blog from './pages/Blog';
import BlogPost from './pages/BlogPost';
import Cart from './pages/Cart';
import { CartProvider } from './context/CartContext';


// Admin Pages
import AdminLayout from './pages/Admin/AdminLayout';
import AdminDashboard from './pages/Admin/AdminDashboard';
import Books from './pages/Admin/Books';
import Orders from './pages/Admin/Orders';
import Announcements from './pages/Admin/Announcements';
import Wishlist from './pages/Wishlist';
import BookDetails from './pages/BookDetails';
function App() {
  return (
    <CartProvider>
      <Router>
        <Routes>
          {/* Public Routes */}
          <Route path="/" element={<Home />} />
          <Route path="/BookCatalog" element={<BookCatalog />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/blog" element={<Blog />} />
          <Route path="/blog/:id" element={<BlogPost />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/wishlist" element={<Wishlist />} />
          <Route path="/bookdetails" element={<BookDetails />} />

          {/* Admin Routes without Protection */}
          <Route path="/admin" element={<AdminLayout />}>
            <Route path="dashboard" element={<AdminDashboard />} />
            <Route path="books" element={<Books />} />
            <Route path="orders" element={<Orders />} />
            <Route path="announcements" element={<Announcements />} />
          </Route>
        </Routes>
      </Router>
    </CartProvider>
  );
}

export default App;
