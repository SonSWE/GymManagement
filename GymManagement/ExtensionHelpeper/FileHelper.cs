using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement
{
    public class FileHelper
    {
        public bool UpLoadFile(Stream file, string path, string fileName)
        {
            try
            {
                string combine = Path.Combine(path, fileName);
                if (File.Exists(path))
                {
                    return false;
                }
                else
                {
                    using (var stream = System.IO.File.Create(combine))
                    {
                        file.CopyTo(stream);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
