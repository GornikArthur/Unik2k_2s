import { useState, useEffect  } from 'react'
import './likes_page.css'
import ContactCard from '../components/ContactCard'
import BottomNav from '../components/BottomNav'

function Likes(){
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetch("http://127.0.0.1:8000/likes")
          .then((res) => res.json())
          .then((data) => setUsers(data.users));
      }, []);

    return (
        <div className="container">
            <h2>Likes</h2>
            <div className="data">
                {users.map(user => <ContactCard user={user} key={user.user_id} displayTG={true}/>)}
                <BottomNav />
            </div>
        </div>
    )
}

export default Likes