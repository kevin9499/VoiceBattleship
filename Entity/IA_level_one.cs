using System.Collections.Generic;
using System;

namespace board_game
{
    class IALevelOne : IA
    {
        private int x;
        private int y;
        Random rand;
        private List<int> action;
        private List<List<int>> triedPositions;
        public IALevelOne()
        {
        }

        private List<int> chooseRandomPositions()
        {
            rand = new Random();
            x = rand.Next(0, 5);
            y = rand.Next(0, 5);
            action.Add(x);
            action.Add(y);
            return action;
        }

        private List<int> generateUniquePosition()
        {
            action = chooseRandomPositions();
            if (triedPositions.Contains(action)) generateUniquePosition();
            else
            {
                triedPositions.Add(action);
                return action;
            }
            return null;
        }
        public List<int> playComputerTurn()
        {
            action = generateUniquePosition();
            return action;
        }
    }
}