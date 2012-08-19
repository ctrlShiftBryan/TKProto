using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TKModel
{
    [Serializable]
    public partial class Resource
    {
    
    }

    public partial class Category : IDisplay
    {
        public string Display
        {
            get { return this.Name; }
           
        }

        public string Value
        {
            get { return this.Id.ToString(); }
        }

        public bool IsLast { get; set; }
    }

    public interface IDisplay
    {
        string Display { get;  }
        string Value { get;  }
    }
}
