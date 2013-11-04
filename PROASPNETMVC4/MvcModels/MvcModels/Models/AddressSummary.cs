using System.Web.Mvc;


namespace MvcModels.Models
{
    public class AddressSummary
    {

        //[Bind(Include = "City")]
        public string City { get; set; }
        public string Country { get; set; }
    }
}