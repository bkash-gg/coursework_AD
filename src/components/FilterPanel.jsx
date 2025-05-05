import React from 'react';

const FilterPanel = () => {
  return (
    <aside className="bg-white border rounded-lg p-4 w-full lg:w-64 shadow-sm">
      <h2 className="text-lg font-semibold mb-4">Filters</h2>

      <div className="mb-4">
        <label className="block text-sm font-medium mb-1">Price Range</label>
        <input type="range" min="0" max="100" className="w-full" />
        <div className="text-sm text-gray-600 mt-1">$0 - $100</div>
      </div>

      <div className="mb-4">
        <label className="block text-sm font-medium mb-1">Availability</label>
        <div className="space-y-1 text-sm">
          <label className="flex items-center gap-2">
            <input type="checkbox" className="form-checkbox" /> In Stock
          </label>
          <label className="flex items-center gap-2">
            <input type="checkbox" className="form-checkbox" /> In Library
          </label>
        </div>
      </div>

      <div className="mb-4">
        <label className="block text-sm font-medium mb-1">Genres</label>
        <div className="space-y-1 text-sm max-h-32 overflow-auto">
          {['Biography', 'Children', 'Fantasy', 'History', 'Mystery'].map((genre) => (
            <label key={genre} className="flex items-center gap-2">
              <input type="checkbox" className="form-checkbox" /> {genre}
            </label>
          ))}
        </div>
      </div>

      <button className="w-full mt-4 text-sm bg-gray-100 hover:bg-gray-200 py-2 rounded">
        Clear All
      </button>
    </aside>
  );
};

export default FilterPanel; 