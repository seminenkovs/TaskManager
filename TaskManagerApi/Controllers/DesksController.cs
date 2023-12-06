using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers
{
    public class DesksController : Controller
    {
        // GET: DesksController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DesksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DesksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesksController/Create
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

        // GET: DesksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DesksController/Edit/5
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

        // GET: DesksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DesksController/Delete/5
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
