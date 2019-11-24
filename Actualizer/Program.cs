using System.IO;
using RightsResolver.Implementation;
using RightsResolverUser.Models;

namespace RightsResolverUser
{
    class Program
    {
        public static readonly string rulesPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Rules");

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
