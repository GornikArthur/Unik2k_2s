
import { Link } from 'react-router-dom';
function BottomNav() {
    return (
        <nav className="bottom-nav">
            <Link to="/likes"><img src="img/heart.png" alt="Heart Picture"/></Link>
            <Link to="/"><img src="img/search.png" alt="Heart Picture"/></Link>
            <Link to="/edit"><img src="img/Profile.png" alt="Heart Picture"/></Link>
        </nav>
    );
}

export default BottomNav
