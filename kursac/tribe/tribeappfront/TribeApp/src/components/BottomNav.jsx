
import { Link } from 'react-router-dom';
function BottomNav() {
    return (
        <nav className="bottom-nav">
            <Link to="/likes"><a href="#" className="nav-item"><img src="img/heart.png" alt="Heart Picture"/></a></Link>
            <Link to="/"><a href="#" className="nav-item"><img src="img/search.png" alt="Heart Picture"/></a></Link>
            <Link to="/edit"><a href="#" className="nav-item"><img src="img/Profile.png" alt="Heart Picture"/></a></Link>
        </nav>
    );
}

export default BottomNav
