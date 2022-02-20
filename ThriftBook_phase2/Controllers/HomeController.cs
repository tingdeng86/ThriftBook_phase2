﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.Repositories;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
            
        //Display Index with Detail view
        public IActionResult Index()
        {
            var bookView = _context.Book;
            return View(bookView);
        }        

        public ActionResult Details(int bookID)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            BookVM bVM = bdRepo.Get(bookID);
            
            return View(bVM);

        }        

        [HttpGet]
        public ActionResult Edit(int bookID)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            BookVM bVM = bdRepo.Get(bookID);

            return View(bVM);
        }

        [HttpPost]
        public ActionResult Edit(BookVM bVM)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            bdRepo.Update(bVM);
            return RedirectToAction(nameof(Details), new { Title = bVM.Title, Author = bVM.Author, Genre = bVM.Genre, BookQuality = bVM.BookQuality, BookQuantity = bVM.BookQuantity, Price = bVM.Price, BookID = bVM.BookID });

        }

        [HttpGet]
        public ActionResult Create()
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind("Title, Author, Genre, BookQuality, BookQuantity, BookPhoto, Price, StoreName")] BookVM bVM)
        {
            ViewData["Message"] = "";
            Book book;
            try
            {
                BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
                book = bdRepo.Add(bVM);

                ViewData["Message"] = "Created successfully";
                return RedirectToAction("Index", "Home", new { Title = bVM.Title, Author = bVM.Author, Genre = bVM.Genre, BookQuality = bVM.BookQuality, BookQuantity = bVM.BookQuantity, Price = bVM.Price, BookID = bVM.BookID });
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
                return View(bVM);
            }
        }

        public ActionResult Delete(int id)
        {
            ViewData["Message"] = "";
            try
            {
                BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
                bdRepo.Delete(id);
                ViewData["Message"] = "Deleted successfully";
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
            }
            return RedirectToAction("Index", "Home", new { message = ViewData["Message"] });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
