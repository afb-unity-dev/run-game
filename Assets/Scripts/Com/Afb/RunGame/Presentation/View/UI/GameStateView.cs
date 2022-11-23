using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class GameStateView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private PopupView nextLevelView;
        [SerializeField]
        private PopupView playLevelView;
        [SerializeField]
        private PopupView gameOverView;

        // Dependencies
        [Inject]
        private IGamePresenter gamePresenter;

        // Unity Methods
        private void Awake() {
            gamePresenter.GameState
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnGameStateChange);
        }

        // Private Methods
        private void OnGameStateChange(GameSate gameSate) {
            nextLevelView.Hide();
            playLevelView.Hide();
            gameOverView.Hide();

            if (gameSate == GameSate.Ready) {
                playLevelView.Show();
            }
            else if (gameSate == GameSate.Complete) {
                nextLevelView.Show();
            }
            else if (gameSate == GameSate.Fail) {
                gameOverView.Show();
            }

        }
    }
}
