using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TacoCatMVC.Models;

namespace TacoCatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindome)
        {
            string inputWord = palindome.InputWord;
            string revWord = " ";

            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];  
            }
            palindome.InputWord = revWord;

            //regular expression Regex;
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9]+","");
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9]+", "");
               

            if (revWord == inputWord)
            {
                palindome.IsPalindome = true;
                palindome.Message = $"Success {palindome.InputWord}is a Palindrome";
            }
            else
            {
                palindome.IsPalindome = false;
                palindome.Message = $"SORRY {palindome.InputWord}is NOT a Palindrome";
            }
            return View(palindome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}