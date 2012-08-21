using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACRL.EFData.Interfaces;
using ACRL.Repo;
using TKModel;

namespace TKServices
{
    public class UndoService : ServiceBase
    {
        public UndoService(IRepository<Category> repoCategories, IRepository<Resource> repoResources, IRepository<TestThing> repoTestThings, IRepository<SavedUndoCommand> repoSavedUndoCommands) : base(repoCategories, repoResources, repoTestThings, repoSavedUndoCommands)
        {
        }

        
    }
}
