namespace board_game
{
    class VRPluginFactory
    {
        public static IVRPlugin GetPlugin(Agent agent, Action action)
        {
            if (action.Plugin.Equals("playerAction")) return new VRPlayerActionPlugin(agent);
            if (action.Plugin.Equals("getPlayerName")) return new VRPluginPlayerName(agent);
            else return null;
        }
    }
}