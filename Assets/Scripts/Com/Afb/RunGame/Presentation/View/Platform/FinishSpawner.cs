using UnityEngine;

namespace Com.Afb.RunGame.Presentation.View {
    public class FinishSpawner : GenericCubeSpawner<FinishView> {
        // Public Methods
        public override FinishView Spawn(float zPosition) {
            return viewPool.Spawn(parent, new Vector3(0, 0, zPosition));
        }
    }
}