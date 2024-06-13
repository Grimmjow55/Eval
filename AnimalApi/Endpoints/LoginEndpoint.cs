using AnimalApi.Data;
using AnimalApi.Dto.Request;
using AnimalApi.Dto.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AnimalApi.Endpoints
{
    public class LoginEndpoint : Endpoint<LoginRequest,UserResponse>
    {
        private readonly AnimalDbContext _context;

        public LoginEndpoint(AnimalDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var passwordhash = HashPassword(req.Password);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == req.Email && x.Password == passwordhash);
            if (user == null)
            {
                return;
            }

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
