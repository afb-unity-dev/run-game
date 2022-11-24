using Com.Afb.RunGame.Business.Model;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICubeUpdatablePresenter {
        // Methods
        void SetSize(Vector3 size);
        void SetColor(Color color);
        void SetIsMoving(bool isMoving);
        void SetSpeed(float speed);
        void SetCut(CubeCutModel cut);
        void SetXPosition(float x);
        void Reset();
        void SetPerfectScore(int score);
    }
}
