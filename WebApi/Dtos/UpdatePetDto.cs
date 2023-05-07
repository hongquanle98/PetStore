namespace WebApi.Dtos
{
    public class UpdatePetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public int? CategoryId { get; set; }
    }
}
