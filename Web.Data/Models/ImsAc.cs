﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsAc
    {
        public int ItaAcId { get; set; }
        public int? ItaAssetId { get; set; }
        public string ItaCompanyName { get; set; }
        public string ItaSize { get; set; }
        public string ItaCreatedBy { get; set; }
        public string ItaCreatedByName { get; set; }
        public DateTime? ItaCreatedByDate { get; set; }
        public string ItaModifiedBy { get; set; }
        public string ItaModifiedByName { get; set; }
        public DateTime? ItaModifiedByDate { get; set; }
        public bool? ItaIsDelete { get; set; }
        [JsonIgnore]
        public virtual ImsAssets ItaAsset { get; set; }
    }
}