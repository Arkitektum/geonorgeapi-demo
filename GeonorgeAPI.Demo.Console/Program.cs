using System.Collections.Generic;
using GeoNorgeAPI;
using www.opengis.net;

namespace GeonorgeAPI.Demo.Console
{
    public class Program
    {
        private static GeoNorge _api = new GeoNorge();

        public static void Main(string[] args)
        {
            SearchForServices();
        }

        private static void SearchForServices()
        {
            var searchFilter = new object[]
                {
                    new PropertyIsLikeType
                    {
                        PropertyName = new PropertyNameType {Text = new[] { "Type" }},
                        Literal = new LiteralType {Text = new[] { "service" }}
                    }
                };

            var searchFilterNames = new[]
                {
                    ItemsChoiceType23.PropertyIsLike,
                };

            SearchResultsType searchResults = _api.SearchWithFilters(searchFilter, searchFilterNames);
            List<CswMetadataEntry> metadataEntries = ParseSearchResults(searchResults);
            
            metadataEntries.ForEach(System.Console.WriteLine);
        }

        private static List<CswMetadataEntry> ParseSearchResults(SearchResultsType results)
        {
            List<CswMetadataEntry> metadatEntries = new List<CswMetadataEntry>();   
            if (results.Items != null)
            {
                foreach (var item in results.Items)
                {
                    RecordType record = (RecordType)item;
                    var metadataEntry = new CswMetadataEntry();
                    for (int i = 0; i < record.ItemsElementName.Length; i++)
                    {
                        var name = record.ItemsElementName[i];
                        var value = record.Items[i].Text != null ? record.Items[i].Text[0] : null;

                        if (name == ItemsChoiceType24.title)
                            metadataEntry.Title = value;
                        else if (name == ItemsChoiceType24.URI && value != null && value.StartsWith("http"))
                            metadataEntry.Uri = value;
                        else if (name == ItemsChoiceType24.identifier)
                            metadataEntry.Uuid = value;
                        else if (name == ItemsChoiceType24.creator)
                            metadataEntry.Creator = value;
                        else if (name == ItemsChoiceType24.type)
                            metadataEntry.Type = value;
                    }
                    metadatEntries.Add(metadataEntry);
                }
            }
            return metadatEntries;
        }
    }
}
