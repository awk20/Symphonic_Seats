﻿using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SymphonicSeats2.Models;

namespace SymphonicSeats2.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly CollectionItemRepository repository;

    // Home controller receives a repository 
    public HomeController(ILogger<HomeController> logger, CollectionItemRepository repository)
    {
        this.repository = repository;
        _logger = logger;
    }

    public IActionResult Index()
    {
        /* var ex = new ArgumentNullException("Foo");
        _logger.LogError(ex, "Error processing request for homepage"); */
        // Get data items and format as a view 
        var items = repository.Get();
        return View(items);
    }

    [Authorize]
    public IActionResult Cart()
    {
        return View();
    }

    // Decarator for listening for httpget of data
    // Cna get API data from ~/api/Items
    [HttpGet("/api/Items")]
    public ActionResult<IEnumerable<CollectionItem>> GetApi()
    {
        return Ok(repository.Get());
    }

    // used to get specific items in the databse
    [HttpGet("/api/Items/{id:int}")]
    public ActionResult<IEnumerable<CollectionItem>> FindItemApi(int id)
    {
        // Get all data and get first item or default where ID matches
        // using FindByID in ColletionItemRepository file
        var item = repository.FindById(id);
        // if nothing found return 404 item
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }


    public IActionResult Create()
    {
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
