using System.Diagnostics;

namespace AnimalApi.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
    }
}
