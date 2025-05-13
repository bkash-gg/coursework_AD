import React, { useState } from "react";

const AddAnnouncementForm = ({ onClose, onAdd }) => {
  const [formData, setFormData] = useState({
    message: "",
    until: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    onAdd({
      id: Date.now(), // temporary ID generation
      message: formData.message,
      until: formData.until,
    });
    onClose();
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  return (
    <div className="bg-white rounded-lg shadow-lg p-6">
      <div className="flex justify-between items-center mb-6">
        <h3 className="text-xl font-semibold text-gray-800">
          Add New Announcement
        </h3>
        <button onClick={onClose} className="text-gray-500 hover:text-gray-700">
          ✕
        </button>
      </div>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label
            htmlFor="message"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Announcement Message
          </label>
          <textarea
            id="message"
            name="message"
            value={formData.message}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            rows="3"
            placeholder="Enter your announcement message..."
          />
        </div>
        <div>
          <label
            htmlFor="until"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Valid Until
          </label>
          <input
            type="date"
            id="until"
            name="until"
            value={formData.until}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
          />
        </div>
        <div className="flex justify-end space-x-3 pt-4">
          <button
            type="button"
            onClick={onClose}
            className="px-4 py-2 border border-gray-300 rounded-md text-gray-700 hover:bg-gray-50"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="px-4 py-2 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] text-white rounded-md hover:shadow-lg transition-all duration-300 hover:scale-[1.02] active:scale-95 shadow-md hover:shadow-[#3B6CF7]/30"
          >
            Add Announcement
          </button>
        </div>
      </form>
    </div>
  );
};

const EditAnnouncementForm = ({ announcement, onClose, onEdit }) => {
  const [formData, setFormData] = useState({
    message: announcement.message,
    until: announcement.until,
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    onEdit({
      ...announcement,
      ...formData,
    });
    onClose();
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  return (
    <div className="bg-white rounded-lg shadow-lg p-6">
      <div className="flex justify-between items-center mb-6">
        <h3 className="text-xl font-semibold text-gray-800">
          Edit Announcement
        </h3>
        <button onClick={onClose} className="text-gray-500 hover:text-gray-700">
          ✕
        </button>
      </div>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label
            htmlFor="message"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Announcement Message
          </label>
          <textarea
            id="message"
            name="message"
            value={formData.message}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            rows="3"
          />
        </div>
        <div>
          <label
            htmlFor="until"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Valid Until
          </label>
          <input
            type="date"
            id="until"
            name="until"
            value={formData.until}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
          />
        </div>
        <div className="flex justify-end space-x-3 pt-4">
          <button
            type="button"
            onClick={onClose}
            className="px-4 py-2 border border-gray-300 rounded-md text-gray-700 hover:bg-gray-50"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="px-4 py-2 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] text-white rounded-md hover:shadow-lg transition-all duration-300 hover:scale-[1.02] active:scale-95 shadow-md hover:shadow-[#3B6CF7]/30"
          >
            Save Changes
          </button>
        </div>
      </form>
    </div>
  );
};

const Announcements = () => {
  const [announcements, setAnnouncements] = useState([
    { id: 1, message: "Big Summer Sale - 30% Off!", until: "2025-07-01" },
    { id: 2, message: "New Arrivals: Mystery Books", until: "2025-05-30" },
  ]);
  const [showForm, setShowForm] = useState(false);
  const [editingAnnouncement, setEditingAnnouncement] = useState(null);

  const handleAdd = (newAnnouncement) => {
    setAnnouncements((prev) => [...prev, newAnnouncement]);
  };

  const handleEdit = (editedAnnouncement) => {
    setAnnouncements((prev) =>
      prev.map((a) => (a.id === editedAnnouncement.id ? editedAnnouncement : a))
    );
  };

  const handleRemove = (id) => {
    setAnnouncements((prev) => prev.filter((a) => a.id !== id));
  };

  return (
    <div className="space-y-6">
      <h2 className="text-2xl font-bold text-gray-800">Announcements</h2>
      <p className="text-gray-600 mt-1">Manage your store announcements</p>
      <div className="flex justify-between items-center">
        <div></div>
        {!showForm && !editingAnnouncement && (
          <button
            onClick={() => setShowForm(true)}
            className="px-4 py-2 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] text-white rounded-lg hover:shadow-lg transition-all duration-300 hover:scale-[1.02] active:scale-95 shadow-md hover:shadow-[#3B6CF7]/30"
          >
            + Add Announcement
          </button>
        )}
      </div>

      {showForm && (
        <AddAnnouncementForm
          onClose={() => setShowForm(false)}
          onAdd={handleAdd}
        />
      )}

      {editingAnnouncement && (
        <EditAnnouncementForm
          announcement={editingAnnouncement}
          onClose={() => setEditingAnnouncement(null)}
          onEdit={handleEdit}
        />
      )}

      {!showForm && !editingAnnouncement && (
        <div className="bg-white rounded-lg shadow-md overflow-hidden">
          <div className="divide-y divide-gray-200">
            {announcements.map((announcement) => (
              <div
                key={announcement.id}
                className="p-6 hover:bg-gray-50 transition-colors duration-200"
              >
                <div className="flex justify-between items-start">
                  <div>
                    <p className="text-lg font-medium text-gray-900">
                      {announcement.message}
                    </p>
                    <p className="text-sm text-gray-500 mt-1">
                      Valid until: {announcement.until}
                    </p>
                  </div>
                  <div className="flex space-x-3">
                    <button
                      onClick={() => setEditingAnnouncement(announcement)}
                      className="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 text-sm"
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleRemove(announcement.id)}
                      className="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600 text-sm"
                    >
                      Delete
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default Announcements;
