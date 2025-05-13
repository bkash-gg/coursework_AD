import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Book, Tag, UserCheck, ShoppingCart } from "lucide-react";
import axios from "axios";
import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  Legend,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  ResponsiveContainer,
} from "recharts";
import { div } from "framer-motion/client";

const COLORS = ["#6366F1", "#10B981", "#F59E0B", "#EF4444"];

const AdminDashboard = () => {
  const navigate = useNavigate();
  const [stats, setStats] = useState({
    totalBooks: 0,
    totalMembers: 0,
    totalOrders: 0,
    booksOnSale: 0,
  });

  const categoryData = [
    { name: "Fiction", value: 400 },
    { name: "Non-fiction", value: 300 },
    { name: "Science", value: 200 },
    { name: "Romance", value: 100 },
  ];

  const monthlyOrders = [
    { month: "Jan", orders: 30 },
    { month: "Feb", orders: 45 },
    { month: "Mar", orders: 60 },
    { month: "Apr", orders: 20 },
    { month: "May", orders: 50 },
  ];

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const res = await axios.get("/api/admin/dashboard"); // Replace with your actual API
        if (res.data.success) {
          setStats(res.data.stats);
        }
      } catch (error) {
        console.error("Error fetching stats", error);
      }
    };
    fetchStats();
  }, []);

  const Card = ({ icon: Icon, title, value, color }) => (
    <div className="p-6 bg-white rounded-lg shadow flex justify-between items-center">
      <div>
        <p className="text-sm text-gray-500">{title}</p>
        <h3 className="text-2xl font-bold text-gray-800">{value}</h3>
      </div>
      <Icon className={`h-8 w-8 ${color}`} />
    </div>
  );

  return (
    <div className="space-y-10">
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Admin Dashboard</h2>
      <p className="text-gray-600 mb-4">
        Overview of key metrics and statistics
      </p>
      <div className="flex justify-between items-center mb-6">
        <h3 className="text-lg font-semibold text-gray-700">Welcome, Admin!</h3>
      </div>
      {/* Stat Cards */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card
          icon={Book}
          title="Total Books"
          value={stats.totalBooks}
          color="text-blue-500"
        />
        <Card
          icon={Tag}
          title="Books On Sale"
          value={stats.booksOnSale}
          color="text-green-500"
        />
        <Card
          icon={UserCheck}
          title="Total Members"
          value={stats.totalMembers}
          color="text-indigo-500"
        />
        <Card
          icon={ShoppingCart}
          title="Total Orders"
          value={stats.totalOrders}
          color="text-red-500"
        />
      </div>

      {/* Navigation Buttons */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <button
          onClick={() => navigate("/admin/books")}
          className="bg-blue-600 hover:bg-blue-700 text-white py-3 rounded-lg shadow"
        >
          Manage Books
        </button>
        <button
          onClick={() => navigate("/admin/orders")}
          className="bg-indigo-600 hover:bg-indigo-700 text-white py-3 rounded-lg shadow"
        >
          Manage Orders
        </button>
        <button
          onClick={() => navigate("/admin/announcements")}
          className="bg-green-600 hover:bg-green-700 text-white py-3 rounded-lg shadow"
        >
          Announcements
        </button>
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        {/* Pie Chart */}
        <div className="bg-white p-6 rounded-lg shadow">
          <h3 className="text-lg font-semibold mb-4 text-gray-700">
            Book Categories
          </h3>
          <ResponsiveContainer width="100%" height={300}>
            <PieChart>
              <Pie
                data={categoryData}
                dataKey="value"
                nameKey="name"
                cx="50%"
                cy="50%"
                outerRadius={80}
                label
              >
                {categoryData.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={COLORS[index % COLORS.length]}
                  />
                ))}
              </Pie>
              <Tooltip />
              <Legend verticalAlign="bottom" height={36} />
            </PieChart>
          </ResponsiveContainer>
        </div>

        {/* Bar Chart */}
        <div className="bg-white p-6 rounded-lg shadow">
          <h3 className="text-lg font-semibold mb-4 text-gray-700">
            Monthly Orders
          </h3>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart data={monthlyOrders}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="month" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="orders" fill="#6366F1" />
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
