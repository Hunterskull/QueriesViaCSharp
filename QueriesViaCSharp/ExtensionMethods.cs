using System;
using System.Data;

namespace QueriesViaCSharp
{
    public static class ExtensionMethods
    {
        public static void Dump(this DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }
    }
}