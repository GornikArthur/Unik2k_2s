function InterestCard({ interest }) {
    const { Id, Title, Description } = interest;

    const removeInterest = () => {
        fetch(`http://localhost:8000/remove_interest/${Id}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ Id })
        })
        .then(response => {
            if (!response.ok) throw new Error("Failed to save");
            return response.json();
        })
    }

    return (
        <div className="interest">
            <div className="interest-header">
                <span className="interest-title">{Title}</span>
                <button className="interest-btn" onClick={removeInterest}>
                    <img 
                        src="../img/dislike.png" // Заменить на путь к своей иконке
                        alt="Удалить" 
                    />
                </button>
            </div>
            <p>{Description}</p>
        </div>
    );
}

export default InterestCard
