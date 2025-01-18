using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Core.Dto.AUTH;
using TaskManager.Core.Interfaces;
using TaskManager.DataProvider.Context;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    public AuthService (ApplicationDbContext db)
    {
        _db = db; 
    }

    public async Task<BaseResponse<string>> LogIn(AuthDto authDto)
    {
        try
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == authDto.Email && !x.IsDeleted);
            if (user == null || authDto.Password != user.Password)
                return new BaseResponse<string>(null, false , "Invalid Password");
            

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("0BD0E95C-6387-4135-A80E-489FF6E5C1DF");

            var expirationDate = authDto.RememberMe ? DateTime.UtcNow.AddYears(10) : DateTime.UtcNow.AddDays(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name , user.FullName),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return new BaseResponse<string>(tokenString, true);
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(null, false, ex.Message);
        }

    }
}