using System.ComponentModel;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using DotNetEnv;
using System.Threading.Tasks;

namespace board_game
{
    class Game
    {
        private static Game instance = null;
        Agent agent = Agent.getInstance(DotNetEnv.Env.GetString("AZURE_API_KEY"), DotNetEnv.Env.GetString("AZURE_API_LOCATION"));
        Grid grille;
        Grid grilleIa;
        Grid grilleAttack;
        public Player player;
        private int level = 0;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public static Game getInstance()
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }

        private Game()
        {
            this.grille = new Grid();
            this.grilleIa = new Grid();
            this.grilleAttack = new Grid();
            this.player = new Player();
        }
        public void displayGrid()
        {
            Console.Write("____________\n\n");
            Console.Write($" {this.player.Name}'s  board  \n");
            Console.Write("____________\n\n");

            for (int k = 0; k < this.grille.Board.GetLength(1); k++)
            {
                switch (k)
                {
                    case 0:
                        Console.Write(" 0");
                        Console.Write("|");
                        break;
                    case 1:
                        Console.Write("1");
                        Console.Write("|");
                        break;
                    case 2:
                        Console.Write("2");
                        Console.Write("|");
                        break;
                    case 3:
                        Console.Write("3");
                        Console.Write("|");
                        break;
                    case 4:
                        Console.Write("4");
                        Console.Write("|");
                        break;
                    case 5:
                        Console.Write("5");
                        break;
                }
            }
            Console.Write("\n");
            for (int i = 0; i < this.grille.Board.GetLength(0); i++)
            {
                char letter = ' ';
                if (i == 0) letter = 'A';
                else if (i == 1) letter = 'B';
                else if (i == 2) letter = 'C';
                else if (i == 3) letter = 'D';
                else if (i == 4) letter = 'E';
                else if (i == 5) letter = 'F';
                Console.Write(letter);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                for (int j = 0; j < this.grille.Board.GetLength(1); j++)
                {
                    Console.Write(this.grille.Board[i, j]);
                    if (j != 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("|");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
        }
        public async Task initPlayer()
        {
            Console.WriteLine("Quel est votre nom ?");
            await agent.SynthesisToSpeakerAsync("Quel est votre nom ?");
            await agent.startListening();
            // Console.ReadLine();
        }
        public void initShip1()
        {
            List<Boat> boats = new List<Boat>();
            bool resultx = false;
            bool resulty = false;
            int x;
            int y;
            bool resultletter;
            char letter;
            char upperletter;
            Console.Write("\n Choose the location of your first Ship : Cuirasse ");
            do
            {
                string inputx;
                Console.Write("\n Enter the coordinates of x: ");
                inputx = Console.ReadLine();
                resultletter = char.TryParse(inputx, out letter);
                upperletter = char.ToUpper(letter);
                x = 6;
                if (resultletter == true && ((upperletter == 'A') || (upperletter == 'B') || (upperletter == 'C') ||
                 (upperletter == 'D') || (upperletter == 'E') || (upperletter == 'F')))
                {
                    resultx = true;
                    if (upperletter == 'A') x = 0;
                    else if (upperletter == 'B') x = 1;
                    else if (upperletter == 'C') x = 2;
                    else if (upperletter == 'D') x = 3;
                    else if (upperletter == 'E') x = 4;
                    else if (upperletter == 'F') x = 5;
                }
                //resultx = int.TryParse(inputx, out x);
                if (resultx == false || x > 5)
                {
                    Console.WriteLine(resultx + "t" + x);
                    Console.Write("\n Please Enter Letter between A and F Only.");
                }
                else
                {
                    Console.Write("\n you choose x = " + (x));
                    break;
                }
            } while (resultx == false || x > 5);

            do
            {
                string inputy;
                Console.WriteLine("\n Enter the coordinates of y: ");
                inputy = Console.ReadLine();
                resulty = int.TryParse(inputy, out y);
                if (resulty == false || y > 5)
                {
                    Console.WriteLine("Please Enter Numbers between 0 and 5 Only.");
                }
                else
                {
                    Console.WriteLine("you choose y = " + (y));
                    break;
                }
            } while (resulty == false || y > 5);

            BoatCuirasse boatCuirasse = new BoatCuirasse();

            Console.WriteLine("In which direction do you want the ship to be oriented ?");
            var t = 0;
            char input = ' ';
            if (x > 0 && x < 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for north, S for South, E for East, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type  S for South, W for West, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, E for East, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 0 && x < 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, S for South, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'E' || input == 'S' || input == 'N') t = 1;
                }
            }
            else if (x > 0 && x < 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, S for South, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;
            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x };
                    yy = new int[] { y, y - 1 };
                    boatCuirasse.X = xx;
                    boatCuirasse.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x };
                    yy = new int[] { y, y + 1 };
                    boatCuirasse.X = xx;
                    boatCuirasse.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1 };
                    yy = new int[] { y, y };
                    boatCuirasse.X = xx;
                    boatCuirasse.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1 };
                    yy = new int[] { y, y };
                    boatCuirasse.X = xx;
                    boatCuirasse.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grille.Board[boatCuirasse.X[0], boatCuirasse.Y[0]] = '1';
            this.grille.Board[boatCuirasse.X[1], boatCuirasse.Y[1]] = '1';
            //test getter
            Console.WriteLine(boatCuirasse.X[0].ToString() + ", " + boatCuirasse.Y[0].ToString());
            Console.WriteLine(boatCuirasse.X[1].ToString() + ", " + boatCuirasse.Y[1].ToString());
        }
        public void initShip2()
        {
            List<Boat> boats = new List<Boat>();

            bool resultx = false;
            bool resulty = false;
            int x;
            int y;
            bool resultletter;
            char letter;
            char upperletter;
            Console.Write("\n Choose the location of your second Ship : Destroyer ");
            do
            {
                do
                {
                    string inputx;
                    Console.Write("\n Enter the coordinates of x: ");
                    inputx = Console.ReadLine();
                    resultletter = char.TryParse(inputx, out letter);
                    upperletter = char.ToUpper(letter);
                    x = 6;
                    if (resultletter == true && ((upperletter == 'A') || (upperletter == 'B') || (upperletter == 'C') ||
                    (upperletter == 'D') || (upperletter == 'E') || (upperletter == 'F')))
                    {
                        resultx = true;
                        if (upperletter == 'A') x = 0;
                        else if (upperletter == 'B') x = 1;
                        else if (upperletter == 'C') x = 2;
                        else if (upperletter == 'D') x = 3;
                        else if (upperletter == 'E') x = 4;
                        else if (upperletter == 'F') x = 5;
                    }
                    //resultx = int.TryParse(inputx, out x);
                    if (resultx == false || x > 5)
                    {
                        Console.WriteLine(resultx + "t" + x);
                        Console.Write("\n Please Enter Letter between A and F Only.");
                    }
                    else
                    {
                        Console.Write("\n you choose x = " + (x));
                        break;
                    }
                } while (resultx == false || x > 5);

                do
                {
                    string inputy;
                    Console.WriteLine("\n Enter the coordinates of y: ");
                    inputy = Console.ReadLine();
                    resulty = int.TryParse(inputy, out y);
                    if (resulty == false || y > 5)
                    {
                        Console.WriteLine("Please Enter Numbers between 0 and 5 Only.");
                    }
                    else
                    {
                        Console.WriteLine("you choose y = " + (y));
                        break;
                    }
                } while (resulty == false || y > 5);
            } while (this.grille.Board[x, y].Equals('1'));


            BoatDestroyer boatDestroyer = new BoatDestroyer();

            Console.WriteLine("In which direction do you want the ship to be oriented ?");
            var t = 0;
            char input = ' ';
            if (x > 1 && x < 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for north, S for South, E for East, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type W for West, S for South, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, E for East, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 1 && x < 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, E for East, N for North");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'E' || input == 'S' || input == 'N') t = 1;
                }
            }
            else if (x > 1 && x < 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, S for South, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;

            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x, x };
                    yy = new int[] { y, y - 1, y - 2 };
                    boatDestroyer.X = xx;
                    boatDestroyer.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x, x };
                    yy = new int[] { y, y + 1, y + 2 };
                    boatDestroyer.X = xx;
                    boatDestroyer.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1, x + 2 };
                    yy = new int[] { y, y, y };
                    boatDestroyer.X = xx;
                    boatDestroyer.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1, x - 2 };
                    yy = new int[] { y, y, y };
                    boatDestroyer.X = xx;
                    boatDestroyer.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grille.Board[boatDestroyer.X[0], boatDestroyer.Y[0]] = '1';
            this.grille.Board[boatDestroyer.X[1], boatDestroyer.Y[1]] = '1';
            this.grille.Board[boatDestroyer.X[2], boatDestroyer.Y[2]] = '1';
            //test getter
            Console.WriteLine(boatDestroyer.X[2].ToString() + ", " + boatDestroyer.Y[2].ToString());
        }

        public void initShip3()
        {
            List<Boat> boats = new List<Boat>();

            bool resultx = false;
            bool resulty = false;
            int x;
            int y;
            bool resultletter;
            char letter;
            char upperletter;
            Console.Write("\n Choose the location of your third Ship : NuclearShip ");
            do
            {
                do
                {
                    string inputx;
                    Console.Write("\n Enter the coordinates of x: ");
                    inputx = Console.ReadLine();
                    resultletter = char.TryParse(inputx, out letter);
                    upperletter = char.ToUpper(letter);
                    x = 6;
                    if (resultletter == true && ((upperletter == 'A') || (upperletter == 'B') || (upperletter == 'C') ||
                    (upperletter == 'D') || (upperletter == 'E') || (upperletter == 'F')))
                    {
                        resultx = true;
                        if (upperletter == 'A') x = 0;
                        else if (upperletter == 'B') x = 1;
                        else if (upperletter == 'C') x = 2;
                        else if (upperletter == 'D') x = 3;
                        else if (upperletter == 'E') x = 4;
                        else if (upperletter == 'F') x = 5;
                    }
                    //resultx = int.TryParse(inputx, out x);
                    if (resultx == false || x > 5)
                    {
                        Console.WriteLine(resultx + "t" + x);
                        Console.Write("\n Please Enter Letter between A and F Only.");
                    }
                    else
                    {
                        Console.Write("\n you choose x = " + (x));
                        break;
                    }
                } while (resultx == false || x > 5);

                do
                {
                    string inputy;
                    Console.WriteLine("\n Enter the coordinates of y: ");
                    inputy = Console.ReadLine();
                    resulty = int.TryParse(inputy, out y);
                    if (resulty == false || y > 5)
                    {
                        Console.WriteLine("Please Enter Numbers between 0 and 5 Only.");
                    }
                    else
                    {
                        Console.WriteLine("you choose y = " + (y));
                        break;
                    }
                } while (resulty == false || y > 5);
            } while (this.grille.Board[x, y].Equals('1'));

            BoatNuclearShip boatNuclearShip = new BoatNuclearShip();

            Console.WriteLine("In which direction do you want the ship to be oriented ?");
            var t = 0;
            char input = ' ';
            if (x <= 2 && y <= 2)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, E for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x >= 3 && y >= 3)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for north, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x >= 3 && y <= 2)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type N for North, 2 for East");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x <= 2 && y >= 3)
            {
                while (t == 0)
                {
                    Console.WriteLine("Type S for South, W for West");
                    input = char.ToUpper(Console.ReadKey().KeyChar);
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;

            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x, x, x };
                    yy = new int[] { y, y - 1, y - 2, y - 3 };
                    boatNuclearShip.X = xx;
                    boatNuclearShip.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x, x, x };
                    yy = new int[] { y, y + 1, y + 2, y + 3 };
                    boatNuclearShip.X = xx;
                    boatNuclearShip.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1, x + 2, x + 3 };
                    yy = new int[] { y, y, y, y };
                    boatNuclearShip.X = xx;
                    boatNuclearShip.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1, x - 2, x - 3 };
                    yy = new int[] { y, y, y, y };
                    boatNuclearShip.X = xx;
                    boatNuclearShip.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grille.Board[boatNuclearShip.X[0], boatNuclearShip.Y[0]] = '1';
            this.grille.Board[boatNuclearShip.X[1], boatNuclearShip.Y[1]] = '1';
            this.grille.Board[boatNuclearShip.X[2], boatNuclearShip.Y[2]] = '1';
            this.grille.Board[boatNuclearShip.X[3], boatNuclearShip.Y[3]] = '1';
            //test getter
            Console.WriteLine(boatNuclearShip.X[3].ToString() + ", " + boatNuclearShip.Y[3].ToString());
        }

        public void PlayerInput(Grid grid)
        {
            Console.WriteLine("Enter coordinates to Attack on ship");
            Console.WriteLine("Cordinate X:");
            var temp = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Cordinate Y:");
            var temp1 = Convert.ToInt32(Console.ReadLine());
        }

        public async Task<bool> isWin()
        {
            for (int i = 0; i < this.grille.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.grille.Board.GetLength(1); j++)
                {
                    if (this.grilleIa.Board[i, j] == '1')
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine("#########################");
            Console.WriteLine("#    Vous avez gagné    #");
            Console.WriteLine("#########################");
            await agent.SynthesisToSpeakerAsync("Vous avez gagné !");
            this.player.Win = true;
            save();
            return true;
        }
        public async Task<bool> isIaWin()
        {
            for (int i = 0; i < this.grille.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.grille.Board.GetLength(1); j++)
                {
                    if (this.grille.Board[i, j] == '1')
                    {
                        return false;
                    }
                }
            }
            this.player.Win = false;
            Console.WriteLine("#########################");
            Console.WriteLine("#       You loose  :(   #");
            Console.WriteLine("#########################");
            await agent.SynthesisToSpeakerAsync("Vous avez perdu !");
            return true;
        }

        public void displayGridIa()
        {
            for (int k = 0; k < this.grille.Board.GetLength(1); k++)
            {
                switch (k)
                {
                    case 0:
                        Console.Write(" 0");
                        Console.Write("|");
                        break;
                    case 1:
                        Console.Write("1");
                        Console.Write("|");
                        break;
                    case 2:
                        Console.Write("2");
                        Console.Write("|");
                        break;
                    case 3:
                        Console.Write("3");
                        Console.Write("|");
                        break;
                    case 4:
                        Console.Write("4");
                        Console.Write("|");
                        break;
                    case 5:
                        Console.Write("5");
                        break;
                }
            }
            Console.Write("\n");
            for (int i = 0; i < this.grilleIa.Board.GetLength(0); i++)
            {
                char letter = ' ';
                if (i == 0) letter = 'A';
                else if (i == 1) letter = 'B';
                else if (i == 2) letter = 'C';
                else if (i == 3) letter = 'D';
                else if (i == 4) letter = 'E';
                else if (i == 5) letter = 'F';
                Console.Write(letter);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                for (int j = 0; j < this.grilleIa.Board.GetLength(1); j++)
                {
                    Console.Write(this.grilleIa.Board[i, j]);
                    if (j != 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("|");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
        }

        public void displayGridAttack()
        {
            Console.Write("_____________\n\n");
            Console.Write(" Enemy board \n");
            Console.Write("_____________\n\n");

            for (int k = 0; k < this.grilleAttack.Board.GetLength(1); k++)
            {
                switch (k)
                {
                    case 0:
                        Console.Write(" 0");
                        Console.Write("|");
                        break;
                    case 1:
                        Console.Write("1");
                        Console.Write("|");
                        break;
                    case 2:
                        Console.Write("2");
                        Console.Write("|");
                        break;
                    case 3:
                        Console.Write("3");
                        Console.Write("|");
                        break;
                    case 4:
                        Console.Write("4");
                        Console.Write("|");
                        break;
                    case 5:
                        Console.Write("5");
                        break;
                }
            }
            Console.Write("\n");
            for (int i = 0; i < this.grilleAttack.Board.GetLength(0); i++)
            {
                char letter = ' ';
                if (i == 0) letter = 'A';
                else if (i == 1) letter = 'B';
                else if (i == 2) letter = 'C';
                else if (i == 3) letter = 'D';
                else if (i == 4) letter = 'E';
                else if (i == 5) letter = 'F';
                Console.Write(letter);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                for (int j = 0; j < this.grilleAttack.Board.GetLength(1); j++)
                {
                    Console.Write(this.grilleAttack.Board[i, j]);
                    if (j != 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("|");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
        }
        public void initShipIa1()
        {

            List<Boat> boats = new List<Boat>();
            Random rand = new Random();
            var x = rand.Next(0, 5);
            var y = rand.Next(0, 5);
            // string[] letter = new string[]{"n","s","e","w"} // tu rajouteras le reste

            BoatCuirasse boatCuirasseIA = new BoatCuirasse();
            var t = 0;
            char input = ' ';
            if (x > 0 && x < 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "NSEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "SEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y > 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "NEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 0 && x < 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "NSE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'E' || input == 'S' || input == 'N') t = 1;
                }
            }
            else if (x > 0 && x < 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    var possibilities = "NSW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    var possibilities = "SW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            else if (x == 0 && x < 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "SE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y == 0 && y < 5)
            {
                while (t == 0)
                {
                    var possibilities = "NE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x > 0 && x == 5 && y > 0 && y == 5)
            {
                while (t == 0)
                {
                    var possibilities = "NW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;
            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x };
                    yy = new int[] { y, y - 1 };
                    boatCuirasseIA.X = xx;
                    boatCuirasseIA.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x };
                    yy = new int[] { y, y + 1 };
                    boatCuirasseIA.X = xx;
                    boatCuirasseIA.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1 };
                    yy = new int[] { y, y };
                    boatCuirasseIA.X = xx;
                    boatCuirasseIA.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1 };
                    yy = new int[] { y, y };
                    boatCuirasseIA.X = xx;
                    boatCuirasseIA.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grilleIa.Board[boatCuirasseIA.X[0], boatCuirasseIA.Y[0]] = '1';
            this.grilleIa.Board[boatCuirasseIA.X[1], boatCuirasseIA.Y[1]] = '1';
        }
        public void initShipIa2()
        {
            List<Boat> boats = new List<Boat>();
            Random rand = new Random();
            var x = rand.Next(0, 5);
            var y = rand.Next(0, 5);

            BoatDestroyer boatDestroyerIa = new BoatDestroyer();

            var t = 0;
            char input = ' ';
            if (x > 1 && x < 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "NSEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "SEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y > 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "NEW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'E' || input == 'W') t = 1;
                }
            }
            else if (x > 1 && x < 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "NSE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'E' || input == 'S' || input == 'N') t = 1;
                }
            }
            else if (x > 1 && x < 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    var possibilities = "NSW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    var possibilities = "SW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            else if (x <= 1 && x < 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "SE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y <= 1 && y < 4)
            {
                while (t == 0)
                {
                    var possibilities = "NE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x > 1 && x >= 4 && y > 1 && y >= 4)
            {
                while (t == 0)
                {
                    var possibilities = "NW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;

            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x, x };
                    yy = new int[] { y, y - 1, y - 2 };
                    boatDestroyerIa.X = xx;
                    boatDestroyerIa.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x, x };
                    yy = new int[] { y, y + 1, y + 2 };
                    boatDestroyerIa.X = xx;
                    boatDestroyerIa.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1, x + 2 };
                    yy = new int[] { y, y, y };
                    boatDestroyerIa.X = xx;
                    boatDestroyerIa.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1, x - 2 };
                    yy = new int[] { y, y, y };
                    boatDestroyerIa.X = xx;
                    boatDestroyerIa.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grilleIa.Board[boatDestroyerIa.X[0], boatDestroyerIa.Y[0]] = '1';
            this.grilleIa.Board[boatDestroyerIa.X[1], boatDestroyerIa.Y[1]] = '1';
            this.grilleIa.Board[boatDestroyerIa.X[2], boatDestroyerIa.Y[2]] = '1';
        }

        public void initShipIa3()
        {
            List<Boat> boats = new List<Boat>();
            Random rand = new Random();
            var x = rand.Next(0, 5);
            var y = rand.Next(0, 5);

            BoatNuclearShip boatNuclearShipIa = new BoatNuclearShip();

            Console.WriteLine("In which direction do you want the ship to be oriented ?");
            var t = 0;
            char input = ' ';
            if (x <= 2 && y <= 2)
            {
                while (t == 0)
                {
                    var possibilities = "SE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'E') t = 1;
                }
            }
            else if (x >= 3 && y >= 3)
            {
                while (t == 0)
                {
                    var possibilities = "NW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'W') t = 1;
                }
            }
            else if (x >= 3 && y <= 2)
            {
                while (t == 0)
                {
                    var possibilities = "NE";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'N' || input == 'E') t = 1;
                }
            }
            else if (x <= 2 && y >= 3)
            {
                while (t == 0)
                {
                    var possibilities = "SW";
                    input = possibilities[rand.Next(possibilities.Length)];
                    if (input == 'S' || input == 'W') t = 1;
                }
            }
            int[] xx;
            int[] yy;

            switch (input)
            {
                case 'W':
                    xx = new int[] { x, x, x, x };
                    yy = new int[] { y, y - 1, y - 2, y - 3 };
                    boatNuclearShipIa.X = xx;
                    boatNuclearShipIa.Y = yy;
                    break;
                case 'E':
                    xx = new int[] { x, x, x, x };
                    yy = new int[] { y, y + 1, y + 2, y + 3 };
                    boatNuclearShipIa.X = xx;
                    boatNuclearShipIa.Y = yy;
                    break;
                case 'S':
                    xx = new int[] { x, x + 1, x + 2, x + 3 };
                    yy = new int[] { y, y, y, y };
                    boatNuclearShipIa.X = xx;
                    boatNuclearShipIa.Y = yy;
                    break;
                case 'N':
                    xx = new int[] { x, x - 1, x - 2, x - 3 };
                    yy = new int[] { y, y, y, y };
                    boatNuclearShipIa.X = xx;
                    boatNuclearShipIa.Y = yy;
                    break;
                default:
                    Console.WriteLine("Type N for North, E for East, W for West");
                    break;
            }
            this.grilleIa.Board[boatNuclearShipIa.X[0], boatNuclearShipIa.Y[0]] = '1';
            this.grilleIa.Board[boatNuclearShipIa.X[1], boatNuclearShipIa.Y[1]] = '1';
            this.grilleIa.Board[boatNuclearShipIa.X[2], boatNuclearShipIa.Y[2]] = '1';
            this.grilleIa.Board[boatNuclearShipIa.X[3], boatNuclearShipIa.Y[3]] = '1';
            //test getter
            Console.WriteLine(boatNuclearShipIa.X[3].ToString() + ", " + boatNuclearShipIa.Y[3].ToString());
        }

        /*
            Player turn attack
        */
        public async void Attack(int x = 0, int y = 0)
        {
            // bool resultx = false;
            // bool resulty = false;
            // bool resultletter;
            // char letter;
            // char upperletter;
            // Console.Write("\n Choose attack location");

            // do
            // {
            //     string inputx;
            //     Console.Write("\n Enter the coordinates of x: ");
            //     inputx = Console.ReadLine();
            //     resultletter = char.TryParse(inputx, out letter);
            //     upperletter = char.ToUpper(letter);
            //     x = 6;
            //     if (resultletter == true && ((upperletter == 'A') || (upperletter == 'B') || (upperletter == 'C') ||
            //     (upperletter == 'D') || (upperletter == 'E') || (upperletter == 'F')))
            //     {
            //         resultx = true;
            //         if (upperletter == 'A') x = 0;
            //         else if (upperletter == 'B') x = 1;
            //         else if (upperletter == 'C') x = 2;
            //         else if (upperletter == 'D') x = 3;
            //         else if (upperletter == 'E') x = 4;
            //         else if (upperletter == 'F') x = 5;
            //     }
            //     //resultx = int.TryParse(inputx, out x);
            //     if (resultx == false || x > 5)
            //     {
            //         Console.WriteLine(resultx + "t" + x);
            //         Console.Write("\n Please Enter Letter between A and F Only.");
            //     }
            //     else
            //     {
            //         Console.Write("\n you choose x = " + (x));
            //         break;
            //     }
            // } while (resultx == false || x > 5);

            // do
            // {
            //     string inputy;
            //     Console.WriteLine("\n Enter the coordinates of y: ");
            //     inputy = Console.ReadLine();
            //     resulty = int.TryParse(inputy, out y);
            //     if (resulty == false || y > 5)
            //     {
            //         Console.WriteLine("Please Enter Numbers between 0 and 5 Only.");
            //     }
            //     else
            //     {
            //         Console.WriteLine("you choose y = " + (y));
            //         break;
            //     }
            // } while (resulty == false || y > 5);

            if (this.grilleIa.Board[x, y].Equals('1'))
            {
                this.grilleIa.Board[x, y] = '2';
                this.grilleAttack.Board[x, y] = 'X';
                await agent.SynthesisToSpeakerAsync($"Bateau touché en {x}; {y}");
                this.player.Score += 2;
            }
            else if (this.grilleAttack.Board[x, y].Equals('o') || this.grilleAttack.Board[x, y].Equals('X'))
            {
                Console.WriteLine("You have already hit this case");
            }
            else if (this.grilleIa.Board[x, y].Equals(' '))
            {
                this.grilleAttack.Board[x, y] = 'o';
                await agent.SynthesisToSpeakerAsync($"Coup manqué en {x};{y}");
                this.player.Score--;
            }
        }

        /*
            Ia turn attack
        */
        public async void AttackIa()
        {
            while (true)
            {
                Random rand = new Random();
                var x = rand.Next(0, 6);
                var y = rand.Next(0, 6);
                await agent.SynthesisToSpeakerAsync($"L'ennemie attaque en {x};{y}");

                if (this.grille.Board[x, y].Equals('1'))
                {
                    this.grille.Board[x, y] = 'X';
                    await agent.SynthesisToSpeakerAsync("L'ennemie vous a touché");
                    break;
                }
                else if (this.grille.Board[x, y].Equals('o') || this.grille.Board[x, y].Equals('X'))
                {

                }
                else if (this.grille.Board[x, y].Equals(' '))
                {
                    this.grille.Board[x, y] = 'o';
                    await agent.SynthesisToSpeakerAsync("L'ennemie vous a manqué");
                    break;
                }
            }
        }

        private Boolean getBoatPosition()
        {
            for (int i = 0; i < this.grilleIa.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.grilleIa.Board.GetLength(1); j++)
                {
                    if (this.grilleIa.Board[i, j].Equals('1')) return true;
                    else return false;
                }
            }
            return false;
        }
        public void save()
        {
            string text = "";
            do
            {
                Console.WriteLine("\n do you want to save ?\nEnter yes or no.");
                text = Console.ReadLine();
                if (text != "yes" && text != "no")
                {
                    Console.WriteLine("Please Enter yes or no");
                }
                else if (text == "yes")
                {
                    Console.WriteLine("save success");
                    Save save = new Save();
                    save.WriteXML(this.player);
                    break;
                }
                else if (text == "no")
                {
                    break;
                }
            } while (text != "yes" && text != "no");
        }
    }
}

