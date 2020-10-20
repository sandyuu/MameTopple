using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MameToppleApi.Models.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int Score { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsActive { get; set; }
        public List<Card> SPCard { get; set; }
    }

}
