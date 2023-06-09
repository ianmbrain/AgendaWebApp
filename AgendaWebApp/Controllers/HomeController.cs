﻿using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgendaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("NewIndex");
        }

    public IActionResult NewIndex()
    {
        var users = _userRepository.GetAll();

        List<UserViewModel> viewUserList = new List<UserViewModel>();

        foreach (IdentityUser i in users)
        {
            var viewUser = new UserViewModel
            {
                Email = i.Email,
                ActiveTaskCount = _userRepository.GetActiveTaskCount(i.Id),
                ActiveMinorTasks = _userRepository.GetActiveMinor(i.Id),
                ActiveRelevantTasks = _userRepository.GetActiveRelevant(i.Id),
                ActiveImportantTasks = _userRepository.GetActiveImportant(i.Id),
                FinishedTaskCount = _userRepository.GetFinishedTaskCount(i.Id),
                FinishedMinorTasks = _userRepository.GetFinishedMinor(i.Id),
                FinishedRelevantTasks = _userRepository.GetFinishedRelevant(i.Id),
                FinishedImportantTasks = _userRepository.GetFinishedImportant(i.Id)

            };

            viewUserList.Add(viewUser);
        }

        return View(viewUserList);
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