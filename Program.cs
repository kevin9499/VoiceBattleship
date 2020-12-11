using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DotNetEnv;

namespace board_game
{
    class Program
    {
        static Agent warshipAgent = Agent.getInstance(DotNetEnv.Env.GetString("AZURE_API_KEY"), DotNetEnv.Env.GetString("AZURE_API_LOCATION"));
        static async Task Main(string[] args)
        {
            // load env variables
            DotNetEnv.Env.Load();
            var title = @"
            ****************************************************************************************************************************************************************************************************************************************************************************************
            *    .S     S.     sSSs  S.        sSSs    sSSs_sSSs     .S_SsS_S.     sSSs        sdSS_SSSSSSbs    sSSs_sSSs           .S_SSSs     .S_SSSs    sdSS_SSSSSSbs  sdSS_SSSSSSbs  S.        sSSs    sSSs   .S    S.    .S   .S_sSSs            sSSSSs   .S_SSSs     .S_SsS_S.     sSSs      *
            *    .SS     SS.   d%%SP  SS.      d%%SP   d%%SP~YS%%b   .SS~S*S~SS.   d%%SP        YSSS~S%SSSSSP   d%%SP~YS%%b         .SS~SSSSS   .SS~SSSSS   YSSS~S%SSSSSP  YSSS~S%SSSSSP  SS.      d%%SP   d%%SP  .SS    SS.  .SS  .SS~YS%%b          d%%%%SP  .SS~SSSSS   .SS~S*S~SS.   d%%SP     *
            *    S%S     S%S  d%S'    S%S     d%S'    d%S'     `S%b  S%S `Y' S%S  d%S'               S%S       d%S'     `S%b        S%S   SSSS  S%S   SSSS       S%S            S%S       S%S     d%S'    d%S'    S%S    S%S  S%S  S%S   `S%b        d%S'      S%S   SSSS  S%S `Y' S%S  d%S'       *
            *    S%S     S%S  S%S     S%S     S%S     S%S       S%S  S%S     S%S  S%S                S%S       S%S       S%S        S%S    S%S  S%S    S%S       S%S            S%S       S%S     S%S     S%|     S%S    S%S  S%S  S%S    S%S        S%S       S%S    S%S  S%S     S%S  S%S        *
            *    S%S     S%S  S&S     S&S     S&S     S&S       S&S  S%S     S%S  S&S                S&S       S&S       S&S        S%S SSSS%P  S%S SSSS%S       S&S            S&S       S&S     S&S     S&S     S%S SSSS%S  S&S  S%S    d*S        S&S       S%S SSSS%S  S%S     S%S  S&S        *
            *    S&S     S&S  S&S_Ss  S&S     S&S     S&S       S&S  S&S     S&S  S&S_Ss             S&S       S&S       S&S        S&S  SSSY   S&S  SSS%S       S&S            S&S       S&S     S&S_Ss  Y&Ss    S&S  SSS&S  S&S  S&S   .S*S        S&S       S&S  SSS%S  S&S     S&S  S&S_Ss     *
            *    S&S     S&S  S&S~SP  S&S     S&S     S&S       S&S  S&S     S&S  S&S~SP             S&S       S&S       S&S        S&S    S&S  S&S    S&S       S&S            S&S       S&S     S&S~SP  `S&&S   S&S    S&S  S&S  S&S_sdSSS         S&S       S&S    S&S  S&S     S&S  S&S~SP     *
            *    S&S     S&S  S&S     S&S     S&S     S&S       S&S  S&S     S&S  S&S                S&S       S&S       S&S        S&S    S&S  S&S    S&S       S&S            S&S       S&S     S&S       `S*S  S&S    S&S  S&S  S&S~YSSY          S&S sSSs  S&S    S&S  S&S     S&S  S&S        *
            *    S*S     S*S  S*b     S*b     S*b     S*b       d*S  S*S     S*S  S*b                S*S       S*b       d*S        S*S    S&S  S*S    S&S       S*S            S*S       S*b     S*b        l*S  S*S    S*S  S*S  S*S               S*b `S%%  S*S    S&S  S*S     S*S  S*b        *
            *    S*S  .  S*S  S*S.    S*S.    S*S.    S*S.     .S*S  S*S     S*S  S*S.               S*S       S*S.     .S*S        S*S    S*S  S*S    S*S       S*S            S*S       S*S.    S*S.      .S*P  S*S    S*S  S*S  S*S               S*S   S%  S*S    S*S  S*S     S*S  S*S.       *
            *    S*S_sSs_S*S   SSSbs   SSSbs   SSSbs   SSSbs_sdSSS   S*S     S*S   SSSbs             S*S        SSSbs_sdSSS         S*S SSSSP   S*S    S*S       S*S            S*S        SSSbs   SSSbs  sSS*S   S*S    S*S  S*S  S*S                SS_sSSS  S*S    S*S  S*S     S*S   SSSbs     *
            *    SSS~SSS~S*S    YSSP    YSSP    YSSP    YSSP~YSSY    SSS     S*S    YSSP             S*S         YSSP~YSSY          S*S  SSY    SSS    S*S       S*S            S*S         YSSP    YSSP  YSS'    SSS    S*S  S*S  S*S                 Y~YSSY  SSS    S*S  SSS     S*S    YSSP     *
            *                                                                SP                      SP                             SP                 SP        SP             SP                                       SP   SP   SP                                 SP           SP              *
            *                                                                Y                       Y                              Y                  Y         Y              Y                                        Y    Y    Y                                  Y            Y               *                                                                                                                                                                                                                                                                                *
            ****************************************************************************************************************************************************************************************************************************************************************************************
            ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.ResetColor();
            warshipAgent.onSpeech += CallbackVoice;
            Game myGame = Game.getInstance();
            await myGame.initPlayer();
            // Console.ReadLine();
            Save save = new Save();
            myGame.displayGrid();
            // myGame.save();
            myGame.initShip1();
            myGame.displayGrid();
            myGame.initShip2();
            myGame.displayGrid();
            myGame.initShip3();
            myGame.initShipIa1();
            myGame.initShipIa2();
            myGame.initShipIa3();
            myGame.displayGrid();
            myGame.displayGridAttack();
            await warshipAgent.SynthesisToSpeakerAsync("L'ennemie a bien créé sa map. A vous de jouer !");
            await warshipAgent.startListening();
            Console.ReadLine();
        }


        static async void CallbackVoice(string text)
        {
            await warshipAgent.stopListening();
            Action action = Action.searchAction(text);
            if (action != null)
            {
                IVRPlugin plugin = VRPluginFactory.GetPlugin(warshipAgent, action);
                if (plugin != null)
                {
                    await plugin.dispatchAction(action.Args);
                }
            }
            else
            {
                await warshipAgent.SynthesisToSpeakerAsync(text + " n'est pas une commande valide, réessayez");
                await warshipAgent.startListening();
            }
        }
    }
}
