using System;
using System.Threading.Tasks;
using UserManager.BL.Models;
using UserManager.Core.Enums;
using UserManager.Core.Extentions;
using UserManager.DAL.Entities;
using UserManager.DAL.Repository;

namespace UserManager.BL.Services.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task<UserInfo> GetUserInfo(int id)
        {
            UserInfo userInfo = await _userInfoRepository.GetById(id);

            if (userInfo == null)
                throw new ArgumentNullException($"User with id { id } not found");

            return userInfo;
        }

        public async Task<Response> CreateUserAsync(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException("CreateUserAsync method got null argument");

            var dbEnity = await _userInfoRepository.GetById(userInfo.ID);

            if (dbEnity != null)
                return new Response
                {
                    Success = false,
                    ErrorId = ErrorIntIdEnum.EntityAlreadyExists,
                    ErrorMsg = $"User with id {userInfo.ID} already exist"
                };

            await _userInfoRepository.InsertAsync(userInfo);

            return new Response
            {
                Success = true,
                ErrorId = ErrorIntIdEnum.NoError,
                User = ToUser(userInfo)
            };
        }

        public async Task<RemoveUserResponse> RemoveUserAsync(RemoveUserRequest removeUserRequest)
        {
            if (removeUserRequest == null)
                throw new ArgumentNullException("RemoveUserAsync method got null argument");

            var dbEnity = await _userInfoRepository.GetById(removeUserRequest.RemoveUser.Id);

            if (dbEnity == null)
                return new RemoveUserResponse
                {
                    ErrorId = ErrorIntIdEnum.EntityNotFound,
                    Msg = $"User with id { removeUserRequest.RemoveUser.Id } not found",
                    Success = false
                };

            await _userInfoRepository.DeleteAsync(dbEnity);

            return new RemoveUserResponse
            {
                Msg = $"User with id { removeUserRequest.RemoveUser.Id } was removed",
                Success = true,
                User = dbEnity
            };
        }

        public async Task<SetStatusResponse> SetStatusAsync(SetStatusRequest setStatusRequest)
        {
            if (setStatusRequest == null)
                throw new ArgumentNullException("SetStatusAsync method got null argument");

            int id;
            if (Int32.TryParse(setStatusRequest.Id, out id) == false)
                throw new ArgumentException("User id is not numeric value");

            var dbEnity = await _userInfoRepository.GetById(id);

            if (dbEnity == null)
                throw new ArgumentNullException($"User with id { setStatusRequest.Id } not found");

            var serializedBeforeChange = dbEnity.Serialize();

            UserStatusEnum newStatus = (UserStatusEnum)Enum.Parse(typeof(UserStatusEnum), setStatusRequest.NewStatus);
            dbEnity.Status = newStatus;

            var serializedAfterChange = dbEnity.Serialize();


            if (serializedBeforeChange != serializedAfterChange)
                await _userInfoRepository.UpdateAsync(dbEnity);

            return ToSetStatusResponse(dbEnity);
        }

        #region private
        private User ToUser(UserInfo userInfo)
        {
            return new User
            {
                Id = userInfo.ID,
                Name = userInfo.Name,
                Status = userInfo.Status
            };
        }

        private SetStatusResponse ToSetStatusResponse(UserInfo userInfo)
        {
            return new SetStatusResponse
            {
                Id = userInfo.ID,
                Name = userInfo.Name,
                Status = userInfo.Status
            };
        }


        #endregion


    }
}
