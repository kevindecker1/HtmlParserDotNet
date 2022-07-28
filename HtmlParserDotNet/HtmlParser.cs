using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParserDotNet
{
    public static class HtmlParser
    {
        internal static HtmlDocument Load(string value, ParsingType parsingType)
        {
            if (!value.HasValue())
            {
                return null;
            }

            var document = new HtmlDocument(value, parsingType);
            document.Parse();

            return document;
        }

        /// <summary>
        /// Load an HtmlDocument from an html string
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlDocument LoadFromHtmlString(string html)
        {
            return Load(html, ParsingType.Html);
        }

        /// <summary>
        /// Load an HtmlDocument from a path to a file
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static HtmlDocument LoadFromFile(string filepath)
        {
            return Load(filepath, ParsingType.File);
        }

        /// <summary>
        /// Load an HtmlDocument from an URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HtmlDocument LoadFromUrl(string url)
        {
            return Load(url, ParsingType.Url);
        }
    }
}
