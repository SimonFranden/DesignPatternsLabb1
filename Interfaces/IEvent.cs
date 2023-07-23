using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Interfaces
{
    public interface IEvent
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public string GetReminderMessage();

    }
}
