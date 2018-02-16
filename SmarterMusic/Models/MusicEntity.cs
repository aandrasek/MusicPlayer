using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmarterMusic.Models
{
    public class MusicEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string MusicURL { get; set; }
    }
}