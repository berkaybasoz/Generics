using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Attr
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SimpleMapperAttribute : Attribute
    {
        public string HeaderName { get; set; }
    }
}
