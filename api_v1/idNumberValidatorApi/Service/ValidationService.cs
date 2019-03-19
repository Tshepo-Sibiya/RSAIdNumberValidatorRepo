using idNumberValidatorApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace idNumberValidatorApi.Service
{
    public class ValidationService
    {
        public ValidationService(string idToValidate)
        {
            IdToValidate = idToValidate;
        }
        public string IdToValidate { get; set; }

        public bool IsValidLength { get; set; }
        public bool IsValidBirthDate { get; set; }
        public bool IsValidGender { get; set; }
        public bool IsValidCitizenship { get; set; }

        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string IdentityNumber { get; set; }

        public void ValidateLength()
        {
            int stringLength = IdToValidate.Length;
            if (stringLength != 13)
            {
                IsValidLength = false;
            }
            else
            {
                IsValidLength = true;
            }
        }

        public void GetValidateCitizenship()
        {
            string idStr = IdToValidate.Substring(10, 1);
            if (idStr == "1")
            {
                Citizenship = "Foreign";
                IsValidCitizenship = true;
            }
            else if (idStr == "0")
            {
                Citizenship = "South African";
                IsValidCitizenship = true;
            }
            else
            {
                Citizenship = "Invalid";
                IsValidCitizenship = false;
            }
        }

       

        public void GetValidateBirthDate()
        {
            var idStr = IdToValidate.Substring(0, 6);
            DateTime dt;
            if (DateTime.TryParseExact(idStr, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                BirthDate = dt.ToString("dd MMMM yyy");
                IsValidBirthDate = true;
            }
            else
            {
                IsValidBirthDate = false;
            }

        }

        public void GetValidateGender()
        {
            var idStr = IdToValidate.Substring(6, 4);
            if (Convert.ToInt32(idStr) >= 5000 && Convert.ToInt32(idStr) <= 9999)
            {
                Gender = "Male";
                IsValidGender = true;
            }
            else if (Convert.ToInt32(idStr) < 5000)
            {
                Gender = "Female";
                IsValidGender = true;
            }
            else {
                IsValidGender = false;
            }
        }


        public void GetResult()
        {
            ValidateLength();
            if (IsValidLength) {
                GetValidateCitizenship();
                GetValidateGender();
                GetValidateBirthDate();
            }
            
            
        }
    }
}
