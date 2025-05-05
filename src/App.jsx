import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import BookCatalog from './pages/BookCatalog';
import Home from './pages/Home';
import './App.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/BookCatalog" element={<BookCatalog />} />
      </Routes>
    </Router>
  );
}

export default App;
