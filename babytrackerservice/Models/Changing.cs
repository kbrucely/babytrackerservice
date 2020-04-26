using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babytrackerservice.Models
{
    public class Changing
    {
        public int poop_id { get; set; }
        public DateTime poop_at { get; set; }

        public BabyName baby { get; set; }
    }
}
