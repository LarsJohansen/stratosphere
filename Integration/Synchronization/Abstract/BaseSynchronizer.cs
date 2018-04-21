using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Integration.Synchronization.Abstract
{
    public class BaseSynchronizer
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        public BaseSynchronizer(IMapper mapper, ILogger<BaseSynchronizer> logger)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
