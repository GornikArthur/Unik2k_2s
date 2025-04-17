function InterestCard({ interest, isMain, fetchUser }) {
    const { interest_id, Title, Description } = interest;

    const removeInterest = () => {
        fetch(`http://localhost:8000/remove_interest/${interest_id}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ interest_id })
        })
        .then(response => {
            fetchUser();
            if (!response.ok) throw new Error("Failed to save");
            return response.json();
        })
    }

    return (
        <div className="interest">
            <div className="interest-header">
                <span className="interest-title">{Title}</span>
                {isMain && <button className="interest-btn" onClick={removeInterest}>
                    <img 
                        src="../img/dislike.png" // Заменить на путь к своей иконке
                        alt="Удалить" 
                    />
                </button>}
            </div>
            <p>{Description}</p>
        </div>
    );
}

export default InterestCard
