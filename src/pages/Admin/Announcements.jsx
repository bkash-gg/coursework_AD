import React, { useState } from "react";

const Announcements = () => {
  const [announcements, setAnnouncements] = useState([
    { id: 1, message: "Big Summer Sale - 30% Off!", until: "2025-07-01" },
    { id: 2, message: "New Arrivals: Mystery Books", until: "2025-05-30" },
  ]);

  return (
    <div>
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Announcements</h2>

      <ul className="space-y-4">
        {announcements.map((a) => (
          <li key={a.id} className="bg-white p-4 rounded shadow flex justify-between items-center">
            <div>
              <p className="font-medium text-gray-700">{a.message}</p>
              <p className="text-sm text-gray-500">Valid Until: {a.until}</p>
            </div>
            <button className="text-sm text-red-500 hover:underline">Remove</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Announcements;
