using Com.Afb.RunGame.Presentation.Presenter;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CameraFollowView : MonoBehaviour {
        // Dependencies
        [Inject]
        private ICharacterPresenter characterPresenter;

        // Unity methods
        private void Start() {
            characterPresenter.Position
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnCharacterPosition);
        }

        private void OnCharacterPosition(Vector3 position) {
            transform.DOMoveX(position.x, 0.5f);
        }
    }
}
