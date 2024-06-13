using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using AnimalApi.Models;
using FastEndpoints;
using System.Security.Cryptography;
using System.Text;

namespace AnimalApi.Endpoints
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest,UserResponse>
    {
        private readonly AnimalDbContext _context;

        public CreateUserEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {

            var user = new User
            {
                Email = req.Email,
                Password = HashPassword(req.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
            });
        }

        public string HashPassword(string password)
        {
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
