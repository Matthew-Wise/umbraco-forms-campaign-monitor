using System.Collections.Generic;
using System.Linq;

namespace Mw.UmbFormsCampaignMonitor.Models
{
    public class ListMappingModel
    {
        public ListMappingModel()
        {
            Mappings = Enumerable.Empty<FieldMappingModel>();
        }

        public string ListId { get; set; }

        public IEnumerable<FieldMappingModel> Mappings { get; set; }
    }  

}
