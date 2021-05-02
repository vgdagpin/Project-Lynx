using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.Models;

namespace Lynx.Interfaces
{
    public interface ILynxAPI
    {
        Task<LynxResponse<T>> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default);
        Task<LynxResponse<T>> PostAsync<T, TBody>(string requestUri, TBody body, CancellationToken cancellationToken = default);
    }
}
