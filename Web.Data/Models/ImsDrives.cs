﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Web.Data.Models
{
    public partial class ImsDrives
    {
        public int ItdDriveId { get; set; }
        public int? ItaAssetId { get; set; }
        public string ItdType { get; set; }
        public string ItdCapacity { get; set; }
        public string ItdCreatedBy { get; set; }
        public string ItdCreatedByName { get; set; }
        public DateTime? ItdCreatedByDate { get; set; }
        public string ItdModifiedBy { get; set; }
        public string ItdModifiedByName { get; set; }
        public DateTime? ItdModifiedByDate { get; set; }
        public bool? ItdIsDelete { get; set; }
        [JsonIgnore]
        public virtual ImsAssets ItaAsset { get; set; }
    }
}