using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sudoku_WebService.Tests.TestInput
{
    public static class JsonToJObject
    {
        public static JObject GetSample(string SampleName)
        {
            var JsonSample = JObject.Parse(File.ReadAllText($"../../../TestInput/{SampleName}.json"));
            return JsonSample;
        }
    }
}
