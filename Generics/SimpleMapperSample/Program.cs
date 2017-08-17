using Infra.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = GetDataTable();
            List<Student> list = table.Map<Student>();
            foreach (var st in list)
            {
                Console.WriteLine("Student {0} name {1}",
                          st.Id, st.Name);
            }
            Console.ReadLine();
        }

        private static DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            DataRow dr = dt.NewRow();
            dr["Id"] = 1056; // or dr[0]=1056;
            dr["Name"] = "Berkay Başöz 1056"; // or dr[1]="Berkay Başöz";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Id"] = 1057; // or dr[0]=1056;
            dr["Name"] = "Berkay Başöz 1057"; // or dr[1]="Berkay Başöz";
            dt.Rows.Add(dr);
            return dt;
        }
    }
}
