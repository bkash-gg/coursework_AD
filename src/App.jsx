import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import BookCatalog from './pages/BookCatalog';
import Home from './pages/Home';
import './App.css';
import Login from './pages/Login';
import SignUp from './pages/SignUp';
import Blog from './pages/Blog';
import BlogPost from './pages/BlogPost';



function App() {
  return (
    <Router>
      
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/BookCatalog" element={<BookCatalog />} />
        <Route path="/login" element={<Login/>} />
        <Route path="/signup" element={<SignUp/>} />
        <Route path="/blog" element={<Blog/>} />
        <Route path="/blog/:id" element={<BlogPost/>} />
      </Routes>
    </Router>
  );
}

export default App;
