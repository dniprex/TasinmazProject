using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Dtos;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface IUserService:IGenericService<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();

        Task<User> AddUserAsync(string email, string password, string role);
        Task<User> UpdateUserAsync(int id, UserDTO userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
