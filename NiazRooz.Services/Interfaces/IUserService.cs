using NiazRooz.DomainClasses;
using NiazRooz.DTO;
using System;
using System.Collections.Generic;
using System.Text;


namespace NiazRooz.Services
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string username);
        string GetUserAvatarByUserName(string username);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);
        int GetUserIdByUserName(string userName);

        void DeleteUser(int userId);

        #region User Panel
        InformationUserViewModel GetUserInformationById(int userId);
        InformationUserViewModel GetUserInformationByUserName(string username);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        EditProfileViewModel GetDataForEditProfileUser(string username);
        void EditProfile(string username, EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string username);

        void ChangeUserPassword(string userName, string newPassword);

        #endregion

        #region Admin Panel

        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        UserForAdminViewModel GetUsersForFilter(int pageId = 1, string filterEmail = "", string filterUserName = "");
        UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        int AddUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
        void UndoUsersByAdmin(int userId);

        #endregion
    }
}
