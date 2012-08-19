using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TKMVC.Views.Resource
{
    public class EditResourceViewModel
    {
        public TKModel.Resource Resource { get; set; }

        private List<TKModel.Category> _allCategories;
        public List<TKModel.Category> AllCategories
        {
            get
            {
                var exclude = Resource.Categories.Select(x => x.Id).ToList();
                return _allCategories.Where(x => !exclude.Contains(x.Id)).ToList();
            }
            set { _allCategories = value; }
        }

        public Guid UndoGuid { get; set; }
    }
}