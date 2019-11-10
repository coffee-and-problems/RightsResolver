using System.IO;
using RightsResolver;

namespace Actualizer
{
    class Program
    {
        public const string rulesPath = @"C:\Users\julie\source\Kontur\RightsResolver\RightsResolver\Rules";

        static void Main(string[] args)
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var usersPath = Path.Combine(currentDirectory, "Users.txt");
            var users = new UserReader(usersPath).GetUsers();

            var resolver = new Resolver(rulesPath, AllProductsArray.Products);
            var userRights = resolver.GetUserRights(users);
        }
    }
}
