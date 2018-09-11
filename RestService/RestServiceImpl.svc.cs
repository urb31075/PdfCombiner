// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RestServiceImpl.svc.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the RestServiceImpl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RestService
{
    /// <summary>
    /// The rest service impl.
    /// </summary>
    public class RestServiceImpl : IRestServiceImpl
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
        public string XmlData(string id)
        {
            return "XML You requested product " + id;   // Вызов: http://localhost:35798/RestServiceImpl.svc/xml/20
        }

        /// <summary>
        /// The json data.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string JsonData(string id)
        {
            return "JSON You requested product " + id; // Вызов http://localhost:35798/RestServiceImpl.svc/json/20
        }
    }
}