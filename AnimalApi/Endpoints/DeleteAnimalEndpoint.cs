using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using FastEndpoints;

namespace AnimalApi.Endpoints
{
    public class DeleteAnimalEndpoint : Endpoint<DeleteAnimalRequest, AnimalResponse>
    {
        private readonly AnimalDbContext _context;

        public DeleteAnimalEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("/animals/delete/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteAnimalRequest req, CancellationToken ct)
        {
            var animal = await _context.Animals.FindAsync(req.Id);
            if (animal == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new AnimalResponse
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
            }, cancellation: ct);
        }
    }
}
