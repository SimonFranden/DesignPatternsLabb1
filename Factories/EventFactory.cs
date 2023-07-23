using CalendarApp.ConcreteEvents;
using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Factories
{
    public class EventFactory
    {
        public IEvent CreateEvent(string eventType, string title, DateTime startTime)
        {
            switch (eventType.ToLower())
            {
                case "meeting":
                    return new Meeting
                    {
                        Title = title,
                        Date = startTime
                    };

                case "birthday":
                    return new Birthday
                    {
                        Title = title,
                        Date = startTime
                    };

                case "vacation":
                    return new Vacation
                    {
                        Title = title,
                        Date = startTime
                    };

                case "default":
                    return new Default
                    {
                        Title = title,
                        Date = startTime
                    };


                default:
                    throw new ArgumentException("Invalid event type.");
            }
        }
    }

}
