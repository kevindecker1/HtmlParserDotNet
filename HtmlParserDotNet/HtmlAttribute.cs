using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParserDotNet
{
    public class HtmlAttribute
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }

        public HtmlAttribute()
        {
            this.Values = new List<string>();
        }
    }
}
