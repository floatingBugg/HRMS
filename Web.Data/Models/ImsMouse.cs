﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsMouse
    {
        public int ItmMouseId { get; set; }
        public int? ItaAssetId { get; set; }
        public string ItmModelNo { get; set; }
        public string ItmCompanyName { get; set; }
        public string ItmCreatedBy { get; set; }
        public string ItmCreatedByName { get; set; }
        public DateTime? ItmCreatedBy1 { get; set; }
        public string ItmModifiedBy { get; set; }
        public string ItmModifiedByName { get; set; }
        public DateTime? ItmModifiedBy1 { get; set; }
        public bool? ItmIsDelete { get; set; }
        [JsonIgnore]
        public virtual ImsAssets ItaAsset { get; set; }
    }
}