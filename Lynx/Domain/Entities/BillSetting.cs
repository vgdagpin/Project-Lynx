﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class BillSetting
    {
        public Guid ID { get; set; }
        public short BillID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
