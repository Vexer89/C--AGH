using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB04.assets
{
    public class EmployeeTerritory
    {
        public String employeeid { get; set; }
        public String territoryid { get; set; }

        public static EmployeeTerritory CsvToModel(String[] values)
        {
            return new EmployeeTerritory
            {
                employeeid = values[0],
                territoryid = values[1]
            };
        }

        public override string ToString()
        {
            return employeeid + " " + territoryid;
        }
    }
}

