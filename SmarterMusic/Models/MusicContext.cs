using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmarterMusic.Models
{
    public class MusicContext:DbContext
    {
        public DbSet<MusicEntity> Music { get; set; }
    }
}