# HtmlParserDotNet

HtmlParserDotNet is a .NET Framework library for parsing and querying HTML elements. It is lightweight, fast and easy to use!

The purpose of this library is to provide simplicity in parsing HTML. I know there's already a few really good and robust solutions out there (HtmlAgilityPack, AngleSharp to name a couple), but I found that these required more setup to get going. The biggest difference between HtmlParserDotNet and the aforementioned, is that HtmlParserDotNet is strictly readonly. At this time there is not functionality to manipulate the DOM or create elements. HtmlParserDotNet hopes to make up for that in performance and simplicity.

# Usage

HtmlParserDotNet provides 3 easy methods to parse HTML, by providing a file, an URL, or html as a string.

```
using HtmlParserDotNet;
```

```
// Load from URL
var document = HtmlParserDotNet.HtmlParser.LoadFromUrl("https://www.w3schools.com/");

// Load from File
var document = HtmlParserDotNet.HtmlParser.LoadFromFile("index.html");

// Load from Html string
var document = HtmlParserDotNet.HtmlParser.LoadFromHtmlString(@"");
```
