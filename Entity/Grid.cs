using System;

namespace board_game
{
    class Grid
    {
        private char[,] board = new char[,]{ { ' ', ' ', ' ', ' ', ' ', ' '}, { ' ', ' ', ' ', ' ', ' ', ' ' }, { ' ', ' ', ' ', ' ', ' ', ' '}, { ' ', ' ', ' ', ' ', ' ', ' '}, { ' ', ' ', ' ', ' ', ' ', ' '}, { ' ', ' ', ' ', ' ', ' ', ' '} };
        public char[,] Board{
            get{ return board;}
            set{ board = value;}
        }
        public Grid(){

        }
    }
}