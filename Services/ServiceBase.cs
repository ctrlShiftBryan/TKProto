﻿
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using TKData;
using TKData.Interfaces;
using TKModel;

namespace TKServices
{
    public abstract class ServiceBase
    {
        protected ServiceBase(
    
    
        IRepository<Category> repoCategories 
    	
    ,    IRepository<Resource> repoResources 
    	
    ,    IRepository<TestThing> repoTestThings 
    	
    ,    IRepository<SavedUndoCommand> repoSavedUndoCommands 
    	
    	)
            {
                    RepoCategories = repoCategories ;
        RepoResources = repoResources ;
        RepoTestThings = repoTestThings ;
        RepoSavedUndoCommands = repoSavedUndoCommands ;
    
    
            }
        
              protected IRepository<Category> RepoCategories ;
         protected IRepository<Resource> RepoResources ;
         protected IRepository<TestThing> RepoTestThings ;
         protected IRepository<SavedUndoCommand> RepoSavedUndoCommands ;
    }
}