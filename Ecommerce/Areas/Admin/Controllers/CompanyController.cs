using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using FileIO = System.IO.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.Models;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly string WEB_ROOT_PATH;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            var companys = _companyRepository.GetAll().ToList();

            return View(companys);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var company = new Company();
            if (id is not null && id != 0)
            {
                company = _companyRepository.Get(p => p.Id == id);
            }

            return View(company);
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Entry data wrong";
                return View(company);
            }

            if (company.Id == 0)
            {
                _companyRepository.Add(company);
            }
            else
            {
                _companyRepository.Update(company);
            }

            _companyRepository.Save();

            TempData["success"] = "Company was created";
            return RedirectToAction("Index");


        }

        [HttpGet("delete")]
        public IActionResult DeleteView(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var companyToDelete = _companyRepository.Get(p => p.Id == id);

            if (companyToDelete is null)
                return NotFound();

            return View("Delete", companyToDelete);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var companys = _companyRepository.GetAll().ToList();
            return Json(new { data = companys });
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var companyToDelete = _companyRepository.Get(p => p.Id == id);
            if (companyToDelete is null)
                return Json(new { success = false, message = "Company not found" });

            _companyRepository.Remove(companyToDelete);
            _companyRepository.Save();

            return Json(new { success = true, message = "Company deleted" });

        }
        #endregion
    }
}
