import { useState } from 'react';

function AddNewInterest({ onSave, onCancel }) {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSaveClick = () => {
        if (title.trim()) {
            onSave({ Title: title, Description: description });
        }
    };

    return (
        <>
            <div className="interest">
                <input
                    id="new-interest"
                    className="interest-title"
                    type="text"
                    placeholder="Enter interest"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                />
                <textarea
                    id="new-description"
                    placeholder="Enter description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>
            <div className="change-interest">
                <button className="change-interest-buttons" id="save-btn" onClick={handleSaveClick}>Save</button>
                <button className="change-interest-buttons" id="cancel-btn" onClick={onCancel}>Cancel</button>
            </div>
        </>
    );
}

export default AddNewInterest;
