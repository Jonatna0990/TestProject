using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using APIService.Services;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureController : BaseController
    {
        private readonly NomenclatureService _nomenclature;

        public NomenclatureController(NomenclatureService _dbHelper)
        {
            this._nomenclature = _dbHelper;
        }

        public override ActionResult<string> Get()
        {
            var all = _nomenclature.all();
            if (all != null && all.Count > 0)
            {
                return result(all);
            }
            return error("not found");
        }

        public override ActionResult<string> Get(int id)
        {
            var nomenclature = _nomenclature.get(id);
            if (nomenclature == null)
            {
                return error("not found");
            }
            return result(new List<Nomenclature>(){ nomenclature });
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
                var res = _nomenclature.delete(id);
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
            if(Request.ContentType == null || Request.Form == null) return error("wrong params");
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
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!dict.ContainsKey("name") || !dict.ContainsKey("price") || !dict.ContainsKey("from_date") || !dict.ContainsKey("to_date"))
                return error("wrong params");

            string name = dict["name"];
            string price = dict["price"];
            string from_date = dict["from_date"];
            string to_date = dict["to_date"];

       

            try
            {
                var _price = decimal.Parse(price, CultureInfo.InvariantCulture.NumberFormat);
                var _from_date = DateTime.ParseExact(from_date, "yyyy-MM-dd HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                var _to_date = DateTime.ParseExact(to_date, "yyyy-MM-dd HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                var nomenclature = new Nomenclature() { id_nomenclature = id, name = name, price = _price, fromDate = _from_date, toDate = _to_date };
                if (mode == Mode.Add) _nomenclature.add(nomenclature);
                    else _nomenclature.update(id, nomenclature); 
                return result(new List<Nomenclature>() { nomenclature });
            }
            catch (Exception e)
            {
                return error("wrong params");
            }


        }
    }
}
