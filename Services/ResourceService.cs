using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TKCommon;
using TKData;
using TKData.Interfaces;
using TKModel;
using TKServices.Interfaces;

namespace TKServices
{
    public class ResourceService : ServiceBase, IResourceService
    {


         

        //gets  single category or the root category
        public ResourceService(IRepository<Category> repoCategories, IRepository<Resource> repoResources, IRepository<TestThing> repoTestThings, IRepository<SavedUndoCommand> repoSavedUndoCommands) : base(repoCategories, repoResources, repoTestThings, repoSavedUndoCommands)
        {
        }

        public Category GetCategory(Guid? id, Paged paged)
        {
            var cat =
                id.HasValue
                    ? RepoCategories.Get(x => x.Id == id.Value)
                    : RepoCategories.Get(x => x.ParentId == null);



            DbEntityEntry<Category> entry = RepoCategories.Entry(cat);


            paged.Total = entry.Collection(x => x.Resources).Query().Count();

            entry.Collection(x => x.Resources).Query().Include(x => x.Categories)
                .Where(x => true)
                .OrderBy(x => x.Id)
                .Skip(paged.Start)
                .Take(paged.Length)
                .Load();
            entry.Collection(x => x.SubCategories).Query().Take(10).Load();

            LoadParent(entry);

            return entry.Entity;
        }
        public Resource GetResource(Guid id)
        {
            var resource = RepoResources.GetById(id);

            DbEntityEntry<Resource> entry = RepoResources.Entry(resource);

         //   var l = entry.Collection(x => x.Categories).IsLoaded;

            entry.Collection(x => x.Categories).Load();

          //  l = entry.Collection(x => x.Categories).IsLoaded;

            return entry.Entity;

        }
        public List<Category> GetAllCategories()
        {
           return RepoCategories.GetAll().ToList();
        }

        //loads all the parent categories for bread crumbs
        private void LoadParent(DbEntityEntry<Category> cat)
        {
            cat.Reference(x => x.ParentCategory).Load();

            if (cat.Entity.ParentId != null)
                LoadParent(RepoCategories.Entry(cat.Entity.ParentCategory));
        }
        public void Remove(Resource resource, Category cateogry)
        {
        

        
        }
        public void UpdateResourceCategories(Resource resource)
        {
            var fromDb = GetResource(resource.Id);

            var oCats = fromDb.Categories.Select(x => x.Id).ToList();
            var nCats = resource.Categories.Select(x => x.Id).ToList();

            var deletes = oCats.Where(x => !nCats.Contains(x));
            var adds = nCats.Where(x => !oCats.Contains(x));


            foreach (var c in deletes.ToList().Select(id => fromDb.Categories.Single(x => x.Id == id)))
            {
                fromDb.Categories.Remove(c);
            }

            foreach (var c in adds.Select(guid => new Category() { Id = guid}))
            {
                fromDb.Categories.Add(c);
                RepoResources.DbContext.Entry(c).State = EntityState.Unchanged;//unattached object
                RepoResources.DbContext.Entry(fromDb).Collection(x =>x.Categories).Load();
            }

           // RepoResources.Entry(resource).State = EntityState.Modified;
            RepoResources.Update(fromDb);
           // RepoResources.Entry(resource).State = EntityState.Modified;
          

            RepoResources.Save();
            RepoResources.DbContext.Entry(fromDb).Collection(x => x.Categories).Load();
            resource = fromDb;
        }
        public Guid SaveUndoCommand(SavedUndoCommand suc)
        {
           RepoSavedUndoCommands.Add(suc);
           RepoSavedUndoCommands.Save();
            return suc.Id;
        }

        public SavedUndoCommand GetSaveUndoCommand(Guid id)
        {
            var undo = RepoSavedUndoCommands.GetById(id);
            return undo;
        }

        public void Update(Resource resource)
        {
            RepoResources.Update(resource);     
            RepoResources.Save();
        }

    
       
    }
}
