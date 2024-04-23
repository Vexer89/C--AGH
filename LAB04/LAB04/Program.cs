using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using System.Globalization;
using LAB04.assets;

class Reader<T>
{
    public List<T> readList(String path, Func<String[], T> CsvToModel)
    {
        List<T> values = File.ReadAllLines(path).Skip(1).Select(v => CsvToModel(v.Split(','))).ToList();
        return values;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        List<Employee> employees = new Reader<Employee>().readList("data/employees.csv", Employee.CsvToModel);
        List<EmployeeTerritory> employeeTerritories = new Reader<EmployeeTerritory>().readList("data/employee_territories.csv", EmployeeTerritory.CsvToModel);
        List<Region> regions = new Reader<Region>().readList("data/regions.csv", Region.CsvToModel);
        List<Territory> territories = new Reader<Territory>().readList("data/territories.csv", Territory.CsvToModel);
        List<OrderDetail> ordersDetails = new Reader<OrderDetail>().readList("data/orders_details.csv", OrderDetail.CsvToModel);
        List<Order> orders = new Reader<Order>().readList("data/orders.csv", Order.CsvToModel);
        
        Console.WriteLine("\nEx 1.Last name of all employees");
        foreach (Employee employee in employees)
            Console.WriteLine(employee.lastname);
        
        Console.WriteLine("\nEx 2.Last name + Region + Territory of all employees");
        var query1 = from e in employees
            join et in employeeTerritories on e.employeeid equals et.employeeid
            join t in territories on et.territoryid equals t.territoryid
            join r in regions on t.regionid equals r.regionid
            select new { L = e.lastname, R = r.regiondescription, T = t.territorydescription };
        foreach (var i in query1)
            Console.WriteLine(i.L + " " + i.R + " " + i.T);
        
        Console.WriteLine("\nEx 3.Region + Surname of employees working in those regions");
        var query2 = from e in (from ee in employees
                join et in employeeTerritories on ee.employeeid equals et.employeeid
                join t in territories on et.territoryid equals t.territoryid
                join r in regions on t.regionid equals r.regionid
                select new { E = ee, R = r.regiondescription }).Distinct()
            group e by e.R into g
            select new { R = g.Key, S = g.Select(x => x.E.lastname).Distinct().ToList() };

        foreach (var i in query2)
            foreach (var j in i.S)
                Console.WriteLine(i.R + " " + j);
        
        Console.WriteLine("\nEx 4.Region + Count of employees working in those regions");
        var query3 = from e in employees
            join et in employeeTerritories on e.employeeid equals et.employeeid
            join t in territories on et.territoryid equals t.territoryid
            join r in regions on t.regionid equals r.regionid
            group e by r into g
            select new { R = g.Key, C = g.Distinct().Count() };
        foreach (var i in query3)
            Console.WriteLine(i.R.regiondescription + " " + i.C);
        
        System.Console.WriteLine("\nEx 5.Count of orders + average value of order + max value of order for every employee");
        var query4 = from e in employees
            join o in orders on e.employeeid equals o.employeeid
            join od in ordersDetails on o.orderid equals od.orderid
            group od by e.employeeid into employeeOrders
            select new {
                employeeid = employeeOrders.Key,
                //averagePrice = employeeOrders.Sum(x => double.Parse(x.unitprice, CultureInfo.InvariantCulture) * int.Parse(x.quantity) * (1 - double.Parse(x.discount, CultureInfo.InvariantCulture))) / employeeOrders.Select(x => x.orderid).Distinct().Count(),
                averagePrice = employeeOrders.Average(x => double.Parse(x.unitprice, CultureInfo.InvariantCulture) * int.Parse(x.quantity) * (1 - double.Parse(x.discount, CultureInfo.InvariantCulture))),
                numberOfOrders = employeeOrders.Select(o => o.orderid).Distinct().Count(),
                highestPrice = employeeOrders.Max(x => double.Parse(x.unitprice, CultureInfo.InvariantCulture) * int.Parse(x.quantity) * (1 - double.Parse(x.discount, CultureInfo.InvariantCulture)))
            };
        foreach (var i in query4)
            Console.WriteLine(i.employeeid  + ",     count: " + i.numberOfOrders + ",     avg value: " + i.averagePrice + ",     max value: " + i.highestPrice);    }
}