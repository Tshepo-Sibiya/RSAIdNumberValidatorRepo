using CsvHelper;
using idNumberValidatorApi.Models;
using idNumberValidatorApi.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace idNumberValidatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateIdController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        public List<IdModel>  Post([FromBody] List<IdModel> data)
        {
            try {
                List<IdModel> Passed = new List<IdModel>();
                List<IdModel> Failed = new List<IdModel>();
                foreach (var item in data)
                {
                    var ValidateService = new ValidationService(item.IdentityNumber);
                    ValidateService.GetResult();

                    if (ValidateService.IsValidLength && ValidateService.IsValidBirthDate && ValidateService.IsValidGender && ValidateService.IsValidCitizenship)
                    {


                        Passed.Add(new IdModel
                        {
                            BirthDate = ValidateService.BirthDate,
                            Citizenship = ValidateService.Citizenship,
                            Gender = ValidateService.Gender,
                            IdentityNumber = ValidateService.IdToValidate
                        });
                    }
                    else
                    {
                        string reason = "";
                        if (!ValidateService.IsValidLength)
                        {
                            reason += " Invalid length" + (ValidateService.IsValidBirthDate ? " - " : " ");
                        }

                        if (!ValidateService.IsValidBirthDate)
                        {
                            reason += "Invalid Birth Date" + (!ValidateService.IsValidGender ? " - " : " ");
                        }

                        if (!ValidateService.IsValidGender)
                        {
                            reason += "Invalid Gender " + (!ValidateService.IsValidCitizenship ? " - " : " ");
                        }

                        if (!ValidateService.IsValidCitizenship)
                        {
                            reason += " Invalid Citizenship";
                        }
                        Failed.Add(new IdModel
                        {
                            IdentityNumber = ValidateService.IdToValidate,
                            FailureReason = reason
                        });
                    }

                }

                Directory.CreateDirectory(@"C:\ProcessedIds");
                string failedIds = @"C:\ProcessedIds\Failed" + DateTime.Now.ToString("-dd-MM-yyyy-hh-mm-ss") + ".csv";
                string passedIds = @"C:\ProcessedIds\Passed" + DateTime.Now.ToString("-dd-MM-yyyy-hh-mm-ss") + ".csv";


                using (TextWriter writer = new StreamWriter(failedIds, false, System.Text.Encoding.UTF8))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecords(Failed);
                }

                using (TextWriter writer = new StreamWriter(passedIds, false, System.Text.Encoding.UTF8))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecords(Passed);
                }

                var result = Passed.Concat(Failed).ToList();
                return result;
            }
            catch (Exception ex) {
                var message = ex;
            }
            return null;
            

        }

    }
}