using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
using createsend_dotnet;

namespace Mw.UmbFormsCampaignMonitor.ApiControllers
{
    [IsBackOffice]
    public class ListMappingController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public IEnumerable<BasicList> GetLists()
        {
            var auth = new ApiKeyAuthenticationDetails(CampaignMonitorConfiguration.ApiKey);
            var client = new Client(auth, CampaignMonitorConfiguration.ClientId);            
            return client.Lists();
        }

        [HttpGet]
        public IEnumerable<ListCustomField> GetListFields(string listId)
        {
            var auth = new ApiKeyAuthenticationDetails(CampaignMonitorConfiguration.ApiKey);            
            var list = new List(auth, listId);
            return list.CustomFields();
        }
    }
}
