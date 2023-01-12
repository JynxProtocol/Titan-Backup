namespace Titan.API.Models.AWK
{
    public class AWKPhoto
    {
        public int DocID { get; set; }
        public int DocumentType { get; set; }
        public int AWKID { get; set; }
        public string DocumentName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        //public byte[] Image { get; set; }
        public string Image { get; set; }
    }
}
