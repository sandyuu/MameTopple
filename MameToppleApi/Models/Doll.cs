using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Models
{
    public class Doll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
