using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Mw.UmbFormsCampaignMonitor;
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
                //if (ValidateSettings().Any())
                //{
                //    return WorkflowExecutionStatus.NotConfigured;
                //}
                //var settings = MappingToModel();
                //var mappedFields = record.RecordFields.Select(f =>
                //{
                //    string listField;
                //    var mapped = settings.Mappings.TryGetValue(f.Key.ToString(), out listField);
                //    if (!mapped || string.IsNullOrWhiteSpace(listField)) return null;
                //    return new
                //    {
                //        ListField = listField,
                //        Value = f.Value.ValuesAsString()
                //    };
                //});
                //if (!mappedFields.Any())
                //{
                //    throw new Exception("No mappings found");
                //}               

            }
            catch (Exception ex)
            {
                LogHelper.Error<PushToMarketing>("Failed to send users record to marketing", ex);
                return WorkflowExecutionStatus.Failed;
            }
            return WorkflowExecutionStatus.Completed;
        }        

        public override List<Exception> ValidateSettings()
        {
            var apiKey = CampaignMonitorConfiguration.ApiKey;
            var clientId = CampaignMonitorConfiguration.ClientId;
            return new List<Exception>();
            //var errors = new List<Exception>();
            //try
            //{
            //    var settings = MappingToModel();
            //    if (settings == null) errors.Add(new NullReferenceException("No settings configured"));
            //    if (string.IsNullOrWhiteSpace(settings.Provider)) errors.Add(new NullReferenceException("Provider must be set"));
            //    if (string.IsNullOrWhiteSpace(settings.ListId)) errors.Add(new NullReferenceException("ListId must be set"));
            //    if (settings.Mappings.Any()) errors.Add(new Exception("Must have at least one mapping"));
            //}
            //catch (Exception ex)
            //{
            //    errors.Add(ex);
            //}
            //return errors;
        }
    }
}