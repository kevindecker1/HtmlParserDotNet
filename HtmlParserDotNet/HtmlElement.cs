using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HtmlParserDotNet.Extensions;

namespace HtmlParserDotNet
{
    public class HtmlElement
    {
        public string Tag { get; set; }

        public List<HtmlElement> Children { get; set; }

        public List<HtmlAttribute> Attributes { get; set; }
        
        public string InnerHTML { get; set; }

        public HtmlElement()
        {
            this.Children = new List<HtmlElement>();
            this.Attributes = new List<HtmlAttribute>();
        }

        public HtmlElement(XmlNode node)
            : this()
        {
            this.Tag = node.Name;
            this.InnerHTML = node.InnerText;

            FillAttributes(node);
        }

        private void FillAttributes(XmlNode node)
        {
            if (node != null && node.Attributes != null)
            {
                var attributes = node.Attributes.ToList();
                foreach (var attribute in attributes)
                {
                    this.Attributes.Add(new HtmlAttribute()
                    {
                        Name = attribute.Name,
                        Values = ParseAttributeValues(attribute)
                    });
                }
            }
        }

        private List<string> ParseAttributeValues(XmlAttribute xmlAttribute)
        {
            var values = new List<string>();
            if (xmlAttribute != null)
            {
                if (xmlAttribute.Name == "class")
                {
                    values = xmlAttribute.Value.Split(' ').ToList();
                }
                else if (xmlAttribute.Name == "style")
                {
                    values = xmlAttribute.Value.Split(';').ToList();
                }
                else
                {
                    values.Add(xmlAttribute.Value);
                }
            }

            return values;
        }
    }
}
