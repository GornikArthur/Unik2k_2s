
function Header({image, editable}) {
    return (
        <header>
            <h2>Edit Profile</h2>
            <div className="profile-pic">
                <img src={image} alt="Profile Picture" />
                {editable && <button class="edit-btn" id="load-btn">Load</button>}
            </div>
        </header>
    );
}

export default Header
