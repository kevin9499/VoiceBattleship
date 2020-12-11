using System;
using System.Threading.Tasks;
using System.Collections;

namespace board_game
{
    class VRPlayerActionPlugin : IVRPlugin
    {
        Agent agent;
        int x;
        int y;

        public VRPlayerActionPlugin(Agent agent)
        {
            this.agent = agent;
        }

        public async Task dispatchAction(ArrayList args)
        {
            string text = args[0].ToString();
            string[] splitedResult = text.Split(new string[] { "en" }, StringSplitOptions.None);
            string[] getXY = text.Split(new string[] { " " }, StringSplitOptions.None);

            Console.WriteLine($"x: {getXY[0]}, y: {getXY[1]},");
            if (getXY[1] == "un") y = 1;
            else if (getXY[1] == "deux") y = 2;
            else if (getXY[1] == "trois") y = 3;
            else if (getXY[1] == "quatre") y = 4;
            else if (getXY[1] == "cinq") y = 5;
            else if (getXY[1] == "six") y = 6;
            else y = Convert.ToInt32(getXY[1]);

            Char letterX = char.ToLower(Convert.ToChar(getXY[0]));
            Game myGame = Game.getInstance();
            if (letterX == 'a') x = 0;
            else if (letterX == 'b') x = 1;
            else if (letterX == 'c') x = 2;
            else if (letterX == 'd') x = 3;
            else if (letterX == 'e') x = 4;
            else if (letterX == 'f') x = 5;
            do
            {
                await agent.SynthesisToSpeakerAsync($"Vous avez attaqué en {letterX};{y}");
                myGame.Attack(x, y);
                myGame.AttackIa();
                myGame.displayGrid();
                myGame.displayGridAttack();
                Console.WriteLine("Quel est votre prochain coup ?");
                if (myGame.isWin().Result || myGame.isIaWin().Result)
                {
                    break;
                }
                else
                {
                    await agent.startListening();
                    Console.ReadLine();
                }
            } while (!myGame.isIaWin().Result && !myGame.isWin().Result);
        }
    }
}