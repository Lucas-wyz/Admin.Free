using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Free.Controllers
{
    /// <summary>
    /// 试卷
    /// </summary>
    /// <param name="dbc"></param>
    /// <param name="logger"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController(AppDbContext dbc, ILogger<ExamsController> logger) : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultObjet<List<Exams>> Get([FromQuery] QueryParameters queryParameters)
        {
            var list = dbc.Exams.Page(queryParameters.Page, queryParameters.Size).ToList();

            return this.OKResult<List<Exams>>(list);

        }


        [HttpDelete("{id}")]
        public ResultObjet<bool> Del([FromRoute] string id)
        {

            var query = dbc.Exams.Where(x => x.ID == id).ToList();

            foreach (var item in query)
            {
                item.IsDeleted = true;
            }
            dbc.SaveChanges();

            return this.OKResult<bool>(true);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="exams"></param>
        /// <returns></returns>
        [HttpPost()]
        public ResultObjet<bool> Post([FromBody] Exams exams)
        {
            exams.ID = Guid.NewGuid().ToString("n");

            dbc.Add(exams);
            dbc.SaveChanges();

            return this.OKResult<bool>(true);
        }

    }
}
