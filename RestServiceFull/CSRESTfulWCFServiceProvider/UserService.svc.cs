// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.svc.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   WCF Service class to provide operations
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CSRESTfulWCFServiceProvider
{
    using System.Collections.Generic;
    using Utilities;

    /// <summary>
    /// WCF Service class to provide operations
    /// </summary>
    internal class UserService : IUserService
    {
        #region Methods

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Return user list</returns>
        public List<User> GetAllUsers()
        {
            return User.UserObject.GetAllUsers();
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        public void CreateUser(User user)
        {
            User.UserObject.CreateUser(user);
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        public void UpdateUser(User user)
        {
            User.UserObject.UpdateUser(user);
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void DeleteUser(string id)
        {
            User.UserObject.DeleteUser(id);
        }

        #endregion
    }
}
