using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;
using System.Security.Claims;

namespace MyTasks.Services
{
    public class UserService : IUserService
    {
        #region Prop
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMyTaskRepository _myTaskRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public UserService(IHttpContextAccessor contextAccessor, 
            IMyTaskRepository myTaskRepository, IWebHostEnvironment webHostEnvironment)
        {
            _contextAccessor = contextAccessor;
            _myTaskRepository = myTaskRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<EditProfileViewModel> GetEditUserAsync(string userId)
        {
            var userDetail = await _myTaskRepository.GetUserAsync(userId);

            EditProfileViewModel user = new()
            {
                Name = userDetail.Name,
                Email = userDetail.Email
            };

            return user;
        }
        public async Task<UserModel> GetUserAsync(string userId)
        {
            var userDetail = await _myTaskRepository.GetUserAsync(userId);
            return userDetail;
        }
        
        public async Task UpdateUserProfileAsync(EditProfileViewModel updatedUser)
        {
            var existingUser =
                    await _myTaskRepository.GetUserAsync(GetUserId());

            if (updatedUser.ProfilePicture != null)
            {
                string imageFolderPath = 
                    Path.Combine(_webHostEnvironment.WebRootPath, "img");

                if (existingUser.PhoneNumber != null)
                {

                }

                var uniqueFileName = await UploadFile(updatedUser.ProfilePicture, imageFolderPath);
                existingUser.PhoneNumber = uniqueFileName;
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            await _myTaskRepository.UpdateUserAsync(existingUser);
        }

        private async Task<string> UploadFile(IFormFile formFile, string folderPath)
        {
            string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") 
                + "_" + formFile.FileName;

            string filePath = Path.Combine(folderPath, uniqueFileName);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
        #endregion
    }
}
