import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import BookCatalog from './pages/BookCatalog'
import './App.css'
import Home from './pages/Home'

function App() {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<Home />} />
        <Route path="/" element={<BookCatalog />} />
        
      </Routes>
    </Router>
  )
}

export default App
