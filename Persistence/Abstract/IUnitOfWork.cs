using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Abstract
{
    public interface IUnitOfWork
    {

        int Complete();
    }
}
