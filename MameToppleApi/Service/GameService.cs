using MameToppleApi.Interface;
using MameToppleApi.Interfaces;
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
        private readonly ICardService _cardService;
        public GameService(IRepository<User> repository, ICardService cardService)
        {
            _repository = repository;
            _cardService = cardService;
        }

        public PlayerViewModel GetPlayerInfo(string account)
        {
            var user = _repository.GetOne(x => x.Account == account);
            return new PlayerViewModel()
            {
                Avatar = user.Avatar,
                Nickname = user.NickName,
                IsActive = false,
                IsPlaying = false,
                Score = 0,
                SPCard = _cardService.GetAllCards().ToList()
            };
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
                case "DropDown":
                    return dolls.Take(dolls.Count - 1).ToList();
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
                case "DropDown":
                    dolls.Add(doll);
                    dolls.RemoveAt(targetIndex);
                    return dolls;
                default:
                    return null;
            }
        }

        public int CheckPoint(List<Doll> dolls, List<Doll> missionDolls)
        {
            int score = 0;
            var firstThreeIndex = new List<int>()
            {
                dolls[0].Id,
                dolls[1].Id,
                dolls[2].Id
            };
            var missionIndex = new List<int>()
            {
                missionDolls[0].Id,
                missionDolls[1].Id,
                missionDolls[2].Id
            };
            if(firstThreeIndex[0] == missionIndex[0])
            {
                score += 9;
            }
            if(firstThreeIndex.Take(2).Contains(missionIndex[1]))
            {
                score += 5;
            }
            if (firstThreeIndex.Contains(missionIndex[2]))
            {
                score += 2;
            }

            return score;
        }
    }
}
