using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Free.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousUsersController(AppDbContext dbc, ILogger<LoginController> logger) : ControllerBase
    {
        public MapperConfiguration _configMap => new MapperConfiguration(cfg =>
            {
            });



        [HttpGet]
        public ResultObjet<List<AnonymousUsers>> Get([FromQuery] QueryParameters queryParameters)
        {
            var list = dbc.AnonymousUsers.Page(queryParameters.Page, queryParameters.Size).ToList();
            return this.OKResult(list);
        }


        [HttpDelete("{ID}")]
        public ResultObjet Del([FromRoute] string ID)
        {
            var query = dbc.AnonymousUsers.Where(x => x.ID == ID).ToList();

            foreach (var item in query)
            {
                item.IsDeleted = true;
            }


            dbc.SaveChanges();
            return this.OKResult();
        }



    }
}
