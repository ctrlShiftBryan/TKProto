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
using System.Xml.Serialization;
using TKModel.Interfaces;
namespace TKModel
{
    public partial class Category : IEntity
    {
        public Category()
        {
            this.SubCategories = new List<Category>();
            this.Resources = new List<Resource>();
        }
    
    		
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
    
    
    			public string Name { get; set; }
    			public Nullable<System.Guid> ParentId { get; set; }
    			public System.DateTime CreatedDate { get; set; }
    			public int CreatedBy { get; set; }
    			public Nullable<System.DateTime> ModifiedDate { get; set; }
    			public Nullable<int> ModifiedBy { get; set; }
    	
    		    public List<Category> SubCategories { get; set; }
    			
                public Category ParentCategory { get; set; }
                  [XmlIgnore]
                public List<Resource> Resources { get; set; }
    	}
    
}
