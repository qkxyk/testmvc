using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestWeb.Models;
using TestWeb.Services;
using TestWeb.setting;

namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IOptions<ConnectionOptions> _options;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ICinemaService cinemaService, IOptions<ConnectionOptions> options, IWebHostEnvironment webHostEnvironment)
        {
            this._cinemaService = cinemaService;
            this._options = options;
            this._webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "电影院列表";
            var cinema = await _cinemaService.GetAllAsync();
            return View(cinema);
        }

        public IActionResult Add()
        {
            ViewBag.Title = "添加电影院";
            return View(new Cinema());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Cinema model)
        {
            ViewBag.Title = "添加电影院";
            if (ModelState.IsValid)
            {
                await _cinemaService.AddAsync(model);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int cinemaId)
        {
            var cinema = await _cinemaService.GetByIdAsync(cinemaId);
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Cinema model)
        {
            if (ModelState.IsValid)
            {
                var exist = await _cinemaService.GetByIdAsync(Id);
                if (exist == null)
                {
                    return NotFound();
                }
                exist.Name = model.Name;
                exist.Location = model.Location;
                exist.Capacity = model.Capacity;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Load(IFormFile file)
        {
            /*
            var fileExtension = Path.GetExtension(file.FileName);
            //判断后缀是否是图片
            //const string fileFilt = ".gif|.jpg|.jpeg|.png";
            string webRootPath = _webHostEnvironment.WebRootPath;//wwwroot文件夹

            var filePath = Path.Combine(webRootPath, "Images");//物理路径,不包含头像名称
                                                               //如果路径不存在，创建路径
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            //filePath = Path.Combine(filePath, ext);//头像的物理路径
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            */
            var fileExtension = Path.GetExtension(file.FileName);
            string webRootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, "Images");
            var ff = Path.Combine(filePath, file.FileName);
            var fileName = file.FileName.Trim('"');//获取文件名
            fileName = _webHostEnvironment.WebRootPath + $@"\Images\{fileName}";//指定文件上传的路径
                                                                         //size += file.Length;
            using (FileStream fs = System.IO.File.Create(ff/*fileName*/))//创建文件流
            {
                file.CopyTo(fs);//将上载文件的内容复制到目标流
                fs.Flush();//清除此流的缓冲区并导致将任何缓冲数据写入
            }
            return Ok();
        }
    }
}
