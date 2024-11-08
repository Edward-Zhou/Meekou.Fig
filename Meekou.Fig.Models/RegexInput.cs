﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Models
{
    public class RegexInput
    {
        public string Content { get; set; }
        public string Pattern { get; set; }
    }
    public class RegexOutput
    {
        public string Match { get; set; }
        public List<string> Groups { get; set; }
    }
}
