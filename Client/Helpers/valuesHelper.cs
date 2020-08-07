using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class ValuesHelper
    {

        
        public static string API_URL = "http://localhost:64425/api";

        public static string API_AUTH = "auth";

        public static string API_NOMENCLATURE = "nomenclature";
        public static string API_USER = "user";


        public static string API_UPDATE = "update";
        public static string API_ADD = "add";
        public static string API_DELETE = "delete";

        //Примеры запросов

        //http://localhost:64425/api/nomenclature/update?id=1&price=234&name=234523&from_date=2009-05-08 14:40&to_date=2009-05-08 14:40
        //http://localhost:64425/api/nomenclature/add?price=123&name=234523&from_date=2009-05-08 14:40&to_date=2009-05-08 14:40
        //http://localhost:64425/api/nomenclature/1
        //http://localhost:64425/api/nomenclature/delete?id=1

        //http://localhost:64425/api/user/delete?id=10
        //http://localhost:64425/api/user/add?login=45634563456&pass=hjhdfgdfghdfghdfg
        //http://localhost:64425/api/user?login=45634563456&pass=hjhdfgdfghdfghdfg

    }
}
