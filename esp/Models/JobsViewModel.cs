﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esp.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}