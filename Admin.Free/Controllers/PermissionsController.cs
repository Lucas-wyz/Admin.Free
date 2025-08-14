using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Admin.Free.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbc"></param>
    /// <param name="logger"></param>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PermissionsController(AppDbContext dbc, ILogger<PermissionsController> logger) : ControllerBase
    {
        public MapperConfiguration _configMap => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Permissions, PermissionNodeView>();
        });


        [HttpGet]
        public ResultObjet<List<Permissions>> Get([FromQuery] QueryParameters queryParameters)
        {
            var list = dbc.Permissions.Page(queryParameters.Page, queryParameters.Size).ToList();
            return this.OKResult(list);
        }


        [HttpPost]
        public ResultObjet Post([FromBody] Permissions roles)
        {

            roles.ID = Guid.NewGuid().ToString("n");
            roles.IsDeleted = false;

            var query = dbc.Permissions.Add(roles);
            dbc.SaveChanges();
            return this.OKResult();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResultObjet Put([FromRoute] string id, [FromBody] Roles roles)
        {
            var query = dbc.Roles.Find(id);

            query.Name = roles.Name;
            query.Permissions = roles.Permissions;
            query.Description = roles.Description;

            dbc.SaveChanges();
            return this.OKResult();
        }



        [HttpDelete("{ID}")]
        public ResultObjet Del([FromRoute] string ID)
        {
            var query = dbc.Permissions.Where(x => x.ID == ID).ToList();

            foreach (var item in query)
            {
                item.IsDeleted = true;
            }



            dbc.SaveChanges();
            return this.OKResult();
        }


        [HttpGet("GetTree")]
        public ResultObjet<List<PermissionNodeView>> GetTree()
        {
            var query = dbc.Permissions.ToList();
            var list = GetPermissionTree(query);
            return this.OKResult(list);
        }

        private List<PermissionNodeView> GetPermissionTree(List<Permissions> list, string pid = "0")
        {
            if (list.Count > 0)
            {
                if (pid != null)
                {
                    var viewList = list.Where(x => x.Pid == pid)
                        .Select(x =>
                        {
                            var view = _configMap.CreateMapper().Map<PermissionNodeView>(x);
                            view.Children = GetPermissionTree(list, x.ID);
                            return view;
                        }).ToList();
                    return viewList;
                }
            }
            return new List<PermissionNodeView>();
        }


    }





}
