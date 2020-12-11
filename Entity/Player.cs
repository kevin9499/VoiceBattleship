using System;

namespace board_game
{
    [Serializable()]
    public class Player
    {
        private String name;
        public String Name
        {
            get { return name; }
            set { this.name = value; }
        }
        private int score = 0;
        public int Score
        {
            get { return score; }
            set { this.score = value; }
        }

        private bool win = false;

        public bool Win
        {
            get { return win; }
            set { win = value; }
        }
        public Player()
        {
        }
        public Player(String name)
        {
            this.name = name;
            this.score = 0;
        }
    }
}
