using System;
using System.Collections;
using System.Threading.Tasks;

namespace board_game
{
    class VRPluginPlayerName : IVRPlugin
    {
        Game myGame = Game.getInstance();
        Agent agent;
        String playerName;

        public VRPluginPlayerName(Agent agent)
        {
            this.agent = agent;
        }

        public async Task dispatchAction(ArrayList args)
        {
            String playerName = args[1].ToString();
            myGame.player.Name = playerName;
            await agent.SynthesisToSpeakerAsync($"Bonjour {playerName}");
            await agent.stopListening();
        }
    }
}