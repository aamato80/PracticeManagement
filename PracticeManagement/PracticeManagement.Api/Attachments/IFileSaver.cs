namespace PracticeManagement.Api.Attachments
{
    public interface IFileSaver
    {
        void Save(Stream stream,string fileName);
    }
}
