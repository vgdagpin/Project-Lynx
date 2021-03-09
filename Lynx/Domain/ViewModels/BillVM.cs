using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class BillVM : IMapFrom<Bill>
    {
        public short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }


        public bool IsEnabled { get; set; }

        public string AssemblyName { get; set; }
        public string TypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Bill), GetType());
        }
    }
}
