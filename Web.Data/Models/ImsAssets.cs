﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.Data.Models
{
    public partial class ImsAssets
    {
        public ImsAssets()
        {
            ImsAssign = new HashSet<ImsAssign>();
        }

        public int ItaAssetId { get; set; }
        public int? ItacCategoryId { get; set; }
        public string ItaAssetName { get; set; }
        public int? ItaQuantity { get; set; }
        public int? ItaRemaining { get; set; }
        public int? ItaAssignQuantity { get; set; }
        public long? ItaCost { get; set; }
        public string ItaSerialNo { get; set; }
        public string ItaModel { get; set; }
        public string ItaCompanyName { get; set; }
        public string ItaType { get; set; }
        public DateTime? ItaPurchaseDate { get; set; }
        public string ItaSize { get; set; }
        public string ItaCondition { get; set; }
        public string ItaGeneration { get; set; }
        public string ItaRam { get; set; }
        public string ItaProcessor { get; set; }
        public string ItaStorage { get; set; }
        public string ItaHardriveType { get; set; }
        public string ItaCreatedBy { get; set; }
        public string ItaCreatedByName { get; set; }
        public DateTime? ItaCreatedByDate { get; set; }
        public string ItaModifiedBy { get; set; }
        public string ItaModifiedByName { get; set; }
        public DateTime? ItaModifiedByDate { get; set; }
        public bool? ItaIsDelete { get; set; }

        public virtual ImsAssetsCategory ItacCategory { get; set; }
        public virtual ICollection<ImsAssign> ImsAssign { get; set; }
    }
}