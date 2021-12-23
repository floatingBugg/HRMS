﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class ImsAssetsVM
    {
        public int ItaAssetId { get; set; }
        public int? ItacCategoryId { get; set; }
        public string ItaAssetName { get; set; }
        public int? ItaQuantity { get; set; }
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
    }
}
