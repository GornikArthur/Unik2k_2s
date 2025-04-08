
function ContactCard({ user, displayTG }) {
    const { ProfilePicUrl, Name, Age, Location, TelegramLink } = user;
    const {Country, City} = Location;

    return (
        <div className="profile">
            <img src={ProfilePicUrl} alt={`${Name}'s profile`} />
            <div className="profile-info">
                <h3>{Name}, {Age}</h3>
                <p>📍 {Country}, {City}</p>
            </div>
            {displayTG && <a href={TelegramLink || "#"} className="visit-telegram" target="_blank" rel="noopener noreferrer">
                <img className="telegram-icon" src="/img/telegram.png" alt="Telegram" />
            </a>}
        </div>
    );
}

export default ContactCard
