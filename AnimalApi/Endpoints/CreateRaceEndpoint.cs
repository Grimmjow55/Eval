using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using AnimalApi.Models;
using FastEndpoints;

namespace AnimalApi.Endpoints
{
    public class CreateRaceEndpoint : Endpoint<CreateRaceRequest, RaceResponse>
    {
        private readonly AnimalDbContext _context;

        public CreateRaceEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/races/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRaceRequest req, CancellationToken ct)
        {
            var race = new Race
            {
                Name = req.Name,
                Description = req.Description,
            };

            _context.Races.Add(race);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new RaceResponse
            {
                Id = race.Id,
                Name = race.Name,
                Description = race.Description,
            });
        }
    }
}
