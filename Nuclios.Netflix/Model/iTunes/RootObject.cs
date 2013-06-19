using System.Collections.Generic;

namespace Nuclios.Netflix.Model
{
    public class RootObject
    {
        public int resultCount { get; set; }
        public List<iTunesMovie> results { get; set; }
    }
}