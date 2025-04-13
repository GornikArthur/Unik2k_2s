function LikeDislike({handleLike, handleDisLike}) {
    return (
        <div className="like-dislike">
            <button className="like-dislike-buttons" onClick={handleLike} id="like-btn"><img src="img/like.png" alt="Like Picture"/></button>
            <button className="like-dislike-buttons" onClick={handleDisLike} id="dislike-btn"><img src="img/dislike.png" alt="Disike Picture"/></button>
        </div>
    );
}

export default LikeDislike
