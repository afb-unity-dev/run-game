using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class CharacterPresenter : ICharacterPresenter, ICharacterUpdatablePresenter {
        // Readonly Properties
        private readonly ReactiveProperty<Vector3> position = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<bool> willFall = new ReactiveProperty<bool>(false);

        // Public Properties
        public IReadOnlyReactiveProperty<Vector3> Position => position;
        public IReadOnlyReactiveProperty<bool> WillFall => willFall;

        // Public Methods
        public void SetPosition(Vector3 position) {
            this.position.SetValueAndForceNotify(position);
        }

        public void SetWillFall(bool willFall) {
            this.willFall.Value = willFall;
        }
    }
}
