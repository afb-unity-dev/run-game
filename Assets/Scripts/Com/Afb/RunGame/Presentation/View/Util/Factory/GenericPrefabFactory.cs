using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View.Util.Factory {
    public class GenericPrefabFactory<TValue> : IFactory<TValue> {
        // Dependencies
        [Inject]
        private DiContainer container;

        // Private Properties
        private GameObject prefab;
        private Transform parent;

        // Constructor
        public GenericPrefabFactory(GameObject prefab, Transform parent = null) {
            this.prefab = prefab;
            this.parent = parent;
        }

        // Public Methods
        public TValue Create() {
            TValue view = container.InstantiatePrefabForComponent<TValue>(prefab, parent);
            return view;
        }
    }
}

