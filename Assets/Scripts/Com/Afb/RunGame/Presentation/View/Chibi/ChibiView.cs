using System;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Presentation.View.Util;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class ChibiView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private Animator animator;

        // Dependencies
        [Inject]
        private ICharacterPresenter characterPresenter;
        [Inject]
        private IGamePresenter gamePresenter;

        // Private Properties
        private Rigidbody body;
        private bool willFall;
        private GameSate gameSate;

        // Unity Methods
        private void Awake() {
            body = GetComponent<Rigidbody>();
        }

        private void Start() {
            animator.Play("Idle");

            characterPresenter.WillFall
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnWillFall);

            characterPresenter.Position
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnCharacterPosition);

            gamePresenter.GameState
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnGameStateChange);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ViewConstants.DESPAWNER_TAG) {
                body.isKinematic = true;
                willFall = false;
            }
        }

        // Public Methods
        public void ResetPosition() {
            transform.localPosition = Vector3.zero;
        }

        // Private Methods
        private void OnGameStateChange(GameSate gameSate) {
            this.gameSate = gameSate;

            if (gameSate == GameSate.Playing) {
                Idle();
            }
        }

        private void OnWillFall(bool willFall) {
            this.willFall = willFall;
            body.isKinematic = !willFall;
        }

        private void OnCharacterPosition(Vector3 position) {
            ResetAnimation();
 
            //body.DOMove(position, 0.8f).OnComplete(OnMoveComplete); // TODO not working?
            transform.DOLocalMove(position, willFall ? 0.4f : 0.8f).OnComplete(OnMoveComplete);

            if (position == Vector3.zero) {
                Idle();
            }
            else {
                Run();
            }
        }

        private void OnMoveComplete() {
            ResetAnimation();

            if (gameSate == GameSate.Complete) {
                Dance();
            }
            else if (gameSate != GameSate.Fail) {
                Idle();
            }
        }

        private void ResetAnimation() {
            transform.GetChild(0).localPosition = Vector3.zero;
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        }

        private void Idle() {
            animator.Play("Idle");
        }

        private void Run() {
            animator.Play("Run");
        }

        private void Dance() {
            animator.Play("Dance");
        }
    }
}