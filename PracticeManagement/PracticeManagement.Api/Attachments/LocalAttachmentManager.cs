using System.Runtime;

namespace PracticeManagement.Api.Attachments
{
    public class LocalAttachmentManager : IAttachmentManager
    {
        private readonly string _path;

        public LocalAttachmentManager(string path)
        {
            _path = path;
        }

        public async Task<Stream> Load(string fileName)
        {
            var ms = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(_path, fileName), FileMode.Open, FileAccess.Read))
            {
               await fs.CopyToAsync(ms);
            }
            ms.Seek(0, SeekOrigin.Begin);
            ms.Position = 0;
            return ms;              
        }

        public void Save(Stream stream,string filename)
        {
            using (var fileStream = new FileStream(Path.Combine(_path, filename), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}
