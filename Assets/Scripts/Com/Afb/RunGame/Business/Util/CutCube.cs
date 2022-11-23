using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Util;
using UnityEngine;

namespace Com.Afb.RunGame.Business.Util {
    public static class CutCube {
        // Public Functions
        public static CubeCutModel Cut(CurrentCubeModel cube, float left, float right, int cutDirection) {
            float newWidth = Mathf.Max(right - left, 0);
            float cutWidth = cube.Size.x - newWidth;

            Vector3 newSize = cube.Size;
            newSize.x = newWidth;

            cube.Size = newSize;
            cube.XPosition = left + newWidth / 2;

            var cubeCutModel = new CubeCutModel();
            cubeCutModel.Size = new Vector3(cutWidth, cube.Size.y, cube.Size.z);
            cubeCutModel.Color = cube.Color;
            cubeCutModel.Direction = cutDirection;

            if (cutDirection <= Constants.CUT_DIRECTION_RIGHT) {
                cubeCutModel.XPosition = right + cutWidth / 2;
            }
            else {
                cubeCutModel.XPosition = left - cutWidth / 2;
            }

            return cubeCutModel;
        }
    }
}