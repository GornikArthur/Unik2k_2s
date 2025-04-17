import './edit_prifile_style.css';
import BottomNav from '../components/BottomNav';
import LikeDislike from '../components/LikeDislike';
import InterestsInfo from '../components/InterestsInfo';
import ContactCard from '../components/ContactCard';
import { useState, useEffect } from 'react';

function Search() {
    const [userIndex, setUserIndex] = useState(1);
    const [user, setUser] = useState(null);

    const fetchUser = async (id) => {
        try {
            const res = await fetch(`http://127.0.0.1:8000/search/${id}`);
            if (!res.ok) {
                throw new Error("User not found");
            }
            const data = await res.json();
            setUser(data);
        } catch (error) {
            setUser(null);
            console.error("Error fetching user:", error);
        }
    };

    useEffect(() => {
        fetchUser(userIndex);
    }, [userIndex]);

    const handleLike = () => {
        setUserIndex(prev => prev + 1);
    };

    const handleDisLike = () => {
        setUserIndex(prev => prev + 1);
    };

    if (!user) {
        return <div>Loading or no user found...</div>;
    }

    return (
        <div className="container">
            <ContactCard user={user} displayTG={false} />
            <InterestsInfo interests={user.Interests} isMain={false}/>
            <LikeDislike handleLike={handleLike} handleDisLike={handleDisLike} />
            <BottomNav />
        </div>
    );
}

export default Search;
