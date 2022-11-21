using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICubeUpdatablePresenter {
        // Methods
        void SetSize(Vector3 size);
        void SetColor(Color color);
        void SetIsMoving(bool isMoving);
        void SetSpeed(float speed);
    }
}
