using Com.Afb.RunGame.Presentation.Interactor;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CubeSoundView : MonoBehaviour {
        // Dependencies
        [Inject]
        private ICubeScorePresenter cubeSoundPresenter;
        [Inject]
        private ISoundInteractor soundInteractor;

        // Unity Methods
        private void Start() {
            cubeSoundPresenter.PerfectScore
                .TakeUntilDestroy(gameObject)
                .Subscribe(PlaySound);
        }

        private void PlaySound(int score) {
            if (score == -1) {
                soundInteractor.PlaySound("fail");
            }
            else if (score >= 0) {
                int soundNumber = score;

                if (soundNumber > Constants.NUMBER_OF_NOTES - 1) {
                    soundNumber = ((score - 4) % 3) + Constants.NUMBER_OF_NOTES - 3;
                }

                soundInteractor.PlaySound(soundNumber.ToString());
            }
        }
    }
}
