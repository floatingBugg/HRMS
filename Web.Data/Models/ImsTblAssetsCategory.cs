﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsTblAssetsCategory
    {
        public ImsTblAssetsCategory()
        {
            ImsTblAssests = new HashSet<ImsTblAssests>();
        }

        public int ItacAcId { get; set; }
        public string ItacCategory { get; set; }
        public string ItacCreatedBy { get; set; }
        public string ItacCreatedByName { get; set; }
        public DateTime? ItacCreatedByDate { get; set; }
        public string ItacModifiedBy { get; set; }
        public string ItacModifiedByName { get; set; }
        public DateTime? ItacModifiedByDate { get; set; }
        public bool? ItacIsDelete { get; set; }

        public virtual ICollection<ImsTblAssests> ImsTblAssests { get; set; }
    }
}