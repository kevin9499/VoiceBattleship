using System.Threading.Tasks;
using System.Collections;

namespace board_game
{
    interface IVRPlugin
    {
        Task dispatchAction(ArrayList args);
    }
}