namespace PracticeManagement.Api.Attachments
{
    public class LocalFileSaver : IFileSaver
    {
        private readonly string _path;

        public LocalFileSaver(string path)
        {
            _path = path;
        }
        public void Save(Stream stream,string filename)
        {
            using (var fileStream = new FileStream(_path+"\\"+filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}
