namespace board_game {
    class IAFactory {
        public static IA GetIA(Game myGame) {
            if (myGame.Level == 0) return new IALevelOne();
            else return null;
        }
    }
}