using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface ICompany : IBase<int>, ICommon
    {
        IAddress CompAddress { get; set; }
        int Id { get; set; }
        string CompName { get; set; }
        string CompPhone { get; set; }
        string RegMobile { get; set; }
        string ContactPerson { get; set; }
        string EmailID { get; set; }
        int fk_CountryID { get; set; }
        int? fk_StateID { get; set; }
        string StateName { get; set; }
        bool IsActive { get; set; }
        string Country { get; set; }
        string ProductInterest { get; set; }
        string CompType { get; set; }
    }
}
