using System;
using HtmlParserDotNet;

namespace HtmlParserDotNetTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load Html File
            var fileDoc = HtmlParserDotNet.HtmlParser.LoadFromFile("index.html");

            // Load URL
            var urlDoc = HtmlParserDotNet.HtmlParser.LoadFromUrl("https://www.w3schools.com/");

            // Load Html string
            var htmlDoc = HtmlParserDotNet.HtmlParser.LoadFromHtmlString(@"
                <!DOCTYPE html>
                <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta charset='utf-8' />
                    <title></title>
                </head>
                <body>
                    <div>
                        <div id='test1' class='c c1' style='width:auto;background-color:white;'>
                            <input id='txtI' type='hidden' value='test' />
                        </div>
                        <div id='test2' class='c c1'>
                            <span>Test inner HTML!</span>
                        </div>
                    </div>
                </body>
                </html>");



            // Various extension methods are available 

            // Get an element by ID
            var idElement = fileDoc.DocumentElement.GetElementById("test1");

            // Get a list of elements by class name
            var classes = fileDoc.DocumentElement.GetElementsByClassName("c1");

            // Get a list of elements by tag name
            var tags = fileDoc.DocumentElement.GetElementsByTagName("input");

            // ... and more!
        }
    }
}