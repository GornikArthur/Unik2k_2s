import { useState } from 'react';
import './edit_prifile_style.css';
import BottomNav from '../components/BottomNav';
import BasicInfo from '../components/BasicInfo';
import InterestsInfo from '../components/InterestsInfo';
import Header from '../components/Header';
import AddNewInterest from '../components/AddNewInterest';

function Search() {
    const [showNewInterest, setShowNewInterest] = useState(false);
    const [interests, setInterests] = useState([
        {
            id: 1,
            Title: "Boxing",
            Description: "I train to build strength, discipline, and a powerful mindset."
        },
        {
            id: 2,
            Title: "Programming",
            Description: "I'm interested in web development, especially JavaScript and Python."
        },
        {
            id: 3,
            Title: "Finance & Investing",
            Description: "I study financial markets, stocks, and long-term investment strategies."
        },
        {
            id: 4,
            Title: "Music",
            Description: "I play guitar and enjoy emotional soundtracks and instrumental music."
        },
        {
            id: 5,
            Title: "Faith",
            Description: "I'm a Christian. I believe that both inner and outer strength matter."
        }
    ]);

    const user = {
        Name: "Artur",
        Age: 25,
        Location: { Country: "Latvia", City: "Riga" },
        TelegramLink: "https://t.me/arturgornik",
        ProfilePicUrl: "/img/Profile.png"
    };

    const handleAddInterestClick = () => setShowNewInterest(true);
    const handleCancel = () => setShowNewInterest(false);

    const handleSave = (newInterest) => {
        setInterests([...interests, { ...newInterest, id: interests.length + 1 }]);
        setShowNewInterest(false);
    };

    return (
        <div className="container">
            <Header image={user.ProfilePicUrl}/>
            <BasicInfo user={user} />
            <div className="edit-info">
                <button className="edit-btn">Edit Profile</button>
            </div>  
            <InterestsInfo interests={interests} />
            {!showNewInterest && (
                <button className="add-interest-btn" onClick={handleAddInterestClick}>
                    <img src="../img/add-interest.png" alt="Profile Picture" />
                    New interest
                </button>
            )}
            {showNewInterest && <AddNewInterest onSave={handleSave} onCancel={handleCancel} />}
            <BottomNav />
        </div>
    );
}

export default Search;
