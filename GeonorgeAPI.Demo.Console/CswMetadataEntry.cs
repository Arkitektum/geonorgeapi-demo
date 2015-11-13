using System;
using System.Text;

namespace GeonorgeAPI.Demo.Console
{
    public class CswMetadataEntry
    {
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public string Uuid { get; set; }
        public string Protocol { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder(Title);
            builder.Append(" [").Append(Creator).AppendLine("]");
            builder.Append("\t uuid=").AppendLine(Uuid);
            builder.Append("\t uri=").AppendLine(Uri);
            builder.Append("\t protocol=").AppendLine(Protocol);
            builder.Append("\t type=").AppendLine(Type);
            return builder.ToString();
        }
    }
}
