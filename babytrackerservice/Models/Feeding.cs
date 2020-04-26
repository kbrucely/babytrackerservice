using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babytrackerservice.Models
{
    public class Feeding
    {
        public int feed_id { get; set; }
        public DateTime fed_at { get; set; }
        public int baby_id { get; set; }

    }
}
