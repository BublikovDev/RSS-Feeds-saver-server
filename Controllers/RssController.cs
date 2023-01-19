using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Serialization;
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
                var response = new WebClient().DownloadString(request.FeedUrl);

                XmlSerializer serializer = new(typeof(Rss));
                using (StringReader reader = new(response))
                {
                    var deserializedRss = (Rss)serializer.Deserialize(reader);
                    deserializedRss.Channel.LastBuildDate = deserializedRss.Channel.LastBuildDateString != null ? Convert.ToDateTime(deserializedRss.Channel.LastBuildDateString) : DateTime.UtcNow;

                    deserializedRss.Channel.PubDate= deserializedRss.Channel.PubDateString !=null ? Convert.ToDateTime(deserializedRss.Channel.PubDateString): DateTime.UtcNow;
                    foreach(var item in deserializedRss.Channel.Items)
                    {
                        item.PubDate= item.PubDateString!=null ? Convert.ToDateTime(item.PubDateString) : DateTime.UtcNow;
                    }

                    await _dataContext.Rsses.AddAsync(deserializedRss);
                    await _dataContext.SaveChangesAsync();
                    return Ok();
                }
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
                var rsses = _dataContext.Rsses.Include(r => r.Channel).ToList();
                return Ok(rsses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //3. Get all unread news from some date(parameters: date)
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUnreadByDate(GetAllUnreadByDateRequest request)
        {
            try
            {
                List<Item> result = new List<Item>();
                foreach (var news in _dataContext.Items)
                {
                    if ((news.PubDate - request.Date).TotalMilliseconds > 0 && news.IsRead == false)
                        result.Add(news);
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
                Item? item = await _dataContext.Items.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (item != null)
                    item.IsRead = request.IsRead;
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