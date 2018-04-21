using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Integration.Synchronization.Abstract;
using Microsoft.Extensions.Logging;

namespace Integration.Synchronization
{
    public class CompetitionSynchronizer : BaseSynchronizer
    {
        public CompetitionSynchronizer(IMapper mapper, ILogger<BaseSynchronizer> logger) : base(mapper, logger)
        {
            
        }
      
    }
}
