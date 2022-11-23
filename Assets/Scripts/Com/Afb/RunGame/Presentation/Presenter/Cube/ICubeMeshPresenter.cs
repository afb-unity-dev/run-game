using Com.Afb.RunGame.Business.Model;
using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICubeMeshPresenter {
        // Properties
        IReadOnlyReactiveProperty<float> XPosition { get; }
        IReadOnlyReactiveProperty<Vector3> Size { get; }
        IReadOnlyReactiveProperty<Color> Color { get; }
        IReadOnlyReactiveProperty<CubeCutModel> CubeCut { get; }
    }
}
