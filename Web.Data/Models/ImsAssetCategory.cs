﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsAssetCategory
    {
        public ImsAssetCategory()
        {
            ImsAssets = new HashSet<ImsAssets>();
        }

        public int ItcCategoryId { get; set; }
        public string ItcCategoryName { get; set; }
        public string ItcCreatedBy { get; set; }
        public string ItcCreatedByName { get; set; }
        public DateTime? ItcCreatedByDate { get; set; }
        public string ItcModifiedBy { get; set; }
        public string ItcModifiedByName { get; set; }
        public DateTime? ItcModifiedByDate { get; set; }
        public bool? ItcIsDelete { get; set; }

        public virtual ICollection<ImsAssets> ImsAssets { get; set; }
    }
}