using DoanQuocVietBTTH2.Data;
using Microsoft.AspNetCore.Mvc;
namespace DoanQuocVietBTTH2.Controllers
{
    public class StudentController : Controller{
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(StudentController std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }return View(std);
        }
    }
}