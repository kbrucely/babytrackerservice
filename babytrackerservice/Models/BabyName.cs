﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babytrackerservice.Models
{
    public class BabyName
    {
        public int baby_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime  baby_birthday { get; set; }
    }
}
