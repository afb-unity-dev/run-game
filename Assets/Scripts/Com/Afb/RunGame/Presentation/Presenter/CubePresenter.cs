using Com.Afb.RunGame.Business.Model;
using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class CubePresenter : ICubePresenter {
        // Readonly Properties
        private readonly ReactiveProperty<float> xPosition = new ReactiveProperty<float>(0);
        private readonly ReactiveProperty<Vector3> size = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<CubeCutModel> cubeCut = new ReactiveProperty<CubeCutModel>(null);
        private readonly ReactiveProperty<bool> isMoving = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<float> moveSpeed = new ReactiveProperty<float>(0);
        private readonly ReactiveProperty<Color> color = new ReactiveProperty<Color>();

        // Private Properties
        public IReadOnlyReactiveProperty<float> XPosition => xPosition;
        public IReadOnlyReactiveProperty<Vector3> Size => size;
        public IReadOnlyReactiveProperty<CubeCutModel> CubeCut => cubeCut;
        public IReadOnlyReactiveProperty<bool> IsMoving => isMoving;
        public IReadOnlyReactiveProperty<float> MoveSpeed => moveSpeed;
        public IReadOnlyReactiveProperty<Color> Color => color;

        public void Reset() {
            cubeCut.Value = null;
        }

        // Public Methods
        public void SetColor(Color color) {
            this.color.Value = color;
        }

        public void SetIsMoving(bool isMoving) {
            this.isMoving.Value = isMoving;
        }

        public void SetXPosition(float x) {
            xPosition.Value = x;
        }

        public void SetSize(Vector3 size) {
            this.size.Value = size;
        }

        public void SetSpeed(float speed) {
            moveSpeed.Value = speed;
        }

        public void SetCut(CubeCutModel cut) {
            cubeCut.Value = cut;
        }
    }
}
