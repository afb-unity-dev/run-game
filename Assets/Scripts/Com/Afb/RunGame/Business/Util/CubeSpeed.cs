namespace Com.Afb.RunGame.Business.Util {
    public static class CubeSpeed {
        public static float Get(int level) {
            return level / 50f + 5;
        }
    }
}
