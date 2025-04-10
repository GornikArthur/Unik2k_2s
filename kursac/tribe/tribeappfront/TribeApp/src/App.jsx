import Likes from './pages/Likes'
import Search from './pages/Search'
import EditProfile from './pages/EditProfile'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route path="/" element={<Search />} />
          <Route path="/likes" element={<Likes />} />
          <Route path="/edit" element={<EditProfile />} />
        </Routes>
    </Router>
    </>
  )
}

export default App
