using System;

namespace Com.Afb.RunGame.Presentation.View.Util.InputHelper {
    public interface IClickEvent {
        public IObservable<InputData> Click { get; }
    }
}