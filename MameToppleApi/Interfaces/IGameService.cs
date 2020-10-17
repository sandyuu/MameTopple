using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MameToppleApi.Interfaces
{
    public interface IGameService
    {
        ICollection<PlayerViewModel> GetPlayerInfo(List<PlayerViewModel> player);

        ICollection<PlayerViewModel> GetResult(List<PlayerViewModel> players);

        List<Doll> ChooseDoll(List<Doll> dolls, string cardName);

        List<Doll> Discard(List<Doll> dolls);

        List<Doll> DropDwon(List<Doll> dolls);

        List<Doll> ChosenDollMove(Doll doll, List<Doll> dolls, string cardName);
    }
}
