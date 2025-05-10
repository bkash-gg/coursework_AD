import React from "react";
import { Link, Outlet } from "react-router-dom";

const AdminLayout = () => {
  return (
    <div className="flex min-h-screen bg-gray-100">
      {/* Left Sidebar with new color */}
      <aside className="w-64 bg-slate-800 text-white p-6">
        <h2 className="text-2xl font-bold mb-8">Admin Panel</h2>
        <nav className="space-y-3">
          <Link to="/admin/dashboard" className="block hover:text-yellow-200">Dashboard</Link>
          <Link to="/admin/books" className="block hover:text-yellow-200">Manage Books</Link>
          <Link to="/admin/orders" className="block hover:text-yellow-200">Manage Orders</Link>
          <Link to="/admin/announcements" className="block hover:text-yellow-200">Announcements</Link>
        </nav>
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col bg-gray-50">
        {/* Top Bar */}
        <header className="bg-white shadow-md px-10 py-4 flex justify-between items-center">
          <h1 className="text-xl font-semibold text-gray-700">Admin Dashboard</h1>
          <span className="text-sm text-gray-500">Welcome, Admin</span>
        </header>

        {/* Page Content */}
        <main className="px-10 py-8 flex-1 overflow-y-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;
