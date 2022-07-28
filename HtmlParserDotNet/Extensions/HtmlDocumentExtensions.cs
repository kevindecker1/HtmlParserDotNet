using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParserDotNet
{
    public static class HtmlDocumentExtensions
    {
        /// <summary>
        /// Gets the HtmlElement who's ID matches the specified id
        /// </summary>
        /// <param name="element"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HtmlElement GetElementById(this HtmlElement element, string id)
        {
            if (element == null) return null;

            var allElements = element.Children.Flatten(x => x.Children);

            return allElements.FirstOrDefault(x => x.Attributes.Any(a => a.Name == "id" && a.Values.Contains(id)));
        }


        /// <summary>
        /// Gets all elements that contain the specified class name
        /// </summary>
        /// <param name="element"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static List<HtmlElement> GetElementsByClassName(this HtmlElement element, string className)
        {
            var list = new List<HtmlElement>();

            if (element == null) return list;
            if (!className.HasValue()) return list;

            var classes = element.EnumerateByClass(className.SafeTrim());
            list.AddRange(classes);
            return list;
        }

        /// <summary>
        /// Get all elements that match the specified tag name
        /// </summary>
        /// <param name="element"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<HtmlElement> GetElementsByTagName(this HtmlElement element, string tag)
        {
            var list = new List<HtmlElement>();

            if (element == null) return list;
            if (!tag.HasValue()) return list;

            var tags = element.EnumerateByTag(tag.SafeTrim());
            list.AddRange(tags);
            return list;
        }

        public static bool HasAttribute(this HtmlElement element, string attributeName)
        {
            if (element == null) return false;
            if (!attributeName.HasValue()) return false;

            return element.Attributes.Any(x => x.Name == attributeName);
        }

        /// <summary>
        /// Gets whether a given element has the specified class
        /// </summary>
        /// <param name="element"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static bool HasClass(this HtmlElement element, string className)
        {
            if (element == null) return false;
            if (!className.HasValue()) return false;

            var classAttribute = element.Attributes.FirstOrDefault(x => x.Name == "class");
            if (classAttribute != null)
            {
                return classAttribute.Values.Contains(className);
            }

            return false;
        }

        private static List<HtmlElement> EnumerateByClass(this HtmlElement element, string className)
        {
            return element.Children
                .Where(x => x.Attributes.Any(a => a.Values.Contains(className)))
                .Union(element.Children.SelectMany(e => EnumerateByClass(e, className))).ToList();
        }

        private static List<HtmlElement> EnumerateByTag(this HtmlElement element, string tag)
        {
            return element.Children
                .Where(x => x.Tag == tag)
                .Union(element.Children.SelectMany(e => EnumerateByTag(e, tag))).ToList();
        }

        private static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion)
             where R : IEnumerable<T>
        {
            var flattened = source.ToList();
            var children = source.Select(recursion);
            if (children != null)
            {
                foreach (var child in children)
                {
                    flattened.AddRange(child.Flatten(recursion));
                }
            }

            return flattened;
        }

        /// <summary>
        /// Get the specified attribute's value for a given element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static List<string> GetAttributeValue(this HtmlElement element, string attributeName)
        {
            if (element == null) return new List<string>();
            if (!attributeName.HasValue()) return new List<string>();

            var attribute = element.Attributes.FirstOrDefault(x => x.Name == attributeName);
            if (attribute != null)
            {
                return attribute.Values;
            }

            return new List<string>();
        }

        /// <summary>
        /// Get all attributes for a given element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<HtmlAttribute> GetAttributes(this HtmlElement element)
        {
            if (element == null) return new List<HtmlAttribute>();
            return element.Attributes;
        }

        /// <summary>
        /// Get all CSS classes for a given element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<string> GetClasses(this HtmlElement element)
        {
            var classes = new List<string>();
            if (element == null) return classes;

            var classAttribute = element.Attributes.FirstOrDefault(x => x.Name == "class");
            if (classAttribute != null)
            {
                classes = classAttribute.Values;
            }

            return classes;
        }


    }
}
