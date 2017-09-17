using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Mw.UmbFormsCampaignMonitor.Models;
using createsend_dotnet;

namespace Mw.UmbFormsCampaignMonitor.Workflows
{
    public class PushToMarketing : WorkflowType
    {
        public PushToMarketing()
        {
            Name = "Send to Campaign Monitor";
            Icon = "icon-settings";
            Description = "Pushes the record to Campaign Monitor";
            Id = new Guid("3ad2abbe-676d-489b-a7c6-aa71f931c347");
        }


        //TODO: Custom picker also needs to get the list id.
        [Umbraco.Forms.Core.Attributes.Setting("List Mapping", description = "Map the form fields to the marketing fields", view = "~/App_Plugins/Mw.UmbFormsCampaignMonitor/listmapping.html")]
        public string ListMapping { get; set; }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            try
            {
                if (ValidateSettings().Any() || string.IsNullOrWhiteSpace(CampaignMonitorConfiguration.ApiKey) || string.IsNullOrWhiteSpace(CampaignMonitorConfiguration.ClientId))
                {
                    return WorkflowExecutionStatus.NotConfigured;
                }
                var listMapping = JsonConvert.DeserializeObject<ListMappingModel>(ListMapping);
                var subscribeString = GetMapAsString(listMapping, record, "Subscribe");
                var subscribe = !string.IsNullOrWhiteSpace(subscribeString) && subscribeString != "False";
                if(!subscribe)
                {
                    return WorkflowExecutionStatus.Completed;
                }
                var emailAddress = GetMapAsString(listMapping, record, "Email Address");                
                if (string.IsNullOrWhiteSpace(emailAddress))
                {
                    return WorkflowExecutionStatus.Completed;
                }
                var name = GetMapAsString(listMapping, record, "Name");
                var lastName = GetMapAsString(listMapping, record, "Lastname");
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    name += " " + lastName;
                }
                var auth = new ApiKeyAuthenticationDetails(CampaignMonitorConfiguration.ApiKey);
                var subscriber = new Subscriber(auth, listMapping.ListId);
                subscriber.Add(emailAddress, name, listMapping.Mappings.
                    Where(m => m.ListField.StartsWith("[")).Select(m => {
                    return new SubscriberCustomField
                    {
                        Key = m.ListField,
                        Value = GetFieldValue(record, m)
                    };
                }).ToList(), subscribe);
                return WorkflowExecutionStatus.Completed;
            }
            catch (Exception ex)
            {
                LogHelper.Error<PushToMarketing>("Failed to send users record to marketing", ex);
                return WorkflowExecutionStatus.Failed;
            }
        }

        public string GetFieldValue(Record record, FieldMappingModel map)
        {
            if (map == null)
            {
                return string.Empty;
            }
            var value = map.StaticValue;
            Guid fieldGuid;
            if (Guid.TryParse(map.Field, out fieldGuid))
            {
                var field = record.GetRecordField(fieldGuid);
                value = field.ValuesAsString();
            }
            return value;
        }

        public string GetMapAsString(ListMappingModel listMapping, Record record, string listField)
        {
            var map = listMapping.Mappings.FirstOrDefault(m => m.ListField == listField);
            return GetFieldValue(record, map);
        }

        public override List<Exception> ValidateSettings()
        {
            var errors = new List<Exception>();
            var mapping = JsonConvert.DeserializeObject<ListMappingModel>(ListMapping);
            if (string.IsNullOrWhiteSpace(mapping.ListId))
            {
                errors.Add(new Exception("A list must be selected"));
                return errors;
            }
            if (mapping.Mappings.Any())
            {
                var emailMap = mapping.Mappings.FirstOrDefault(m => m.ListField == "Email Address");
                if (emailMap == null || (string.IsNullOrWhiteSpace(emailMap.Field) && string.IsNullOrWhiteSpace(emailMap.StaticValue)))
                {
                    errors.Add(new Exception("Email address mapping is required"));
                }
            }
            else
            {
                errors.Add(new Exception("No mappings found, Email address is required"));
            }
            return errors;
        }
    }
}