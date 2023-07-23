using CalendarApp.Factories;
using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalendarApp
{
    public class SaveFileManager
    {
        string filePath = "events.json";

        public void SaveEvents(List<IEvent> events)
        {
            string jsonString = JsonSerializer.Serialize(ConvertListTypeToSaveFileEvent(events), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        public List<IEvent> LoadEvents()
        {
            string jsonString = File.ReadAllText(filePath);
            List<SaveFileEvent> saveFileEvents = JsonSerializer.Deserialize<List<SaveFileEvent>>(jsonString);
           
            return ConvertListTypeToIEvent(saveFileEvents);

        }

        private List<SaveFileEvent> ConvertListTypeToSaveFileEvent(List<IEvent> events)
        {
            List<SaveFileEvent> convertedList = new();
            foreach (IEvent e in events)
            {
                SaveFileEvent convertedEvent = new()
                {
                    Title = e.Title,
                    Date = e.Date,
                    EventType = e.GetType().Name.ToString()
                };
                convertedList.Add(convertedEvent);
            }

            return convertedList;
        }

        private List<IEvent> ConvertListTypeToIEvent(List<SaveFileEvent> events)
        {
            EventFactory eventFactory = new(); 
            List<IEvent> convertedList = new();
            foreach (SaveFileEvent e in events)
            {
                IEvent newEvent = eventFactory.CreateEvent(e.EventType, e.Title, e.Date);//This uses the Factory Method design pattern to create a new Event
                convertedList.Add(newEvent);
            }

            return convertedList;
        }

    }

    public class SaveFileEvent : IEvent
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string EventType { get; set; }

        public string GetReminderMessage()
        {
            throw new NotImplementedException();
        }
    }


}
