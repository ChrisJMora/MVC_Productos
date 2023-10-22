﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_TouristBay.Controllers
{
    public class SucursalController : Controller
    {
        // GET: SucursalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SucursalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SucursalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SucursalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SucursalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SucursalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SucursalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SucursalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}