using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Parse3.Data;
using Parse3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Parse3.Controllers
{
    
    public class HomeController : Controller
    {

        public ApplicationDbContext _context;

        
        private readonly ILogger<HomeController> _logger;
        
        private IWebHostEnvironment _hostingEnvironment;
        

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            
        }

        public IActionResult Index()
        {
            
            var orders = _context.Orders.ToList();
            var customers = _context.Customers.ToList();
            var costrems = _context.CostRems.ToList();
            var moveDBs = _context.MoveDBs.ToList();
            var price = _context.Prices.ToList();
            var model = new ViewModel { Orders = orders, Customers = customers, CostRems=costrems, Prices=price, MoveDBs=moveDBs };
            return View(model);
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


        public IActionResult Import()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "UploadExcel";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table table-bordered'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    MatchCollection kodi;
                    int cnt = 0;

                    foreach (var t in _context.Orders)
                    {
                        if (t != null)
                        {
                            _context.Orders.Remove(t);
                        }

                    }
                    foreach (CodeRem rem in _context.CodeRems)
                    {
                        if (rem != null)
                        {
                            _context.CodeRems.Remove(rem);
                        }
                    }
                    foreach (CostRem rem in _context.CostRems)
                    {
                        if (rem != null)
                        {
                            _context.CostRems.Remove(rem);
                        }
                    }
                    foreach (Customer rem in _context.Customers)
                    {
                        if (rem != null)
                        {
                            _context.Customers.Remove(rem);
                        }
                    }
                    int num = 1;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {

                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");

                        }
                        sb.AppendLine("</tr>");

                        string stroka;
                        stroka = row.GetCell(4).ToString();
                        stroka = stroka.Trim();
                        kodi = Regex.Matches(stroka, @"\d+\.\d+");
                        string str;
                        string str1;
                        str = row.GetCell(2).ToString();
                        str1 = row.GetCell(1).ToString();

                        var orders = new Order[]
                             {
                             new Order{num=num , name= "Заказ"+num, date=str, nameCustomers=str1},
                                };
                        foreach (var m in orders)
                        {
                            _context.Orders.Add(m);
                        }
                        _context.SaveChanges();
                        string[] matches = kodi.Cast<Match>().Select(m => m.Value).ToArray();
                        num++;
                    }
                    



                    var costprice = _context.MoveDBs.FromSqlRaw("SELECT * FROM MoveDBs").ToList();
                    var cost = _context.Prices.FromSqlRaw("SELECT * FROM Prices").ToList();
                    var ord = _context.Orders.FromSqlRaw("SELECT * FROM Orders").ToList();
                    
                    int d = 0;
                    foreach (var pr in costprice)
                    {
                        foreach (var cs in cost)
                        {

                            if (pr.coderab == cs.coderab)
                            {
                                var costrm = new CostRem[]
                                {
                                        new CostRem{coderab=pr.coderab, name=cs.name, cost=cs.cost, nid=pr.name}
                                };
                                foreach (CostRem c in costrm)
                                {
                                    _context.CostRems.Add(c);
                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                    int number = 0;
                    var costrem = _context.CostRems.FromSqlRaw("SELECT * FROM CostRems").ToList();
                    foreach (var or in ord)
                    {
                        
                        foreach (var cr in costrem)
                    {
                            number++;
                            if (or.num != cr.nid)
                            {
                                continue;
                            }
                            else
                            {
                                d += cr.cost;
                                
                            } 
                        }
                        var cust = new Customer[]
                                   {
                                        new Customer{nid1=or.num, Date=or.date, name=or.nameCustomers, EndCost=d}
                                   };
                        foreach (Customer c in cust)
                        {
                            _context.Customers.Add(c);
                        }
                        d = 0;

                    }
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();

            return this.Content(sb.ToString());
        }
       
    }
}

