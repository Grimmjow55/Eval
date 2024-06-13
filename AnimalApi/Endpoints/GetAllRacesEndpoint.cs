
    using AnimalApi.Data;
    using AnimalApi.Dto.Response;
    using FastEndpoints;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    namespace AnimalApi.Endpoints
    {
        public class GetAllRacesEndpoint : EndpointWithoutRequest<List<RaceResponse>>
        {
            private readonly AnimalDbContext _context;

            public GetAllRacesEndpoint(AnimalDbContext context)
            {
                _context = context;
            }

            public override void Configure()
            {
                Verbs(Http.GET);
                Routes("/races");
                AllowAnonymous();
            }

            public override async Task HandleAsync(CancellationToken ct)
            {
                var races = await _context.Races
                    .Select(r => new RaceResponse
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    })
                    .ToListAsync(ct);

                await SendAsync(races, cancellation: ct);
            }
        }
    }
