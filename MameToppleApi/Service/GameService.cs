using MameToppleApi.Interface;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Service
{
    public class GameService : IGameService
    {
        private readonly IRepository<User> _repository;
        public GameService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public ICollection<PlayerViewModel> GetPlayerInfo(List<PlayerViewModel> players)
        {
            return players;
        }

        public ICollection<PlayerViewModel> GetResult(List<PlayerViewModel> players)
        {
            return players.OrderByDescending(x => x.Score).ToList();
        }

        public List<Doll> ChooseDoll(List<Doll> dolls, string cardName)
        {
            switch (cardName)
            {
                case "UpThree":
                    return dolls.Skip(3).ToList();
                case "UpTwo":
                    return dolls.Skip(2).ToList();
                case "UpOne":
                    return dolls.Skip(1).ToList();
                default:
                    return null;
            }
        }

        public List<Doll> Discard(List<Doll> dolls)
        {
            dolls.RemoveAt(dolls.Count - 1);
            return dolls;
        }

        public List<Doll> DropDwon(List<Doll> dolls)
        {
            var first = dolls.First();
            dolls.Add(first);
            return dolls.Skip(1).ToList();
        }

        public List<Doll> ChosenDollMove(Doll doll, List<Doll> dolls, string cardName)
        {
            var targetIndex = dolls.FindIndex(x => x.Id == doll.Id); 
            switch (cardName)
            {
                case "UpThree":
                    dolls.Insert(targetIndex - 3, doll);
                    dolls.RemoveAt(targetIndex + 1);
                    return dolls;
                case "UpTwo":
                    dolls.Insert(targetIndex - 2, doll);
                    dolls.RemoveAt(targetIndex + 1);
                    return dolls;
                case "UpOne":
                    dolls.Insert(targetIndex - 1, doll);
                    dolls.RemoveAt(targetIndex + 1);
                    return dolls;
                default:
                    return null;
            }
        }
    }
}
