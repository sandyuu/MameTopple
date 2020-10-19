using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MameToppleApi.Models
{
    public partial class User
    {
        public string Account { get; set; }

        [Column(TypeName = "varchar(150)")]
        [MaxLength(150)]
        public string Password { get; set; }

        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int? Win { get; set; }
        public int? Lose { get; set; }
    }
}
