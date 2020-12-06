using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace henri_vaucher_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        [Route("ImportPictures")]
        [HttpPost]
        public async Task<ActionResult> PostImportAccounts(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        // 1. Use the reader methods
                        // Skip header
                        reader.Read();
                        do
                        {
                            while (reader.Read())
                            {
                                
                                var test = reader.GetValue(0).ToString();
                            }
                        } while (reader.NextResult());

                    }
                }
                return Created("api/Accounts/ImportAccounts", "test");
            }
            else
            {
                return Created("api/Accounts/ImportAccounts", "No file");
            }
        }
    }
}
