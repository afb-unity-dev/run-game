using System;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Presentation.View.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class MovableXView : MonoBehaviour {
        // Dependencies
        [Inject]
        private ICubeMovePresenter cubeMovePresenter;

        // Private Properties
        private float speed;
        private Rigidbody body;
        private bool isMoving;
        private IDisposable directionObserver;

        // Unity Methods
        private void Awake() {
            body = GetComponent<Rigidbody>();
            isMoving = false;

            cubeMovePresenter.MoveSpeed
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnMoveSpeedChange);

            cubeMovePresenter.IsMoving
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnMoveStateChange);
        }

        // Private Methods
        private void OnMoveSpeedChange(float speed) {
            this.speed = speed;
        }

        private void OnMoveStateChange(bool isMoving) {
            if (isMoving) {
                Vector3 direction = transform.position.x < 0 ? Vector3.right : Vector3.left;
                BeginMove(direction);
            }
            else {
                StopMove();
            }
        }

        private void StopMove() {
            isMoving = false;
            body.velocity = Vector3.zero;
        }

        private void BeginMove(Vector3 direction) {
            directionObserver?.Dispose();
            directionObserver = Observable.EveryFixedUpdate()
                .Where(_ => transform.position.x < ViewConstants.LEFT_BOUNDARY || transform.position.x > ViewConstants.RIGHT_BOUNDARY)
                .TakeWhile(_ => isMoving)
                .TakeUntilDisable(gameObject)
                .Subscribe(ChangeDirection);

            isMoving = true;
            body.velocity = direction * speed;
        }

        private void ChangeDirection(long _) {
            var currentDirection = body.velocity;
            currentDirection.Normalize();

            if (transform.position.x < ViewConstants.LEFT_BOUNDARY) {
                if (currentDirection == Vector3.left) {
                    body.velocity = Vector3.right * speed;
                }
            }
            else if (transform.position.x > ViewConstants.RIGHT_BOUNDARY) {
                if (currentDirection == Vector3.right) {
                    body.velocity = Vector3.left * speed;
                }
            }
        }
    }
}
