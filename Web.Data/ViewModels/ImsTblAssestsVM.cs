﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Web.Data.ViewModels
{
    public partial class ImsTblAssestsVM
    {
        public int ItaAssetId { get; set; }
        public int? ItaCategoryId { get; set; }
        public string ItaAssetsName { get; set; }
        public int ItaAssetsSrNo { get; set; }
        public string ItaAssetDetails { get; set; }
        public int? ItaQuantity { get; set; }
        public int? ItaCost { get; set; }
        public DateTime? ItaPurchasingDate { get; set; }
        public string ItaAssignedTo { get; set; }
        public bool ItaIsDelete { get; set; }
        public string ItaCreatedBy { get; set; }
        public string ItaCreatedByName { get; set; }
        public DateTime? ItaCreatedByDate { get; set; }
        public string ItaModifiedBy { get; set; }
        public string ItaModifiedByName { get; set; }
        public DateTime? ItaModifiedByDate { get; set; }
        [JsonIgnore]
        public virtual ImsTblAssetsCategoryVM ItaCategory { get; set; }
    }
}