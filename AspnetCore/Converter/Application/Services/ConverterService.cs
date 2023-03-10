using Converter.Contracts.Interfaces;

namespace Converter.Application.Services
{
    public class ConverterService : IConverterService
    {
        public ConverterService()
        {
        }

        public string ConverterJsonToCsv(string jsonString)
        {
            jsonString = jsonString.Trim();

            string csv = String.Empty;

            if ((jsonString.StartsWith("[") && jsonString.EndsWith("]") || jsonString.StartsWith("{") && jsonString.EndsWith("}"))
                && jsonString.Contains("{") && jsonString.Contains("}") && jsonString.Contains(":"))
            {
                var clearedJson = jsonString.TrimStart('[').TrimEnd(']');

                var elements = clearedJson.Split('}');
                elements = elements.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

                for (int i = 0; i < elements.Length; i++)
                {
                    if (i == 0)
                    {
                        var labelsElements = elements[i].Split(",");
                        labelsElements = labelsElements.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

                        for (int l = 0; l < labelsElements.Length; l++)
                        {
                            var label = labelsElements[l].Split(":")[0];

                            label = label.Replace("{", "").Replace("\"", "").Trim();

                            if (l > 0)
                                csv += ",";

                            csv += label;

                            if (l == labelsElements.Length - 1)
                                csv += "\n";
                        }
                    }

                    var dataElements = elements[i].Split(",");
                    dataElements = dataElements.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

                    for (int d = 0; d < dataElements.Length; d++)
                    {
                        var data = dataElements[d].Split(":")[1];

                        data = data.Replace("\"", "").Trim();

                        if (d > 0)
                            csv += ",";

                        csv += data;

                        if (d == dataElements.Length - 1 && i != elements.Length - 1)
                            csv += "\n";
                    }
                }

                return csv;
            }

            throw new Exception("invalid-json");
        }
    }
}
