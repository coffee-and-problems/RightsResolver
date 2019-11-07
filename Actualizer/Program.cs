using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightsResolver;

namespace Actualizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(currentDirectory, "Users.txt");
            
            var reader = new UserReader(filePath);
            var users = reader.GetUsers();
        }
    }
}
