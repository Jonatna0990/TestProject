using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService.Services;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _user;

        public UserController(UserService _dbHelper)
        {
            this._user = _dbHelper;
        }


        [HttpPost("auth")]
        public ActionResult<string> Auth()
        {
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!dict.ContainsKey("login") || !dict.ContainsKey("pass"))
                return error("wrong params");

            string login = dict["login"];
            string pass = dict["pass"];

            var user = new Users() { login = login, pass = pass };
            var all = _user.get(user);
            if (all != null && all.Count > 0)
            {
                return result(all);
            }
            return error("not found");

        }

        public override ActionResult<string> Get()
        {
            string login = HttpContext.Request.Query["login"];
            string pass = HttpContext.Request.Query["pass"];

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                var all = _user.all();
                if (all == null)
                {
                    return error("not found");
                }
                return result(all);
            }
            else
            {
                var user = new Users() { login = login, pass = pass };
                var all = _user.get(user);
                if (all != null && all.Count > 0)
                {
                    return result(all);
                }
                return error("not found");
            }
           
        }
        
        public override ActionResult<string> Get(int id)
        {
            var user = _user.get(id);
            if (user == null)
            {
                return error("not found");
            }
            return result<Task>(null);
        }
        public override ActionResult<string> Add()
        {
            if (Request.ContentType == null || Request.Form == null) return error("wrong params");
            return UpdateOrAdd(Mode.Add);
        }
        public override ActionResult<string> Delete()
        {
            if (Request.ContentType == null || Request.Form == null) return error("wrong params");

            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!dict.ContainsKey("id"))
                return error("wrong params");
            try
            {
                int id = Int32.Parse(dict["id"]);
                var res = _user.delete(id);
                if (res)
                    return result<Task>(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return error("delete does not executed");
        }
        public override ActionResult<string> Update()
        {
            if (Request.ContentType == null || Request.Form == null) return error("wrong params");
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!dict.ContainsKey("id"))
                return error("wrong params");
            try
            {
                int id = Int32.Parse(dict["id"]);
                return UpdateOrAdd(Mode.Update, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return error("delete does not executed");
       
        }

        private ActionResult<string> UpdateOrAdd(Mode mode, int id = 0)
        {


            if(mode == Mode.Update && id <=0) return error("wrong params");

            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!dict.ContainsKey("login") || !dict.ContainsKey("pass"))
                return error("wrong params");
            
            string login = dict["login"];
            string pass = dict["pass"];
            
            

            var user = new Users() {id_user = id, login = login, pass = pass };
            if (mode == Mode.Add)
                _user.add(user);
            else _user.update(id, user);
            return result(new List<Users>() { user });

        }

        //check_authentication
        [Route("auth")]
        public ActionResult<string> checkAuthentication(string login, string pass)
        {
           // if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
                return error("wrong params");

        }
    }
}
