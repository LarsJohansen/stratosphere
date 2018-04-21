using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Integration.Synchronization.Abstract
{
    public class BaseSyncronizer
    {
        protected readonly IMapper Mapper;

        public BaseSyncronizer(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
