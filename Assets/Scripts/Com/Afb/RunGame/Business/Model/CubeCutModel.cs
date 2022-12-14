using UnityEngine;

namespace Com.Afb.RunGame.Business.Model {
    public class CubeCutModel {
        public float XPosition { get; set; }
        public Vector3 Size { get; set; }
        public Color Color { get; set; }
        public int Direction { get; set; } // 1 left, -1 right
    }
}
