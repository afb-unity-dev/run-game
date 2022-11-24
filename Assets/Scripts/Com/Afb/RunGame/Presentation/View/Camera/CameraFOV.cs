using UnityEngine;

namespace Com.Afb.RunGame.Presentation.View {
    [ExecuteAlways]
    public class CameraFOV : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private float minFov = 60;
        [SerializeField]
        private float maxFov = 80;

        // Constants
        private const float IPAD_ASPECT = 3f / 4f;
        private const float IPHONE_ASPECT = 18f / 39f;

        // Unity Methods
        private void Awake() {
            SetFOV();
        }

#if UNITY_EDITOR
        private void Update() {
            if (!Application.isPlaying) {
                SetFOV();
            }
        }
#endif

        // Private Methods
        private void SetFOV() {
            var camera = GetComponent<Camera>();

            float lerp = Mathf.InverseLerp(IPAD_ASPECT, IPHONE_ASPECT, camera.aspect);
            float fov = Mathf.Lerp(minFov, maxFov, lerp);
            fov = Mathf.Clamp(fov, minFov, maxFov);
            camera.fieldOfView = fov;
        }
    }
}
