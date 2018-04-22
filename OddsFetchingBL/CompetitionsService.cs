using OddsFetchingBLInterfaces;
using System;
using System.Collections.Generic;
using OddsFetchingEntities;
using DataInterfaces;
using System.Diagnostics;

namespace OddsFetchingBL
{
    public class CompetitionsService : ICompetitionsService
    {
        private IIdsReader<Competition> _trackedCompsReader;

        public CompetitionsService(IIdsReader<Competition> trackedCompsReader)
        {
            _trackedCompsReader = trackedCompsReader;
        }

        public ISet<string> FetchCompetitionIds(MarketFilter filter)
        {
            ISet<string> ids;
            try
            {
                ids = _trackedCompsReader.ReadIds(filter);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", "Error reading tracked comp ids: " + e.Message);
                ids = new HashSet<string>();
            }
            return ids;
        }
    }
}
