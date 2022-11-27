namespace DossierManagement.Api.Attachments
{
    public interface IAttachmentManager
    {
        void Save(Stream stream,string fileName);
        Task<Stream> Load( string fileName);
    }
}
