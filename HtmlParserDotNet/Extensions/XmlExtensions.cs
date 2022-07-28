using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HtmlParserDotNet.Extensions
{
    internal static class XmlExtensions
    {
        static XmlNode FirstOrDefault(this XmlNodeList nodeList)
        {
            if(nodeList == null) return null;
            return nodeList.ToList().FirstOrDefault();
        }

        static XmlNode FirstOrDefault(this XmlNodeList nodeList, Func<XmlNode, bool> expr)
        {
            if (nodeList == null) return null;
            return nodeList.ToList().FirstOrDefault(expr);
        }

        static IEnumerable<XmlNode> Where(this XmlNodeList nodeList, Func<XmlNode, bool> expr)
        {
            if (nodeList == null) return null;
            return nodeList.ToList().Where(expr);
        }

        static List<XmlNode> ToList(this XmlNodeList nodeList)
        {
            if (nodeList == null) return null;

            var list = new List<XmlNode>();
            foreach (XmlNode node in nodeList)
            {
                list.Add(node);
            }
            return list;
        }

        internal static List<XmlAttribute> ToList(this XmlAttributeCollection attributeCollection)
        {
            if (attributeCollection == null) return null;

            var list = new List<XmlAttribute>();
            foreach (XmlAttribute attribute in attributeCollection)
            {
                list.Add(attribute);
            }
            return list;
        }

        internal static HtmlElement ToElement(this XmlNode node)
        {
            if (node == null) return null;

            return new HtmlElement(node);
        }
    }
}
