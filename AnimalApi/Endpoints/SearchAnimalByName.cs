using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AnimalApi.Endpoints
{
    public class SearchAnimalByNameEndpoint : Endpoint<SearchAnimalByNameRequest, List<AnimalResponse>>
    {
        private readonly AnimalDbContext _context;

        public SearchAnimalByNameEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/animals/searchbyname");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SearchAnimalByNameRequest req, CancellationToken ct)
        {
            var animals = await _context.Animals
                .Where(a => a.Name.Contains(req.Name))
                .Select(a => new AnimalResponse
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
