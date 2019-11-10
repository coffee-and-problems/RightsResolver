using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RightsResolver;

namespace Actualizer
{
    class UserReader
    {
        private string filePath { get; }

        public UserReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();
            using (var streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    users.Add(ReadUser(line));
                }
            }

            return users;
        }

        private User ReadUser(string line)
        {
            var idPositions = line.Split(';');
            if (idPositions.Length < 2) throw new ArgumentException("Wrong user format");

            var positions = new List<Position>();
            for (var i = 1; i < idPositions.Length; i++)
            {
                var positionStrings = idPositions[i].Split(',');
                var departments = positionStrings.Take(positionStrings.Length - 1)
                                .Select(int.Parse)
                                .ToArray();
                positions.Add(new Position(departments, positionStrings[positionStrings.Length-1]));
            }

            return new User(Guid.Parse(idPositions[0]), positions);
        }
    }
}