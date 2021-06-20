using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Sudoku_WebService.Formatters
{
    public static class ContentTypeTransformer
    {
        public enum UnifiedContentType { Html, Json, PlainText, Xml}

        public static UnifiedContentType UnifyContentType(string contentType)
        {
            switch(contentType)
            {
                case "text/html":
                    throw new ArgumentException($"Invalid value: {contentType}", "Content-Type");
                case "application/json":
                    return UnifiedContentType.Json;
                case "text/plain":
                    throw new ArgumentException($"Invalid value: {contentType}", "Content-Type");
                case "application/xml":
                    throw new ArgumentException($"Invalid value: {contentType}", "Content-Type");
                default:
                    throw new ArgumentException($"Invalid value: {contentType}", "Content-Type");
            }
        }
        public static Stream FormatContent(UnifiedContentType contentType, object content)
        {
            switch (contentType)
            {
                case UnifiedContentType.Html:
                    throw new NotImplementedException();
                case UnifiedContentType.Json:
                    var jsonString = JsonConvert.SerializeObject(content);
                    return new MemoryStream(Encoding.Default.GetBytes(jsonString));
                case UnifiedContentType.PlainText:
                    throw new NotImplementedException();
                case UnifiedContentType.Xml:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();

            }
        }
        public static T CreateObjectFromContent<T>(UnifiedContentType contentType, string content)
        {
            switch (contentType)
            {
                case UnifiedContentType.Html:
                    throw new NotImplementedException();
                case UnifiedContentType.Json:
                    var FormattedObject = JsonConvert.DeserializeObject<T>(content);
                    return FormattedObject;
                case UnifiedContentType.PlainText:
                    throw new NotImplementedException();
                case UnifiedContentType.Xml:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
