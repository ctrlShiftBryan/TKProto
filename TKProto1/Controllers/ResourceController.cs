using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TKCommon;
using TKCommon.Serialization;
using TKMVC.Views.Resource;
using TKModel;
using TKServices.Interfaces;

namespace TKMVC.Controllers
{
    public class ResourceController : Controller
    {
         private readonly IResourceService _serviceResource;
         public ResourceController(IResourceService iResourceService)
        {
            _serviceResource = iResourceService;
        }


        public ActionResult Edit(Guid? id)
        {

            
            var resource = _serviceResource.GetResource(id.Value);
            var allCategories = _serviceResource.GetAllCategories();
            var vm = new EditResourceViewModel {Resource = resource, AllCategories = allCategories};

            if (TempData["CommandResult"] != null)
            {
                var commandResult = TempData["CommandResult"] as CommandResult;
                if (commandResult != null)
                    vm.UndoGuid = commandResult.UndoCommandId;
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(Resource r)
        {
            var c = new UpdateResourceCommand {Resource = r};
            var res = c.Execute(_serviceResource);
            TempData["CommandResult"] = res;

            return RedirectToAction("Edit", r.Id);
        }

        [HttpPost]
        public ActionResult Undo(Guid? id)
        {



            var undo = _serviceResource.GetSaveUndoCommand(id.Value);
            var undoSer = undo.SerializedCommand;
            var u = new UpdateResourceCommand();
            var typeRegistry = new XmlStringSerializableTypeRegistry();
            typeRegistry.RegisterType(u.GetType());
            var ser = new XmlStringDeserializer(typeRegistry);
            var undoO = ser.Deserialize(undoSer) as UpdateResourceCommand;
            undoO.Execute(_serviceResource);

            return RedirectToAction("Edit", new { id = undoO.Resource.Id});
        }

    }

    public interface ICommand
    {
        CommandResult Execute(IResourceService iResourceService);
    }

    [Serializable]
    [XmlStringSerializable("12341234-1234-1234-1234-123412341234")]
   
    public class UpdateResourceCommand : ICommand
    {
        public bool IsUndo { get; set; }
        private Resource _resource;
        public Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        public UpdateResourceCommand()
        {
            
        }
        public CommandResult Execute(IResourceService iResourceService)
        {
            var result = new CommandResult();
            if(!IsUndo)
            {
               
                var undo = new UpdateResourceCommand
                                                 {Resource = iResourceService.GetResource(_resource.Id), IsUndo = true};

                var ser = new XmlStringSerializer();



                var serialized = ser.Serialize(undo);

                var suc = new SavedUndoCommand  
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    SerializedCommand = serialized
                };

                result.UndoCommandId = iResourceService.SaveUndoCommand(suc);


            }

            


            
            iResourceService.UpdateResourceCategories(_resource);
            return result;
        }
    }

    public class CommandResult
    {
        public Guid UndoCommandId { get; set; }
    }
}
