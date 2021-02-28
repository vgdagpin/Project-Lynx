using System;
using Lynx.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace Lynx.WebAPI.Common
{
    public class DataSecure : IDataSecure
    {
        private readonly IDataProtector protectorSessionBase;
        private readonly IDataProtector protector;

        private readonly IAppUser currentUser;

        public DataSecure(IDataProtectionProvider provider, IAppUser currentUser)
        {
            protector = provider.CreateProtector(Guid.Parse("A634F7A8-3F6D-4109-A1CA-2CE66B1DAE1F").ToString().Substring(0, 8));
            protectorSessionBase = provider.CreateProtector(currentUser.SessionUID.ToString().Substring(0, 8));

            this.currentUser = currentUser;
        }

        public string Protect<T>(T input, bool sessionBase = true)
        {
            if (input == null)
            {
                return null;
            }

            if (input is string)
            {
                if (string.IsNullOrWhiteSpace(input.ToString()))
                {
                    return null;
                }
            }

            string _jsonData = JsonConvert.SerializeObject(input);

            if (sessionBase)
            {
                return protectorSessionBase.Protect(_jsonData);
            }

            return protector.Protect(_jsonData);
        }

        public T Unprotect<T>(string input, bool sessionBase = true)
        {
            try
            {
                string _jsonData = null;

                if (sessionBase)
                {
                    _jsonData = protectorSessionBase.Unprotect(input);
                }
                else
                {
                    _jsonData = protector.Unprotect(input);
                }

                return JsonConvert.DeserializeObject<T>(_jsonData);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
