﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetCategoryCredential
    {
        public int categoryId { get; set; }
        public string categoryname { get; set; }

        public string createdby { get; set; }

        public string createdbyname { get; set; }

        public DateTime createdbydate = new DateTime();

        public string modifiedby { get; set; }
        public string modifiedbyname { get; set; }

        public DateTime modifiedbydate = new DateTime();

        public bool isdelete { get; set; }
    }
}
