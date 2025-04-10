import './change_profile_data_style.css';
import Header from './Header';
import DataEdit from './DataEdit';

function ChangeProfileData({user, onSave, onCancel}) {
    return (
        <>
            <Header image={user.ProfilePicUrl} editable={true}/>
            <section class="info">
                <DataEdit user={user} onSave={onSave} onCancel={onCancel}/>
            </section>
        </>
    );
}

export default ChangeProfileData
