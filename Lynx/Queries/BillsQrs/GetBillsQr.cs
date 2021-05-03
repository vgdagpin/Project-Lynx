using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.BillsQrs
{
    /// <summary>
    /// GetBillsQrHandler
    /// </summary>
    public class GetBillsQr : ITasq<IEnumerable<BillSummaryBO>>
    {
        public GetBillsQr(bool cacheResult = true)
        {
            CacheResult = cacheResult;
        }

        public bool CacheResult { get; }
    }
}
