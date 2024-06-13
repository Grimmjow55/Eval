namespace AnimalApi.Dto.Request
{
    public class CreateAnimalRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
    }
}
