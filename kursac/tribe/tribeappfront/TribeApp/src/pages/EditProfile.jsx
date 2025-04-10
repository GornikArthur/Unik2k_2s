import { useState } from 'react';
import './edit_prifile_style.css';
import BottomNav from '../components/BottomNav';
import BasicInfo from '../components/BasicInfo';
import InterestsInfo from '../components/InterestsInfo';
import Header from '../components/Header';
import AddNewInterest from '../components/AddNewInterest';
import ChangeProfileData from '../components/ChangeProfileData';

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
    const [showEditProfile, setShowEditProfile] = useState(false);

    const [user, setUser] = useState({
        Name: "Artur",
        Age: 25,
        Location: { Country: "Latvia", City: "Riga" },
        TelegramLink: "https://t.me/arturgornik",
        ProfilePicUrl: "/img/Profile.png"
    });

    const handleSaveUserData = (updatedUser) => {
        setUser(updatedUser); 
        setShowEditProfile(false); 
    };

    const handleEditDataClick = () => setShowEditProfile(true); 
    const handleEditDataCancel = () => setShowEditProfile(false);
    const handleAddInterestClick = () => setShowNewInterest(true);
    const handleAddInterestCancel = () => setShowNewInterest(false);

    const handleSaveNewInterest = (newInterest) => {
        setInterests([...interests, { ...newInterest, id: interests.length + 1 }]);
        setShowNewInterest(false);
    };

    return (
        <div className="container">
            {!showEditProfile && (<>
                <Header image={user.ProfilePicUrl}/>
                <BasicInfo user={user} />
                <div className="edit-info">
                    <button className="edit-btn" onClick={handleEditDataClick}>Edit Profile</button>
                </div>  
                <InterestsInfo interests={interests} />
                {!showNewInterest && (
                    <button className="add-interest-btn" onClick={handleAddInterestClick}>
                        <img src="../img/add-interest.png" alt="Profile Picture" />
                        New interest
                    </button>
                )}
                {showNewInterest && <AddNewInterest onSave={handleSaveNewInterest} onCancel={handleAddInterestCancel} />}
                <BottomNav />
            </>)}
            {showEditProfile && (<>
                <ChangeProfileData user={user} onSave={handleSaveUserData} onCancel={handleEditDataCancel}/>
                    
            </>)}
        </div>
    );
}

export default Search;
