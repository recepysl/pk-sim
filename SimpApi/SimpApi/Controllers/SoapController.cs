using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceReferenceSoap;

namespace SimpApi.Controllers
{


    public class Application
    {
        private string _appName;

        public int ApplicationId { get; set; }
        public string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SoapController : ControllerBase
    {
        public SoapController()
        {
        }

        [NonAction]
        public Application GetApplication()
        {
            Application application = new ();
            application.ApplicationId = 1;
            application.AppName = "Test";
            return application;
        }

        [HttpGet]
        public string Get()
        {
            CityServicePortClient client = new CityServicePortClient();
            GetCityRequest cityRequest = new();
            cityRequest.cityCode = "34";
            cityRequest.auth = new Authentication();
            cityRequest.auth.appKey = "1";
            cityRequest.auth.appSecret = "2";
            var response = client.GetCity(cityRequest);
            if (response.result.status == "1")
            {
                string name = response.city.cityName;
                return name;
            }

            return string.Empty;
        }
    }
}
