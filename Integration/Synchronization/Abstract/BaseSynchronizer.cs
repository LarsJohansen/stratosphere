using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Abstract;

namespace Integration.Synchronization.Abstract
{
    public partial class BaseSynchronizer
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly IStratosphereUnitOfWork StratosphereUnitOfWork; 

        public BaseSynchronizer(IStratosphereUnitOfWork stratosphereUnitOfWork, IMapper mapper, ILogger<BaseSynchronizer> logger)
        {
            StratosphereUnitOfWork =
                stratosphereUnitOfWork ?? throw new ArgumentNullException(nameof(stratosphereUnitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected void LogAndThrowException(Exception ex)
        {
            Logger.LogError(ex, ex.ToString());
            throw ex;
        }
    }
}
