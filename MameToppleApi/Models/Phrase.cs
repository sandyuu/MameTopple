using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Models
{
    public class Phrase
    {
        public int Id { get; set; }
        public string content { get; set; }
    }
}
