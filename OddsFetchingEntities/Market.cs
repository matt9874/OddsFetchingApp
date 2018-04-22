using System.Collections.Generic;

namespace OddsFetchingEntities
{
    public class Market:Entity
    {
        //Possibly change to enumeration
        public string Status { get; set; }

        public bool Complete { get; set; }
        public bool Inplay { get; set; }

        public int NumWinners { get; set; }
        public int NumRunners { get; set; }
        public int NumActiveRunners { get; set; }

        public int Version { get; set; }

        public IEnumerable<Runner> Runners { get; set; }
    }
}