using Com.Afb.RunGame.Presentation.Interactor;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public abstract class PopupView : MonoBehaviour {
        // Dependencies
        [Inject]
        protected IGameInteractor gamePresenter;

        // Public Methods
        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }
    }
}