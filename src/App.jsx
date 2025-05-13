import { BrowserRouter as Router, Routes, Route, useLocation } from "react-router-dom";

import Login from "./pages/Login";
import SignUp from "./pages/SignUp";

import Home from "./pages/Home";
import BookCatalog from "./pages/BookCatalog";
import Cart from "./pages/Cart";
import { CartProvider } from "./context/CartContext";

import Navbar from "./components/Navbar";

// Admin Pages
import AdminLayout from "./pages/Admin/AdminLayout";
import AdminDashboard from "./pages/Admin/AdminDashboard";
import Books from "./pages/Admin/Books";
import Orders from "./pages/Admin/Orders";
import Announcements from "./pages/Admin/Announcements";

import Wishlist from "./pages/Wishlist";
import BookDetails from "./pages/BookDetails";

import "./App.css";

function AppRoutes() {
  const location = useLocation();

  const hideNavbarRoutes = ['/login', '/signup'];
  const isAdminRoute = location.pathname.startsWith('/admin');
  const shouldHideNavbar = hideNavbarRoutes.includes(location.pathname) || isAdminRoute;

  return (
    <>
      {!shouldHideNavbar && <Navbar />}
      <Routes>
        {/* Public Routes */}
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/BookCatalog" element={<BookCatalog />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/wishlist" element={<Wishlist />} />
        <Route path="/bookdetails/:id" element={<BookDetails />} />

        {/* Admin Routes */}
        <Route path="/admin" element={<AdminLayout />}>
          <Route path="dashboard" element={<AdminDashboard />} />
          <Route path="books" element={<Books />} />
          <Route path="orders" element={<Orders />} />
          <Route path="announcements" element={<Announcements />} />
        </Route>
      </Routes>
    </>
  );
}

function App() {
  return (
    <CartProvider>
      <Router>
        <AppRoutes />
      </Router>
    </CartProvider>
  );
}

export default App;
