using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Helpers
{

    public enum Roles { Admin, Member } 
    public class Helper
    {
        public static bool DeletePhoto(string root, string folder, string fileName)
        {
            string filePath = Path.Combine(root, folder, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
