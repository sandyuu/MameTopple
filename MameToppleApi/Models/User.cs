﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MameToppleApi.Models
{
    public partial class User
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int? Point { get; set; }
        public int? Win { get; set; }
        public int? Lose { get; set; }
    }
}