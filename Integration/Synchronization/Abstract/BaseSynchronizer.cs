using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Integration.Synchronization.Abstract
{
    public class BaseSynchronizer
    {
        protected readonly IMapper Mapper;

        public BaseSynchronizer(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
