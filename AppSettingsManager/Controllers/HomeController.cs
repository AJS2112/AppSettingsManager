using AppSettingsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AppSettingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private TwilioSettigs _twilioSettigs;
        private readonly IOptions<TwilioSettigs> _twilioOptions;
        private readonly IOptions<SocialLoginSettings> _socialLoginOptions;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptions<TwilioSettigs> twilioOptions, TwilioSettigs twilioSettigs, IOptions<SocialLoginSettings> socialLoginOptions)
        {
            _logger = logger;
            _configuration = configuration;
            _twilioOptions = twilioOptions;

            //_twilioSettigs = new TwilioSettigs();
            //configuration.GetSection("Twilio").Bind(_twilioSettigs);

            _twilioSettigs = twilioSettigs;
            _socialLoginOptions = socialLoginOptions;
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _configuration.GetValue<string>("SendGridKey");

            // IConfiguration
            //ViewBag.TwilioAuthToken = _configuration.GetSection("Twilio").GetValue<string>("AuthToken");
            //ViewBag.TwilioAccountSid = _configuration.GetValue<string>("Twilio:AccountSid");
            //ViewBag.TwilioPhoneNumber = _twilioSettigs.PhoneNumber;

            // IOptions
            //ViewBag.TwilioAuthToken = _twilioOptions.Value.AuthToken;
            //ViewBag.TwilioAccountSid = _twilioOptions.Value.AccountSid;
            //ViewBag.TwilioPhoneNumber = _twilioOptions.Value.PhoneNumber;

            ViewBag.TwilioAuthToken = _twilioSettigs.AuthToken;
            ViewBag.TwilioAccountSid = _twilioSettigs.AccountSid;
            ViewBag.TwilioPhoneNumber = _twilioSettigs.PhoneNumber;

            ViewBag.BottomLevelSetting = _configuration.GetValue<string>("FirstLevelSetting:SecondLevelSetting:BottomLevelSetting");

            ViewBag.FacebookKey = _socialLoginOptions.Value.FacebookSettings.Key;
            ViewBag.GoogleKey = _socialLoginOptions.Value.GoogleSettings.Key;

            ViewBag.ConnectionString = _configuration.GetConnectionString("AppSettingsManagerDb");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}