using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MameToppleApi.Models.ViewModels
{
    public class PlayerViewModel
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int Score { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsActive { get; set; }
        public SPCard SPCard { get; set; }
    }

    public class SPCard
    {
        public bool UpThree { get; set; }
        public bool UpTwo { get; set; }
        public bool UpOne { get; set; }
        public bool Discard { get; set; }
        public bool DropDown { get; set; }
    }
}
