﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsTblAssets
    {
        public ImsTblAssets()
        {
            ImsTblLaptop = new HashSet<ImsTblLaptop>();
        }

        public int ItaAssetId { get; set; }
        public string ItaAssetName { get; set; }
        public int ItacCategoryIdFk { get; set; }
        public int? ItaQuantity { get; set; }
        public long? ItaCost { get; set; }
        public string ItaSerialNo { get; set; }
        public string ItaModel { get; set; }
        public string ItaType { get; set; }
        public string ItaCompanyName { get; set; }
        public string ItaDescription { get; set; }
        public DateTime? ItaPurchaseDate { get; set; }
        public int? ItaAssignedToId { get; set; }
        public string ItaAssignedToName { get; set; }
        public string ItaCreatedBy { get; set; }
        public string ItaCreatedByName { get; set; }
        public DateTime? ItaCreatedByDate { get; set; }
        public string ItaModifiedBy { get; set; }
        public string ItaModifiedByName { get; set; }
        public DateTime? ItaModifiedByDate { get; set; }
        public bool? ItaIsDelete { get; set; }
       
        public virtual EmsTblEmployeeDetails ItaAssignedTo { get; set; }
        
        public virtual ImsTblAssetCategory ItacCategoryIdFkNavigation { get; set; }
        public virtual ICollection<ImsTblLaptop> ImsTblLaptop { get; set; }
    }
}