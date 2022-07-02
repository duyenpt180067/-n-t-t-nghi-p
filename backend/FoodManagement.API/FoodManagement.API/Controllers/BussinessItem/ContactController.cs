
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.BussinessItem;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.BussinessItem;
using FoodManagement.Core.Interfaces.Infrastructure.IService.BussinessItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodManagement.API.Controllers.BussinessItem
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : BaseSubjectController<Contact>
    {
        #region Fields
        IContactRepository _ContactRepository;
        IContactService _ContactService;
        #endregion
        public ContactController(IContactRepository ContactRepository, IContactService ContactService) :base(ContactRepository, ContactService)
        {
            _ContactRepository = ContactRepository;
            _ContactService = ContactService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
