function InterestCard({ interest }) {
    const { Title, Description } = interest;

    return (
        <div className="interest">
            <span className="interest-title">{Title}</span>
            <p>{Description}</p>
        </div>
    );
}

export default InterestCard
