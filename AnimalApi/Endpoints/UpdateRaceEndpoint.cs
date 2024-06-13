using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using FastEndpoints;

namespace AnimalApi.Endpoints
{
    public class UpdateRaceEndpoint : Endpoint<UpdateRaceRequest, RaceResponse>
    {
        private readonly AnimalDbContext _context;

        public UpdateRaceEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/races/update/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateRaceRequest req, CancellationToken ct)
        {
            var race = await _context.Races.FindAsync(req.Id);
            if (race == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            race.Name = req.Name;
            race.Description = req.Description;

            _context.Races.Update(race);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new RaceResponse
            {
                Id = race.Id,
                Name = race.Name,
                Description = race.Description
            }, cancellation: ct);
        }
    }
}
