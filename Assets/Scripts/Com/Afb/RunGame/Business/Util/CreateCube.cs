using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Util;
using UnityEngine;

namespace Com.Afb.RunGame.Business.Util {
    public static class CreateCube {
        public static CurrentCubeModel Create(Vector3 size) {
            var currentCube = new CurrentCubeModel();
            currentCube.Size = size;
            currentCube.Color = GetColor();
            currentCube.XPosition = GetXPosition();
            currentCube.IsMoving = true;
            return currentCube;
        }

        private static Color GetColor() {
            return Random.ColorHSV();
        }

        private static float GetXPosition() {
            return Random.Range(0, 2) == 0 ?
                Constants.LEFT_BOUNDARY :
                Constants.RIGHT_BOUNDARY;
        }
    }
}