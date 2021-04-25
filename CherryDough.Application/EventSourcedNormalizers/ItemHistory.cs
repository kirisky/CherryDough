using System;
using System.Collections.Generic;
using System.Linq;
using CherryDough.Doamin.Core.Events;
using Newtonsoft.Json;

namespace CherryDough.Application.EventSourcedNormalizers
{
    public static class ItemHistory
    {
        public static List<ItemHistoryData> HistoryData { get; set; }

        public static IList<ItemHistoryData> GenerateItemHistoryData(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ItemHistoryData>();
            ItemHistoryDataDeserializer(storedEvents);

            var sortedHistoryData = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<ItemHistoryData>();
            var lastHistoryData = new ItemHistoryData();
            
            foreach (var historyData in sortedHistoryData)
            {
                var jsSlot = new ItemHistoryData
                {
                    Id = historyData.Id == Guid.Empty.ToString() || historyData.Id == lastHistoryData.Id
                        ? ""
                        : historyData.Id,
                    Name = string.IsNullOrWhiteSpace(historyData.Name) || historyData.Name == lastHistoryData.Name
                        ? ""
                        : historyData.Name,
                    Category = string.IsNullOrWhiteSpace(historyData.Category) || historyData.Category == lastHistoryData.Category
                        ? ""
                        : historyData.Category,
                    Action = string.IsNullOrWhiteSpace(historyData.Action) ? "" : historyData.Action,
                    Timestamp = historyData.Timestamp,
                    Who = historyData.Who
                };
                
                list.Add(jsSlot);
                lastHistoryData = historyData;
            }

            return list;
        }

        private static void ItemHistoryDataDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var storedEvent in storedEvents)
            {
                var historyData = JsonConvert.DeserializeObject<ItemHistoryData>(storedEvent.Data);
                if (historyData == null) continue;
                historyData.Timestamp = DateTime
                    .Parse(historyData.Timestamp)
                    .ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (storedEvent.MessageType)
                {
                    case "AddedItemEvent":
                        historyData.Action = "Added";
                        historyData.Who = storedEvent.User;
                        break;
                    case "UpdatedItemEvent":
                        historyData.Action = "Updated";
                        historyData.Who = storedEvent.User;
                        break;
                    case "RemovedItemEvent":
                        historyData.Action = "Removed";
                        historyData.Who = storedEvent.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = storedEvent.User ?? "Anonymous";
                        break;
                }

                HistoryData.Add(historyData);
            }
        }
    }
}