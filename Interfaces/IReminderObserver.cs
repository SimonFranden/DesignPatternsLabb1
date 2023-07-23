using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Interfaces
{
    public interface IReminderObserver
    {
        public void Update(List<string> reminders);
    }
}
