    import InterestCard from "./InterestCard";

    function InterestsInfo({ interests, isMain, fetchUser }) {
        return (
            <section className="interests">
                <h3>Interests</h3>
                {interests.map(interest => <InterestCard interest={interest} key={interest.interest_id} isMain={isMain} fetchUser={fetchUser}/>)}    
            </section>
        );
    }

    export default InterestsInfo
