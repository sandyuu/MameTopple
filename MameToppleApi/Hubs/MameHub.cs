using MameToppleApi.Interface;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Hubs
{
    public class MameHub : Hub
    {
        private readonly IDollService _dollService;
        private readonly IGameService _gameService;
        private readonly ICardService _cardService;
        public MameHub(IDollService dollService, IGameService gameService, ICardService cardService)
        {
            _dollService = dollService;
            _gameService = gameService;
            _cardService = cardService;
        }
        public static Dictionary<string, PlayerViewModel> player = new Dictionary<string, PlayerViewModel>();
        public static List<Doll> tempTower { get; set; }
        public static string tempCard { get; set; }

        //test data
        public List<PlayerViewModel> players = new List<PlayerViewModel>()
        {
            new PlayerViewModel{Avatar = "#", Nickname = "David", Score = 16,
                IsActive = false, IsPlaying = true, Id = 1,
            },

            new PlayerViewModel{Avatar = "#", Nickname = "Amy", Score = 8,
                IsActive = false, IsPlaying = true, Id = 2,
            },

            new PlayerViewModel{Avatar = "#", Nickname = "Tommy", Score = 13,
                IsActive = false, IsPlaying = true, Id = 3,
            }
        };

        //for connection test
        public async Task Test (string test)
        {
            await Clients.All.SendAsync("test", test);
        }

        public async Task PlayerJoin(string account)
        {
            player.Add(Context.ConnectionId, _gameService.GetPlayerInfo(account));
            if(player.Count == 2)
            {
                foreach(var i in player)
                {
                    i.Value.IsPlaying = true;
                }
            }
            await Clients.All.SendAsync("SomeOneJoin", player.Values.AsQueryable().Where(x => x.IsPlaying == false));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            player.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("SomeOneLeave", player.Values.AsEnumerable());
        }

        public async Task GetDollsTower()
        {
            await Clients.All.SendAsync("GetDollsTower", _dollService.GetAllDolls());
        }

        public async Task GetCards()
        {
            await Clients.Caller.SendAsync("GetCards", _cardService.GetAllCards());
        }

        public async Task GetMission()
        {
            await Clients.Caller.SendAsync("GetMission", _dollService.GetMissonDolls());
        }

        public async Task GetMyInfo()
        {
            await Clients.Caller.SendAsync("GetMyInfo", player[Context.ConnectionId]);
        }

        public async Task GetOtherPlayerInfo()
        {
            var opponents = new List<OpponentViewModel>();
            foreach(var i in player)
            {
                if(i.Key == Context.ConnectionId)
                {
                    continue;
                }
                opponents.Add(new OpponentViewModel()
                {
                    Avatar = i.Value.Avatar,
                    Nickname = i.Value.Nickname,
                    Score = i.Value.Score,
                    IsActive = i.Value.IsActive,
                    CurrentCardNum = i.Value.SPCard.Count
                });
            }

            await Clients.Caller.SendAsync("GetOtherPlayerInfo", opponents);
        }

        public async Task UseCard(List<Doll> dolls, string cardName)
        {
            switch (cardName)
            {
                case "UpThree":
                    tempTower = dolls;
                    tempCard = cardName;
                    await Clients.Caller.SendAsync("UseCard", _gameService.ChooseDoll(dolls, "UpThree"));
                    break;
                case "UpTwo":
                    tempTower = dolls;
                    tempCard = cardName;
                    await Clients.Caller.SendAsync("UseCard", _gameService.ChooseDoll(dolls, "UpTwo"));
                    break;
                case "UpOne":
                    tempTower = dolls;
                    tempCard = cardName;
                    await Clients.Caller.SendAsync("UseCard", _gameService.ChooseDoll(dolls, "UpOne"));
                    break;
                case "DropDown":
                    tempTower = dolls;
                    tempCard = cardName;
                    await Clients.All.SendAsync("UseCard", _gameService.ChooseDoll(dolls, "DropDown"));
                    break;
                case "Discard":
                    await Clients.All.SendAsync("UseCard", _gameService.Discard(dolls));//應該直接下一位
                    break;
            }
        }

        public async Task DollChosen(Doll doll)
        {
            await Clients.All.SendAsync("DollChosen", _gameService.ChosenDollMove(doll, tempTower, tempCard));//下一位
        }

        public async Task CheckMyPoint(List<Doll> dolls, List<Doll> missionDolls)
        {
            player[Context.ConnectionId].Score += _gameService.CheckPoint(dolls, missionDolls);
            await Clients.All.SendAsync("CheckMyPoint", player[Context.ConnectionId].Score);
        }

        public async Task GetResult()
        {
            var result =  _gameService.GetResult(players);
            await Clients.All.SendAsync("GetResult", result);
        }

    }
}
