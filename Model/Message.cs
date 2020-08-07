using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Message<T>
    {
        public string status { get; set; }

        [DefaultValue("")]
        public string reason { get; set; }

        [DefaultValue(null)]
        public IList<T> data { get; set; }
    }
}
