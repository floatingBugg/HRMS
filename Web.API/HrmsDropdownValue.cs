﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.API
{
    public partial class HrmsDropdownValue
    {
        public int HdvValueId { get; set; }
        public int? HdvHdDropdownId { get; set; }
        public string HdvValueName { get; set; }
        public string HdvCreatedBy { get; set; }
        public string HdvCreatedByName { get; set; }
        public DateTime? HdvCreatedByDate { get; set; }
        public bool? HdvIsDelete { get; set; }

        public virtual HrmsDropdown HdvHdDropdown { get; set; }
    }
}