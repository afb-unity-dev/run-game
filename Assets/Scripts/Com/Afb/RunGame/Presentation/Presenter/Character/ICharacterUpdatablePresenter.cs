using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICharacterUpdatablePresenter {
        // Methods
        void SetPosition(Vector3 position);
        void SetWillFall(bool willFall);
    }
}
