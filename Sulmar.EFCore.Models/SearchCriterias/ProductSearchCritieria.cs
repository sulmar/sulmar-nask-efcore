using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.EFCore.Models.SearchCriterias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class ProductSearchCritieria : SearchCriteria
    {
        public string Color { get; set; }
        public float? WeightFrom { get; set; }
        public float? WeightTo{ get; set; }
        public string BarCode { get; set; }

        public bool IsValid => string.IsNullOrEmpty(BarCode);


    }
}
