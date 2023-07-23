using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ConcreteEvents
{
    public class Default : IEvent
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public string GetReminderMessage()
        {
            return $"Event Reminder: {Title} is coming up on {Date.ToString("d")}";
        }
    }
}
