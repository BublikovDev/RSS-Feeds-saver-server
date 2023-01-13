using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.Requests;

namespace TestTask.Controllers
{
    public class RssController : ControllerBase
    {
        private readonly AppDbContext _dataContext;

        public RssController(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }


        //1. Add RSS feed (parameters: feed url)
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddNew([FromBody] AddRssRequest request)
        {
            try
            {
                await _dataContext.Rss.AddAsync(new RSS() { FeedUrl = request.FeedUrl, Date = DateTime.UtcNow, IsActive = true, IsRead = false });
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //2. Get all active RSS feeds
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Rss = from rss in _dataContext.Rss
                          where rss.IsActive == true
                          select rss;
                return Ok(Rss);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //3. Get all unread news from some date (parameters: date)
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUnreadByDate(GetAllUnreadByDateRequest request)
        {
            try
            {
                List<RSS> result = new List<RSS>();
                foreach(var rss in _dataContext.Rss)
                {
                    if((rss.Date - request.Date).TotalMilliseconds > 0 && rss.IsRead == false)
                        result.Add(rss);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //4. Set news as read
        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> SetNewsAsRead([FromBody] SetNewsAsReadRequest request)
        {
            try
            {
                RSS? rss = await _dataContext.Rss.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (rss != null)
                    rss.IsRead = request.IsRead;
                else
                    throw new Exception("Id is incorrect!");

                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}