using AnimalApi.Models;
using AnimalApi.Data;
using FastEndpoints;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;

namespace AnimalApi.Endpoints
{
    public class UpdateAnimalEndpoint : Endpoint<UpdateAnimalRequest,AnimalResponse>
    {
        private readonly AnimalDbContext _context;

        public UpdateAnimalEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/animals/update/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateAnimalRequest req, CancellationToken ct)
        {
            var animal = await _context.Animals.FindAsync(req.Id);
            if (animal == null)
            {
                return;
            }

            animal.Name = req.Name;
            animal.Description = req.Description;
            animal.RaceId = req.RaceId;

            await _context.SaveChangesAsync(ct);

            await SendAsync(new AnimalResponse
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
            });
        }

    }
}
