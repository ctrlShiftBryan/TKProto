//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using TKModel.Interfaces;
namespace TKModel
{
    public partial class SavedUndoCommand : IEntity
    {
    		
    		private System.Guid _id;
    
            object IEntity.Id
            {
                get { return _id; }
            }
    
    
            public System.Guid Id
            {
                get { return _id; }
                set { _id = value; }
            }
    
    
    			public string SerializedCommand { get; set; }
    			public System.DateTime CreatedDate { get; set; }
    	}
    
}
