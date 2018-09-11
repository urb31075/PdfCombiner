// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   WCF Service interface to define operations
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CSRESTfulWCFServiceProvider
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using Utilities;

    /// <summary>
    /// WCF Service interface to define operations
    /// </summary>
    [ServiceContract]
    [DataContractFormat]
    internal interface IUserService
    {
        /// <summary>
        /// Definde operation contract
        /// </summary>
        /// <returns>
        /// Return All Users
        /// </returns>
        [WebGet(UriTemplate = "/User/All",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        List<User> GetAllUsers();

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        [WebInvoke(Method = "POST", UriTemplate = "/User/Create", 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void CreateUser(User user);

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        [WebInvoke(Method = "PUT",
           UriTemplate = "/User/Edit",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void UpdateUser(User user);

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [WebInvoke(Method = "DELETE",
          UriTemplate = "/User/Delete/{Id}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void DeleteUser(string id);
    }
}
