using System.Data;
using System.Xml;
using System.Reflection.PortableExecutable;
using System.Data.Common;
using DoanQuocVietBTTH2.Models;
using DoanQuocVietBTTH2.Data;
using Microsoft.AspNetCore.Mvc;
using DoanQuocVietBTTH2.Models.Process;
using Microsoft.EntityFrameworkCore;

namespace DoanQuocVietBTTH2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool EmployeeExists(string employeeID)
        {
            throw new NotImplementedException();
        }
        // GET: Employee
        public async Task< IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }
        private bool EmployeExists(string id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension != ".xls " && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("","Pleae choose excel file to upload! ")
                }
                else{
                    //rename file when upload to server
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excel",fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Employee object
                            var emp = new Employee();
                            //set values for attributes
                            emp.EmployeeID = dt.Rows[i][0].ToString();
                            emp.EmployeeName = dt.Rows[i][1].ToString();
                            emp.EmployeeBoPhan = dt.Rows[i][2].ToString();
                            emp.EmployeeLuong = dt.Rows[i][3].ToString();
                            //add object to Context
                            if (!EmployeeExists(emp.EmployeeID))
                            {
                                _context.Employee.Add(emp);    
                            }

                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }
                }
            } 
            return View();
        }

        
    }  
}