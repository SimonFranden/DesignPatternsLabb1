using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Interfaces
{
    public interface IObservable
    {
        void AddObserver(IReminderObserver observer);
        void RemoveObserver(IReminderObserver observer);
        void NotifyObservers();
    }
}
