using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;
using System.Security.Claims;

namespace MyTasks.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfUserWork _unitOfUserWork;
        #region Prop
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMyTaskRepository _myTaskRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public UserService(IUnitOfUserWork unitOfUserWork, IHttpContextAccessor contextAccessor, 
            IMyTaskRepository myTaskRepository, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfUserWork = unitOfUserWork;
            _contextAccessor = contextAccessor;
            _myTaskRepository = myTaskRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        public async Task GetAllUserAsync(SearchUsersModel searchUsersModel)
        {
            GetUserModel usersInfo = new()
            {
                SearchText = searchUsersModel.SearchText,
                UsersShow = searchUsersModel.PageItemShow,
                SkipUsers = (searchUsersModel.CurrentPage - 1) * searchUsersModel.PageItemShow
            };
            
            var users = await _unitOfUserWork.Users.GetAllUserAsync(usersInfo);
            var roles = await _unitOfUserWork.Roles.GetAllRolesAsync();
            var userRoles = await _unitOfUserWork.UserRoles.GetAllUserRolesAsync();

            searchUsersModel.TotalPages = usersInfo.TotalPages;

            foreach (var user in users)
            {
                var uRole = userRoles.FirstOrDefault(r => r.UserId == user.Id);
                if (uRole == null) user.RoleName = "User";
                else
                {
                    user.RoleId = uRole.RoleId;
                    user.RoleName = roles.FirstOrDefault(role => role.Id == uRole.RoleId).Name;
                }
            }
            searchUsersModel.Tasks = users ?? new List<UserModel>();
        }



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
                    DeleteFile(imageFolderPath, existingUser.PhoneNumber);
                }

                var uniqueFileName = await UploadFileAsync(updatedUser.ProfilePicture, imageFolderPath);
                existingUser.PhoneNumber = uniqueFileName;
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            await _myTaskRepository.UpdateUserAsync(existingUser);
        }

        private async Task<string> UploadFileAsync(IFormFile formFile, string folderPath)
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
        private void DeleteFile(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            int aa = 5;
        }
        #endregion
    }
}
