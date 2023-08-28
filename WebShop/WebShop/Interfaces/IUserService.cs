using WebShop.DTO;

namespace WebShop.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetUserProfile(int id);
        Task EditUserProfile(int id, EditUserDTO editUserDTO);
        Task AddPicture(int id, IFormFile image);
    }
}
