namespace IndigoExam.DTOs.CardDTOs
{
    public class CardPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
