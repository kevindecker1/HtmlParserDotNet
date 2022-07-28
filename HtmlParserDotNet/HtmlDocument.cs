using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sgml;
using HtmlParserDotNet.Extensions;

namespace HtmlParserDotNet
{
    public class HtmlDocument
    {
        private string _rawHtml;
        private XmlDocument _xmlDocument;
        private SgmlReader _sgmlReader;

        /// <summary>
        /// Typically the root Html Element of the document
        /// </summary>
        public HtmlElement DocumentElement;

        public HtmlDocument()
        {
            _xmlDocument = new XmlDocument();
            _sgmlReader = new SgmlReader();
        }

        public HtmlDocument(string value, ParsingType parsingType)
            : this()
        {
            switch (parsingType)
            {
                case ParsingType.Url:
                    _rawHtml = GetHtmlFromUrl(value);
                    break;
                case ParsingType.File:
                    _rawHtml = GetHtmlFromFile(value);
                    break;
                case ParsingType.Html:
                    _rawHtml = value;
                    break;
                default:
                    break;
            }

            SetupSgmlReader();
            SetupXmlDocument();
        }

        private void SetupSgmlReader()
        {
            if (_sgmlReader == null)
            {
                _sgmlReader = new SgmlReader();
            }

            _sgmlReader.DocType = "HTML";
            _sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            _sgmlReader.CaseFolding = CaseFolding.ToLower;
            _sgmlReader.InputStream = new System.IO.StringReader(_rawHtml);
        }

        private void SetupXmlDocument()
        {
            if (_xmlDocument == null)
            {
                _xmlDocument = new XmlDocument();
            }

            _xmlDocument.PreserveWhitespace = false;
            _xmlDocument.XmlResolver = null;

            try
            {
                _xmlDocument.Load(_sgmlReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetHtmlFromFile(string filepath)
        {
            try
            {
                if (ValidateFilePath(filepath))
                {
                    System.IO.TextReader reader = new System.IO.StreamReader(filepath);
                    return reader.ReadToEnd();
                }

                return String.Empty;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        private string GetHtmlFromUrl(string url)
        {
            try
            {
                if (ValidateUrl(url))
                {
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        return client.GetStringAsync(url).Result;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private bool ValidateUrl(string url)
        {
            if (url.HasValue())
            {
                Uri uriResult;
                return Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            return false;
        }

        private bool ValidateFilePath(string filepath)
        {
            if (filepath.HasValue())
            {
                return System.IO.File.Exists(filepath);
            }

            return false;
        }

        internal void Parse()
        {
            if (_xmlDocument != null)
            {
                ParseXmlDocument(_xmlDocument.DocumentElement);
            }
        }

        private void ParseXmlDocument(XmlNode htmlNode)
        {
            ParseNodes(htmlNode);
        }

        private void ParseNodes(XmlNode node)
        {
            var element = node.ToElement();

            if (this.DocumentElement == null)
            {
                this.DocumentElement = element;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                var childElement = ParseNode(childNode);
                element.Children.Add(childElement);
            }
        }

        private HtmlElement ParseNode(XmlNode node)
        {
            var element = node.ToElement();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                var childElement = ParseNode(childNode);
                element.Children.Add(childElement);
            }
            return element;
        }
    }
}
