using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace board_game
{
    class Action
    {
        private String value;
        private String plugin;
        private ArrayList args;

        public Action(string value, string plugin)
        {
            this.value = value;
            this.plugin = plugin;
            this.args = new ArrayList();
        }

        public String Value { get => value; }

        public String Plugin { get => plugin; }
        public ArrayList Args { get => args; }

        public static ArrayList GetAllActions()
        {
            ArrayList list = new ArrayList();
            list.Add(new Action(@"(Tirez|tirez|tiré|Attaquer|attaquery) en (?<positionGroup>[a-fA-F] [0-6a-z][a-z]{0,1})", "playerAction"));
            list.Add(new Action(@"Mon nom est (?<playerName>[aA-zZ]+)", "getPlayerName"));
            return list;
        }

        public static Action searchAction(string text)
        {
            foreach (Action act in GetAllActions())
            {
                Match result = Regex.Match(text, act.Value);
                if (result.Success)
                {
                    Group pos = result.Groups["positionGroup"];
                    Group pseudo = result.Groups["playerName"];
                    act.args.Add(pos.Value);
                    act.args.Add(pseudo.Value);
                    return act;
                }
            }
            return null;
        }
    }
}