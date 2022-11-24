using UnityEngine;

namespace Com.Afb.RunGame.Business.Util {
    public static class PlatformTarget {
        public static int Get(int level) {
            return (int)Mathf.Round(level / 2 + 1);
        }
    }
}
