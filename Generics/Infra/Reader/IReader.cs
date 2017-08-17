using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Reader
{
    public interface IReader
    { 
        DataTable Read();
    }
}
