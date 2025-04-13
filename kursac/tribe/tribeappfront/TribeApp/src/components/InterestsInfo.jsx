import InterestCard from "./InterestCard";

function InterestsInfo({ interests }) {
    return (
        <section className="interests">
            <h3>Interests</h3>
            {interests.map(interest => <InterestCard interest={interest} key={interest.interest_id}/>)}    
        </section>
    );
}

export default InterestsInfo
