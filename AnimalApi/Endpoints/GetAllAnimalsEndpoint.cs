using AnimalApi.Data;
using AnimalApi.Dto.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AnimalApi.Endpoints
{
    public class GetAllAnimalsEndpoint : EndpointWithoutRequest<List<AnimalListResponse>>
    {
        private readonly AnimalDbContext _context;

        public GetAllAnimalsEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/animals");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var races = await _context.Races.ToListAsync(ct);
            var animals = await _context.Animals
                .Select(a => new AnimalListResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    RaceId = a.RaceId
                })
                .ToListAsync(ct);

            await SendAsync(animals, cancellation: ct);
        }
    }
}

