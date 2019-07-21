using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace Meetup.UnitTestExample.Test.Attributes
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly string _propertyName;

        public JsonFileDataAttribute(string filePath)
            : this(filePath, null) { }

        public JsonFileDataAttribute(string filePath, string propertyName)
        {
            _filePath = filePath;
            _propertyName = propertyName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            string path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            string fileData = File.ReadAllText(_filePath);

            if (string.IsNullOrEmpty(_propertyName))
            {
                //whole file is the data
                return JsonConvert.DeserializeObject<List<object[]>>(fileData);
            }

            // Only use the specified property as the data
            JObject allData = JObject.Parse(fileData);
            JToken data = allData[_propertyName];
            return data.ToObject<List<object[]>>();
        }
    }
}
