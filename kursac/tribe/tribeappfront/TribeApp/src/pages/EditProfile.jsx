import { useState, useEffect } from 'react';
import './edit_prifile_style.css';
import BottomNav from '../components/BottomNav';
import BasicInfo from '../components/BasicInfo';
import InterestsInfo from '../components/InterestsInfo';
import ContactCard from '../components/ContactCard';
import AddNewInterest from '../components/AddNewInterest';
import ChangeProfileData from '../components/ChangeProfileData';

function EditProfile() {
    const [showNewInterest, setShowNewInterest] = useState(false);
    const [showEditProfile, setShowEditProfile] = useState(false);

    const [user, setUser] = useState();

    const fetchUser = () => {
        fetch("http://127.0.0.1:8000/edit")
            .then((res) => res.json())
            .then((data) => setUser(data));
    };

    useEffect(() => {
        fetchUser();
    }, []);

    if (!user) {
        return <div>Loading...</div>;
    }

    const handleSaveUserData = (updatedUser) => {
        setUser(updatedUser); 
        setShowEditProfile(false); 
    };

    const handleEditDataClick = () => setShowEditProfile(true); 
    const handleEditDataCancel = () => setShowEditProfile(false);
    const handleAddInterestClick = () => setShowNewInterest(true);
    const handleAddInterestCancel = () => setShowNewInterest(false);

    const handleSaveNewInterest = (newInterest) => {
        const updatedUser = {
            ...user,
            Interests: [...(user.Interests || []), { ...newInterest, interest_id: (user.Interests?.length || 0) + 1 }]
        }
        setUser(updatedUser)
        setShowNewInterest(false);
    };

    return (
        <div className="container">
            {!showEditProfile && (<>
                <ContactCard user={user} key={user.user_id} displayTG={false}/>
                <div className="edit-info">
                    <button className="edit-btn" onClick={handleEditDataClick}>Edit Profile</button>
                </div>  
                <InterestsInfo interests={user?.Interests || []} isMain={true} fetchUser={fetchUser}/>
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

export default EditProfile;
