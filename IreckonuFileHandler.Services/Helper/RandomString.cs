using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IreckonuFileHandler.Services.Helper
{
    public static class RandomString
    {
        private static readonly Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
