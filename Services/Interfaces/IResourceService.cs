using System;
using System.Collections.Generic;
using DT.Common;
using TKCommon;
using TKModel;

namespace ACRL.Repo.Interfaces
{
    public interface IResourceService
    {
        Category GetCategory(Guid? id, Paged paged);
        Resource GetResource(Guid id);
        List<Category> GetAllCategories();
        void Update(Resource resource);

        void UpdateResourceCategories(Resource resource);
        Guid SaveUndoCommand(SavedUndoCommand suc);
        SavedUndoCommand GetSaveUndoCommand(Guid id);
    }
}