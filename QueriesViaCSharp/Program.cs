using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;



namespace QueriesViaCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\LUCASSQLSERVER;Initial Catalog=Northwind;Integrated Security=True";

            string[] queryArray = new string[]
            {
                @"select ProductID, ProductName, SupplierID, p.CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, CategoryName 
                from Products p
                inner join Categories c on p.CategoryID = c.CategoryID
                order by CategoryName,ProductName;",
                @"select top 10 ProductName as 'TenMostExpensiveProducts', UnitPrice from Products
                order by UnitPrice desc;",
                @"select s.City, s.CompanyName, s.ContactName, 'Supplier'  
                from Suppliers s
                union 
                select c.City, c.CompanyName, c.ContactName, 'Customer' from Customers c
                order by City;",
                @"select Distinct OrderID, sum(Quantity * (UnitPrice * (1  - Discount))) Subtotal 
                from [Order Details]
                group by OrderID;",
                @"select o.ShippedDate,o.OrderID,sum(Quantity * (UnitPrice * (1  - Discount))) Subtotal,YEAR(o.ShippedDate) ""Year""  
                from Orders o
                inner join [Order Details] od on o.OrderID = od.OrderID
                where o.ShippedDate between '1996/12/24' and '1997/9/30'
                group by O.OrderID, o.ShippedDate
                order by o.ShippedDate;"
            };
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                for (int i = 0; i < queryArray.Length; i++)
                {
                    SqlCommand cmd = new SqlCommand(queryArray[i], conn);
                    var resultaat = cmd.ExecuteReader();
                    DataTable data = new DataTable();
                    data.Load(resultaat);
                    data.Dump();
                }
                conn.Close();
              
            }
        }
    }
}
