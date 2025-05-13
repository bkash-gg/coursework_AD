import React from "react";

const Orders = () => {
  const orders = [
    { id: 101, member: "Usha Gurung", items: 3, status: "Pending" },
    { id: 102, member: "Ram Shah", items: 1, status: "Completed" },
  ];

  return (
    <div>
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Manage Orders</h2>
      <table className="w-full table-auto bg-white rounded shadow">
        <thead className="bg-gray-100 text-gray-700 text-left">
          <tr>
            <th className="p-4 text-center">Order ID</th>
            <th className="p-4 text-center">Member</th>
            <th className="p-4 text-center">Items</th>
            <th className="p-4 text-center">Status</th>
            <th className="p-4 text-center">Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map(order => (
            <tr key={order.id} className="border-t hover:bg-gray-50">
              <td className="p-4 text-center">{order.id}</td>
              <td className="p-4 text-center">{order.member}</td>
              <td className="p-4 text-center">{order.items}</td>
              <td className="p-4 text-center">
                <span className={`text-sm font-medium ${order.status === "Completed" ? "text-green-600" : "text-yellow-600"}`}>
                  {order.status}
                </span>
              </td>
              <td className="p-4 space-x-3">
                <button className="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 text-sm">View</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Orders;
