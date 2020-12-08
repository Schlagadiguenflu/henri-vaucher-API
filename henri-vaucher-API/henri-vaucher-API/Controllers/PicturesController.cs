using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using henri_vaucher_API.Models;
using henri_vaucher_API.Utility;
using henri_vaucher_API.Utility.PaginationParameters;
using System.Text.Json;

namespace henri_vaucher_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly HenriVaucherContext _context;

        public PicturesController(HenriVaucherContext context)
        {
            _context = context;
        }

        // GET: api/Pictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures([FromQuery]PictureParameters pictureParameters)
        {
            Pagination<Picture> pictures = await Pagination<Picture>
                .ToPagedListAsync(_context.Pictures.AsNoTracking().OrderBy(p => p.PictureId),
                pictureParameters.PageNumber,
                pictureParameters.PageSize);

            var metadata = new
            {
                pictures.TotalCount,
                pictures.PageSize,
                pictures.CurrentPage,
                pictures.TotalPages,
                pictures.HasNext,
                pictures.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return pictures;
        }


        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _context.Pictures.AsNoTracking().Where(p => p.PictureId == id).SingleOrDefaultAsync();

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        // PUT: api/Pictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.PictureId)
            {
                return BadRequest();
            }

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.PictureId }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PictureExists(int id)
        {
            return _context.Pictures.Any(e => e.PictureId == id);
        }

        /// <summary>
        /// Get the next picture from current one
        /// </summary>
        // GET: api/Pictures/next/5
        [HttpGet("next/{id}")]
        public async Task<ActionResult<Picture>> GetPictureNext(int id)
        {
            id++;
            int lastPictureId = await _context.Pictures
                .AsNoTracking()
                .OrderBy(p => p.PictureId)
                .Select(p => p.PictureId)
                .LastOrDefaultAsync();

            Picture picture = null;
            bool etat = true;
            while (lastPictureId >= id && etat)
            {
                picture = await _context.Pictures
                    .Where(p => p.PictureId == id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
                if (picture == null)
                {
                    id++;
                }
                else
                {
                    etat = false;
                }
            }
            if (lastPictureId < id)
            {
                picture = await _context.Pictures
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }

            return picture;
        }

        /// <summary>
        /// Get the previous picture from current one
        /// </summary>
        // GET: api/Pictures/previous/5
        [HttpGet("previous/{id}")]
        public async Task<ActionResult<Picture>> GetPicturePrevious(int id)
        {
            id--;

            Picture picture = null;
            bool etat = true;
            while (id >= 1 && etat)
            {
                picture = await _context.Pictures
                    .Where(p => p.PictureId == id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
                if (picture == null)
                {
                    id--;
                }
                else
                {
                    etat = false;
                }
            }
            if (id < 1)
            {
                picture = await _context.Pictures
                    .AsNoTracking()
                    .OrderBy(p => p.PictureId)
                    .LastOrDefaultAsync();
            }

            return picture;
        }
    }
}
