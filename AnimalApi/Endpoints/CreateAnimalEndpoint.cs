namespace AnimalApi.Endpoints
{
    using AnimalApi.Data;
    using AnimalApi.Models;
    using FastEndpoints;
    using AnimalApi.Dto.Request;
    using AnimalApi.Dto.Response;

    public class CreateAnimalEndpoint : Endpoint<CreateAnimalRequest, AnimalResponse>
    {
        private readonly AnimalDbContext _context;

        public CreateAnimalEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/animals/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAnimalRequest req, CancellationToken ct)
        {
            var animal = new Animal
            {
                Name = req.Name,
                Description = req.Description,
                RaceId = req.RaceId
            };

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new AnimalResponse
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
                RaceId = animal.RaceId
            });
        }
    }
}
