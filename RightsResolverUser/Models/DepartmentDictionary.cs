using System;
using System.Collections.Generic;

namespace Actualizer
{
    class DepartmentDictionary
    {
        private Dictionary<string, int> departmentDictionary;

        public DepartmentDictionary()
        {
            departmentDictionary.Add("Отдел 0", 0);
            departmentDictionary.Add("Отдел 1", 1);
            departmentDictionary.Add("Отдел 2", 2);
            departmentDictionary.Add("Отдел 3", 3);
            departmentDictionary.Add("Отдел 4", 4);
            departmentDictionary.Add("Отдел 5", 5);
        }

        public int? GetId(string department)
        {
            if (!departmentDictionary.ContainsKey(department)) 
                throw new ArgumentException("Данного отдела нет в базе");
            return departmentDictionary[department];
        }
    }
}
