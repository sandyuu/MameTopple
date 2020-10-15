using MameToppleApi.Interface;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Repository;
using MameToppleApi.Service;
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
        public MameHub(IDollService dollService, IGameService gameService)
        {
            _dollService = dollService;
            _gameService = gameService;
        }
        public static Dictionary<string, PlayerViewModel> player { get; set; }
        public static List<Doll> tempTower { get; set; }
        public static string tempCard { get; set; }

        //test data
        public List<PlayerViewModel> players = new List<PlayerViewModel>()
        {
            new PlayerViewModel{Avatar = "#", Nickname = "David", Score = 16,
                IsActive = false, IsPlaying = true,
                SPCard = new SPCard{ UpOne = true, UpTwo = true, UpThree = true, DropDown = true, Discard = true }
            },

            new PlayerViewModel{Avatar = "#", Nickname = "Amy", Score = 8,
                IsActive = false, IsPlaying = true,
                SPCard = new SPCard{ UpOne = true, UpTwo = true, UpThree = true, DropDown = true, Discard = true }
            },

            new PlayerViewModel{Avatar = "#", Nickname = "Tommy", Score = 13,
                IsActive = false, IsPlaying = true,
                SPCard = new SPCard{ UpOne = true, UpTwo = true, UpThree = true, DropDown = true, Discard = true }
            }
        };

        //for connection test
        public async Task Test (string test)
        {
            await Clients.All.SendAsync("test", test);
        }

        public async Task GetDollsTower()
        {
            await Clients.All.SendAsync("GetDollsTower", _dollService.GetAllDolls());
        }

        public async Task GetMission()
        {
            await Clients.Caller.SendAsync("GetMission", _dollService.GetMissonDolls());
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
                case "Discard":
                    await Clients.All.SendAsync("UseCard", _gameService.Discard(dolls));//應該直接下一位
                    break;
                case "DropDown":
                    await Clients.All.SendAsync("UseCard", _gameService.DropDwon(dolls));//應該直接下一位
                    break;
            }
        }

        public async Task DollChosen(Doll doll)
        {
            await Clients.All.SendAsync("DollChosen", _gameService.ChosenDollMove(doll, tempTower, tempCard));//下一位
        }

        public async Task GetResult()
        {
            var result =  _gameService.GetResult(players);
            await Clients.All.SendAsync("GetResult", result);
        }

    }
}
