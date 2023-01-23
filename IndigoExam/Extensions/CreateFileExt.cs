using IndigoExam.DTOs.CardDTOs;

namespace IndigoExam.Extensions
{
    public static class CreateFileExt
    {
        public static string CreateFile(this IFormFile file,string environment,string path)
        {
            string imagename = Guid.NewGuid() + file.FileName; 
            string Fullpath = Path.Combine(environment, path, imagename);
            using (FileStream fileStream = new FileStream(Fullpath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return imagename;
        }
    }
}
