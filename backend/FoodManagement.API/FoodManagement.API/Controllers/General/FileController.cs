using FoodManagement.Core.Consts;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interface.IService.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FoodManagement.API.Controllers.General
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        IStorageService _storageService;
        ServiceResult serviceResult;
        dynamic res;
        Core.Consts.EnumResource _const;
        public FileController(IStorageService storageService)
        {
            _storageService = storageService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                var url = _storageService.UploadFile(file);
                return Ok(url);
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        [HttpPost]
        [Route("Multiple")]
        public IActionResult UploadMultipleFile(List<IFormFile> files)
        {
            try
            {
                var urls = _storageService.UploadMultipleFile(files);
                return Ok(urls);
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
    }
}
