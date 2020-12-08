using ExcelDataReader;
using henri_vaucher_API.Models;
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
        private readonly HenriVaucherContext _context;

        public UploadsController(HenriVaucherContext context)
        {
            _context = context;
        }

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
                                Picture picture = new Picture();
                                try
                                {
                                    picture.Title = reader.GetValue(0).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Title = null;
                                }
                                try
                                {
                                    picture.Date = reader.GetValue(1).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Date = null;
                                }
                                try
                                {
                                    picture.Number = int.Parse(reader.GetValue(2).ToString());
                                }
                                catch (Exception)
                                {
                                    picture.Number = 0;
                                }
                                try
                                {
                                    picture.Signature = reader.GetValue(3).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Signature = null;
                                }
                                try
                                {
                                    picture.PositionSignature = reader.GetValue(4).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.PositionSignature = null;
                                }
                                try
                                {
                                    picture.Height = float.Parse(reader.GetValue(5).ToString());
                                }
                                catch (Exception)
                                {
                                    picture.Height = null;
                                }
                                try
                                {
                                    picture.Width = float.Parse(reader.GetValue(6).ToString());
                                }
                                catch (Exception)
                                {
                                    picture.Width = null;
                                }
                                try
                                {
                                    picture.Surface = float.Parse(reader.GetValue(7).ToString());
                                }
                                catch (Exception)
                                {
                                    picture.Surface = null;
                                }
                                try
                                {
                                    picture.Support = reader.GetValue(8).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Support = null;
                                }
                                try
                                {
                                    picture.Drawing = reader.GetValue(9).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Drawing = null;
                                }
                                try
                                {
                                    picture.DominantTones = reader.GetValue(10).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.DominantTones = null;
                                }
                                try
                                {
                                    picture.Owner = reader.GetValue(11).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Owner = null;
                                }
                                try
                                {
                                    picture.From = reader.GetValue(12).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.From = null;
                                }
                                try
                                {
                                    picture.Remarks = reader.GetValue(13).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.Remarks = null;
                                }
                                try
                                {
                                    picture.File = reader.GetValue(14).ToString();
                                }
                                catch (Exception)
                                {
                                    picture.File = null;
                                }
                                _context.Pictures.Add(picture);
                                await _context.SaveChangesAsync();
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
