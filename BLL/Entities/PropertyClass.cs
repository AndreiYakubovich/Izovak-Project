﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class PropertyClass
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public Dictionary<string,Dictionary<string,object>> Attributes { get; set; }


    }
}
