# HtmlParserDotNet

HtmlParserDotNet is a .NET Framework library for parsing and querying HTML elements. It is lightweight, fast and easy to use!

The purpose of this library is to provide simplicity in parsing HTML. I know there's already a few really good and robust solutions out there (HtmlAgilityPack, AngleSharp to name a couple), but I found that these required more setup to get going, and at times a little slow. The biggest difference between HtmlParserDotNet and the aforementioned, is that HtmlParserDotNet is strictly readonly. At this time there is no functionality to manipulate the DOM or create elements. HtmlParserDotNet hopes to separate itself through performance and simplicity.

# Under the Hood

HtmlParserDotNet is using the built-in .NET System.Xml namespace. As you can guess, the HTML gets loaded into a XmlDocument, which from there, gets parsed into more readable and manageable classes. For any given element, the tag name, inner html, attributes (styles, classes, etc.), and child elements are all accessible. I found that generating more complex Html from URLs would fail loading into an XmlDocument, so the use of the SgmlReader library came into great use. This essentially converts most HTML to valid XML.

# Usage

HtmlParserDotNet provides 3 easy methods to parse HTML, by providing either a file, an URL, or html as a string.

```
using HtmlParserDotNet;
```

```
// Load from URL
var document = HtmlParserDotNet.HtmlParser.LoadFromUrl("https://www.w3schools.com/");

// Load from File
var document = HtmlParserDotNet.HtmlParser.LoadFromFile("index.html");

// Load from Html string
var document = HtmlParserDotNet.HtmlParser.LoadFromHtmlString(@"<!DOCTYPE html>
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
```

Helpful Extension Methods

The DocumentElement property is typically the root html element. From here and from any element these extensions are available.

```
// Get an element by ID
var idElement = document.DocumentElement.GetElementById("test1");

// Get a list of elements by class name
var elements = document.DocumentElement.GetElementsByClassName("c1");

// Get a list of elements by tag name
var elements = document.DocumentElement.GetElementsByTagName("input");

```
