import './likes_page.css'
import ContactCard from '../components/ContactCard'
import BottomNav from '../components/BottomNav'

function Likes(){
    const users = [
        {
            id: 1,
            Name: "Artur",
            Age: 25,
            Location: { Country: "Latvia", City: "Riga" },
            TelegramLink: "https://t.me/arturgornik",
            ProfilePicUrl: "/img/Profile.png", 
        },
        {
            id: 2,
            Name: "Anton",
            Age: 27,
            Location: { Country: "USA", City: "Los-Angeles" },
            TelegramLink: "https://t.me/razhnou",
            ProfilePicUrl: "/img/Profile.png", 
        }
    ]

    return (
        <div className="container">
            <h2>Likes</h2>
            <div className="data">
                {users.map(user => <ContactCard user={user} key={user.id} displayTG={true}/>)}
                <BottomNav />
            </div>
        </div>
    )
}

export default Likes