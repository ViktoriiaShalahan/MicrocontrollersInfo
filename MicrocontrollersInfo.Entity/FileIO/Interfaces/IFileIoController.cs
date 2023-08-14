namespace MicrocontrollersInfo.Entity.FileIO.Interfaces
{
    public interface IFileIoController
    {
        string FileExtension { get; set; }

        void Load(DataSet dataSet, string fileName);
        void Save(DataSet dataSet, string fileName);
    }
}