using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Util;
using UnityEngine;

namespace Com.Afb.RunGame.Business.Util {
    public static class CheckCubePlacement {
        // Public Functions
        public static CubeCutModel Check(CurrentCubeModel cube, float lastPosition, float lastWidth) {
            float index = Mathf.InverseLerp(0, Constants.CUBE_WIDTH, cube.Size.x);
            float threshold = Mathf.Lerp(Constants.POSITION_THRESHOLD_MIN,
                Constants.POSITION_THRESHOLD_MAX, index);

            if (Mathf.Abs(lastPosition - cube.XPosition) < threshold) {
                cube.XPosition = lastPosition;
            }

            float currentPosition = cube.XPosition;

            float currentWidth = cube.Size.x;

            float prevLeft = lastPosition - lastWidth / 2;
            float prevRight = lastPosition + lastWidth / 2;

            float currentLeft = currentPosition - currentWidth / 2;
            float currentRight = currentPosition + currentWidth / 2;

            CubeCutModel cubeCutModel = null;

            // If left
            if (currentLeft < prevLeft) {
                cubeCutModel = CutCube.Cut(cube, prevLeft, currentRight, Constants.CUT_DIRECTION_LEFT);
            }
            // If right
            else if (currentRight > prevRight) {
                cubeCutModel = CutCube.Cut(cube, currentLeft, prevRight, Constants.CUT_DIRECTION_RIGHT);
            }

            return cubeCutModel;
        }
    }
}
