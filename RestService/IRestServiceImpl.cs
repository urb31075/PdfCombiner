// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRestServiceImpl.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the IRestServiceImpl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RestService
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// The RestServiceImpl interface.
    /// </summary>
    [ServiceContract]
    public interface IRestServiceImpl
    {
        /// <summary>
        /// The xml data.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "xml/{id}")]
        string XmlData(string id);

        /// <summary>
        /// The json data.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "json/{id}")]
        string JsonData(string id);
    }
}