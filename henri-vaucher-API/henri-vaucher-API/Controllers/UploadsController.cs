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
                                    picture.Title = reader.GetString(0);
                                }
                                catch (Exception)
                                {
                                    picture.Title = null;
                                }
                                try
                                {
                                    var temp = reader.GetValue(1);
                                    if (temp != null)
                                    {
                                        picture.Date = temp.ToString();
                                    }                                   
                                }
                                catch (Exception)
                                {
                                    picture.Date = null;
                                }
                                try
                                {
                                    var temp = reader.GetValue(2);
                                    if (temp != null)
                                    {
                                        picture.Number = int.Parse(temp.ToString());
                                    }                                 
                                }
                                catch (Exception)
                                {
                                    picture.Number = 0;
                                }
                                try
                                {
                                    picture.Signature = reader.GetString(3);
                                }
                                catch (Exception)
                                {
                                    picture.Signature = null;
                                }
                                try
                                {
                                    picture.PositionSignature = reader.GetString(4);
                                }
                                catch (Exception)
                                {
                                    picture.PositionSignature = null;
                                }
                                try
                                {
                                    var temp = reader.GetValue(5);
                                    if (temp != null)
                                    {
                                        picture.Height = float.Parse(temp.ToString());
                                    }

                                }
                                catch (Exception)
                                {
                                    picture.Height = null;
                                }
                                try
                                {
                                    var temp = reader.GetValue(6);
                                    if (temp != null)
                                    {
                                        picture.Width = float.Parse(temp.ToString());
                                    }
                                }
                                catch (Exception)
                                {
                                    picture.Width = null;
                                }
                                try
                                {
                                    var temp = reader.GetValue(7);
                                    if (temp != null)
                                    {
                                        picture.Surface = float.Parse(temp.ToString());
                                    }
                                }
                                catch (Exception)
                                {
                                    picture.Surface = null;
                                }
                                try
                                {
                                    picture.Support = reader.GetString(8);
                                }
                                catch (Exception)
                                {
                                    picture.Support = null;
                                }
                                try
                                {
                                    picture.Drawing = reader.GetString(9);
                                }
                                catch (Exception)
                                {
                                    picture.Drawing = null;
                                }
                                try
                                {
                                    picture.DominantTones = reader.GetString(10);
                                }
                                catch (Exception)
                                {
                                    picture.DominantTones = null;
                                }
                                try
                                {
                                    picture.Owner = reader.GetString(11);
                                }
                                catch (Exception)
                                {
                                    picture.Owner = null;
                                }
                                try
                                {
                                    picture.From = reader.GetString(12);
                                }
                                catch (Exception)
                                {
                                    picture.From = null;
                                }
                                try
                                {
                                    picture.Remarks = reader.GetString(13);
                                }
                                catch (Exception)
                                {
                                    picture.Remarks = null;
                                }
                                try
                                {
                                    picture.File = reader.GetString(14);
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
