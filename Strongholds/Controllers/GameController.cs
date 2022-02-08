﻿using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Strongholds.Models;
using Strongholds.ViewModels;
using Strongholds.Filters;
using StrongholdsUtil.Models;

namespace Strongholds.Controllers
{
    [AuthorizeUser]
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private APIUtils API;

        public GameController(ILogger<GameController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            API = new APIUtils(clientFactory);
        }

        public async Task<IActionResult> Index()
        {
            var result = await API.GetResultFromAsync("/Status/");


            var model = new GameView();
            return View(model);
        }

        // Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}