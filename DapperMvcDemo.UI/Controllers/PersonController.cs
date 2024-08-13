using Microsoft.AspNetCore.Mvc;
using DapperMvcDemo.Data.Models.Domain;
using DapperMvcDemo.Data.Repo;

namespace DapperMvcDemo.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepo _personRepo;

        public PersonController(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task<IActionResult> Index()
        {
            var people = await _personRepo.GetAllAsync();
            return View(people);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                var result = await _personRepo.DeleteAsync(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode(500, "Error deleting person");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                var result = await _personRepo.AddAsync(person);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode(500, "Error creating person");
                }
            }
            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                var result = await _personRepo.UpdateAsync(person);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode(500, "Error updating person");
                }
            }
            return View(person);
        }
    }
}
