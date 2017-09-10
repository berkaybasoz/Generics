using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class TimeStampExtensions
    {
        public static long GetTimeStampValue(this byte[] bytes)
        {
            return ToEFTimeStamp(bytes); 
        }

        private static Int64 ToEFTimeStamp(byte[] dbVersion)
        {
            Int64 version = 0;
            if (BitConverter.IsLittleEndian)
            {
                byte[] clone = dbVersion.Clone() as byte[];
                Array.Reverse(clone);
                version = BitConverter.ToInt64(clone, 0);
            }
            else
            {
                version = BitConverter.ToInt64(dbVersion, 0);
            }
            return version;

        }
    }
}
