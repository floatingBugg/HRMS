﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.Data.Models
{
    public partial class ImsAssign
    {
        public int ItasAssignId { get; set; }
        public int? ItasItaAssetId { get; set; }
        public int? ItasEtedEmployeeId { get; set; }
        public int? ItasItacCategoryId { get; set; }
        public int? ItasQuantity { get; set; }
        public DateTime? ItasAssignedDate { get; set; }
        public string ItasCreatedBy { get; set; }
        public string ItasCreatedByName { get; set; }
        public DateTime? ItasCreatedByDate { get; set; }
        public string ItasModifiedBy { get; set; }
        public string ItasModifiedByName { get; set; }
        public DateTime? ItasModifiedByDate { get; set; }
        public bool? ItasIsDelete { get; set; }
        [JsonIgnore]
        public virtual EmsTblEmployeeDetails ItasEtedEmployee { get; set; }
        [JsonIgnore]
        public virtual ImsAssets ItasItaAsset { get; set; }
        [JsonIgnore]
        public virtual ImsAssetsCategory ItasItacCategory { get; set; }
    }
}