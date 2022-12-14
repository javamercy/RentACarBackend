using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(
                    "File path cannot be found. Are you missing something?"
                );

            File.Delete(path);
        }

        public string Update(IFormFile file, string path, string root)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return this.Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                string extension = Path.GetExtension(file.FileName);
                string guid = GuidHelper.GuidHelper.GetNewGuid();
                string path = root + guid + extension;

                using (FileStream fileStream = File.Create(path))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return path;
                }
            }

            return null;
        }
    }
}
