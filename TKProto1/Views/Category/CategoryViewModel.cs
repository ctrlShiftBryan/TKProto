using System.Collections.Generic;
using DT.Common;
using TKCommon;
using TKModel;

namespace TKMVC.Views.Category
{
    public class CategoryViewModel
    {
        public TKModel.Category Category { get; set; }
        public Paged Paged { get; set; }
        public TKModel.Category[] BreadCrumbCategoryList
        {
            get
            {
                var array = new List<TKModel.Category>();
                var currentCat = Category;



                array.Add(currentCat);

                while (currentCat.ParentId != null)
                {
                    currentCat = currentCat.ParentCategory;


                    array.Add(currentCat);

                }
                array[0].IsLast = true;
                array.Reverse();
                return array.ToArray();

            }
        }


    }
}