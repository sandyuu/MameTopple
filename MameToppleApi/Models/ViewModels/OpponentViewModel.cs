using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace MameToppleApi.Models.ViewModels
{
    public class OpponentViewModel
    {
        public string Avatar { get; set; }
        public string Nickname { get; set; }
        public bool IsActive { get; set; }
        public int CurrentCardNum { get; set; }
        public int  Score { get; set; }
    }
}
