using System;
using System.Web.Mvc;
using TKCommon;
using TKMVC.Views.Category;
using TKServices;
using TKServices.Interfaces;

namespace TKMVC.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IResourceService _serviceResource;
        public CategoryController(IResourceService iResourceService)
        {
            _serviceResource = iResourceService;
        }
     
      
        public ActionResult Index(Guid? id)
        {
            var page = new Paged {Length = 20};
            var vm = new CategoryViewModel
                         {
                             Category = _serviceResource.GetCategory(
                                 id,
                                 page
                                 ),
                             Paged = page
                         };

            return View(vm);
        }

        
       
    }
}
