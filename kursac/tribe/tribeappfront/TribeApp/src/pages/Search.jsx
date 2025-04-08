import './edit_prifile_style.css';
import BottomNav from '../components/BottomNav';
import LikeDislike from '../components/LikeDislike';
import InterestsInfo from '../components/InterestsInfo';
import ContactCard from '../components/ContactCard'

function Search() {
    const user = {
        Name: "Artur",
        Age: 25,
        Location: { Country: "Latvia", City: "Riga" },
        TelegramLink: "https://t.me/arturgornik",
        ProfilePicUrl: "/img/Profile.png",
        Interests: [
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
        ]
    };

    return (
        <div className="container">
            <ContactCard user={user} displayTG={false}/>
            <InterestsInfo interests={user.Interests} />
            <LikeDislike />
            <BottomNav />
        </div>
    );
}

export default Search;
