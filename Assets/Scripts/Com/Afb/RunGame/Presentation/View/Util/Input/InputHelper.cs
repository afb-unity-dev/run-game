using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View.Util.InputHelper {
    public class InputHelper : IInitializable, IClickEvent {
        // Readonly Properties
        private readonly IObservable<InputData> click;

        // Private Properties
        private int dragThreshold;

        // Public Properties
        public IObservable<InputData> Click => click;

        public InputHelper() {
            IObservable<InputData> down = CreateDown();
            IObservable<InputData> up = CreateUp();

            click = InitializeClick(down, up);
        }

        public void Initialize() {
            int defaultValue = EventSystem.current.pixelDragThreshold;
            dragThreshold = Mathf.Max(defaultValue, (int)(defaultValue * Screen.dpi / 160f));
        }

        private IObservable<InputData> CreateDown() {
            return Observable
                .EveryUpdate()
                // If mouse down
                .Where(_ => {
                    return Input.GetMouseButtonDown(0);
                })
                // Map to InputData
                .Select(_ => {
                    Vector2 pos = Input.mousePosition;

                    return new InputData {
                        state = InputState.Begin,
                        position = pos
                    };
                });
        }

        private IObservable<InputData> CreateUp() {
            return Observable
                .EveryUpdate()
                // If mouse up
                .Where(_ => {
                    return Input.GetMouseButtonUp(0);
                })
                // Map to InputData
                .Select(_ => {
                    Vector2 pos = Input.mousePosition;

                    return new InputData {
                        state = InputState.End,
                        position = pos
                    };
                });
        }

        private IObservable<InputData> InitializeClick(IObservable<InputData> down, IObservable<InputData> up) {
            InputData begin = new InputData();
            // Merge down and up events
            return Observable.Merge(down, up)
                .Where(input => {
                    // If begin save begin position
                    if (input.state == InputState.Begin) {
                        begin = input;
                        return false;
                    }
                    // If end check distance
                    else if (input.state == InputState.End) {
                        var distance = Vector2.Distance(input.position, begin.position);
                        return distance < dragThreshold;
                    }

                    // Should not reach here
                    return false;
                    
                });
        }
    }
}

