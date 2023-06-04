using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApi.Services
{
    internal class UserConverter : IConverter<User, UserDTO>
    {
        public User FromDTO(UserDTO userDTO)
        {
            return new User
            {
                IdNumber = userDTO.IdNumber,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Position = userDTO.Position,
                UserNumbers = userDTO.UserNumbers?.Select(x => new UserNumber { IdNumber = x.IdNumber, UserIdNumber = userDTO.IdNumber }).ToList() ?? new List<UserNumber>()
            };
        }

        public UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                IdNumber = user.IdNumber,
                Name = user.Name,
                Email = user.Email,
                Position = user.Position,
                UserNumbers = user.UserNumbers?.Select(x => new UserNumberDTO { IdNumber = x.IdNumber }).ToList() ?? new List<UserNumberDTO>()
            };
        }
    }
}
