using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idNumberValidatorApi.Models
{
    public class IdModel
    {
        public string IdentityNumber { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public bool IsValid { get; set; }
        public bool IsBirthDateValid { get; set; }
        public bool IsGenderValid { get; set; }
        public bool IsCitizenshipValid { get; set; }
        public string FailureReason { get; set; }

    }
}
