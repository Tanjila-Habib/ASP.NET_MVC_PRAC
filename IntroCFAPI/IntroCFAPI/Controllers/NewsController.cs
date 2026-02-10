using AutoMapper;
using IntroCFAPI.DTOs;
using IntroCFAPI.EF.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroCFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        readonly NC_DbContext db;
        public NewsController(NC_DbContext db)
        {
            this.db = db;
        }
        [HttpPost("Create")]
        public IActionResult Create(NewsDTO n)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<News, NewsDTO>().ReverseMap();
            }
                );
            var mapper = new Mapper(config);
            db.News.Add(mapper.Map<News>(n));
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = db.News.Find(id);
            return Ok(data); 
        }
    }
}

