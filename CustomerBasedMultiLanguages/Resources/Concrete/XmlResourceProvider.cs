using Resources.Abstract;
using Resources.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Resources.Concrete
{
    public class XmlResourceProvider : BaseResourceProvider
    {
        // File path
        private static string _filePath = null;
        private static string _filePathFallback = null;

        private static readonly IDictionary<string, IDictionary<string, ResourceEntry>> ResourcesFallback = new Dictionary<string, IDictionary<string, ResourceEntry>>();

        public XmlResourceProvider(string filePath, string filePathFallback)
            : this(filePath)
        {
            if (!File.Exists(filePathFallback))
            {
                throw new FileNotFoundException(string.Format("XML Resource file {0} was not found", filePath));
            }

            _filePathFallback = filePathFallback;
        }

        public XmlResourceProvider(string filePath)
            : base(filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("XML Resource file {0} was not found", filePath));
            }

            _filePath = filePath;
        }

        protected override IList<ResourceEntry> ReadResources()
        {
            return ReadResources(_filePath);
        }

        protected override void LoadResourceEntities()
        {
            var res = ReadResources(_filePathFallback).ToDictionary(r => string.Format("{0}.{1}", r.Culture.ToLowerInvariant(), r.Name));
            ResourcesFallback.Add(_filePathFallback, res);

            base.LoadResourceEntities();
        }

        protected override object GetResourceFallback(string name, string culture)
        {
            return Resources[_filePath].ContainsKey(string.Format("{0}.{1}", culture, name))
                ? Resources[_filePath][string.Format("{0}.{1}", culture, name)].Value
                : ResourcesFallback[_filePathFallback][string.Format("{0}.{1}", culture, name)].Value;
        }

        protected override ResourceEntry ReadResource(string name, string culture)
        {
            return ReadResource(_filePath, name, culture) ?? ReadResource(_filePathFallback, name, culture);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private IList<ResourceEntry> ReadResources(string filePath)
        {
            return XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value
                }).ToList();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private ResourceEntry ReadResource(string filePath, string name, string culture)
        {
            return XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Where(e => e.Attribute("name").Value == name && e.Attribute("culture").Value == culture)
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value
                }).FirstOrDefault();
        }
    }
}
