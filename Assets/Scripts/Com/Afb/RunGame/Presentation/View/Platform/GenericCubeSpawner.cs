using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public abstract class GenericCubeSpawner<TView> : MonoBehaviour where TView :
        Component, IPoolable<Transform, Vector3> {

        // Serialize Fields
        [SerializeField]
        protected Transform parent;

        // Dependencies
        [Inject]
        protected MonoPoolableMemoryPool<Transform, Vector3, TView> viewPool;

        // Public Abstract Methods
        public abstract TView Spawn(float zPosition);

        // Public Methods
        public void DespawnLast(int amount = -1) {
            List<TView> cubes = new List<TView>();

            if (amount < 0) {
                amount = parent.childCount;
            }

            int lastIndex = parent.childCount - 1;

            amount = Mathf.Min(amount, parent.childCount);

            for (int i = 0; i < amount; i++) {
                int index = lastIndex - i;
                var cube = parent.GetChild(index).GetComponent<TView>();
                cubes.Add(cube);
            }

            foreach (var cube in cubes) {
                viewPool.Despawn(cube);
            }
        }

        public void MoveChildren(float zPos) {
            foreach (Transform child in parent) {
                var localPos = child.localPosition;
                localPos.z += zPos;
                child.localPosition = localPos;
            }
        }
    }
}