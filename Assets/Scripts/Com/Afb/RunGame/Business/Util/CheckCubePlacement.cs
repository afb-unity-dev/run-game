using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Util;
using UnityEngine;

namespace Com.Afb.RunGame.Business.Util {
    public static class CheckCubePlacement {
        public static CubeCutModel Check(CurrentCubeModel cube, float lastPosition, float lastWidth) {
            if (Mathf.Abs(lastPosition - cube.XPosition) < Constants.POSITION_THRESHOLD) {
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
