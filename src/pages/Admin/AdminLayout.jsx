import React from "react";
import { Link, Outlet } from "react-router-dom";

const AdminLayout = () => {
  return (
    <div className="flex min-h-screen bg-gray-100">
      {/* Left Sidebar with new color */}
      <aside className="w-64 bg-slate-800 text-white p-6 pt-9 flex flex-col justify-between">
        <h2 className="text-2xl font-bold mb-8">Admin Panel</h2>
        <nav className="space-y-3">
          <Link to="/admin/dashboard" className="block hover:bg-yellow-500 hover:text-slate-800 text-left pl-9 rounded">Dashboard</Link>
          <Link to="/admin/books" className="block hover:bg-yellow-500 hover:text-slate-800 text-left pl-9 rounded">Manage Books</Link>
          <Link to="/admin/orders" className="block hover:bg-yellow-500 hover:text-slate-800 text-left pl-9 rounded">Manage Orders</Link>
          <Link to="/admin/announcements" className="block hover:bg-yellow-500 hover:text-slate-800 text-left pl-9 rounded">Announcements</Link>
        </nav>
        <br />
        <button>
          <Link to="/admin/logout" className="mt-8 block bg-blue-600 hover:bg-red-700 text-white text-center px-2 rounded">
            Logout
          </Link>
        </button>
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col bg-gray-50">
        {/* Page Content */}
        <main className="px-10 py-8 flex-1 overflow-y-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;
