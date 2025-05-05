import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import BookCatalog from './pages/BookCatalog'
import './App.css'

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<BookCatalog />} />
        {/* Add more routes here as needed */}
      </Routes>
    </Router>
  )
}

export default App
