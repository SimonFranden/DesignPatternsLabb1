using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    //This is the subject in the observer pattern
    public class ReminderManager : IObservable
    {
        private List<IReminderObserver> observers = new ();
        private List<string> reminders = new();

        public void UpdateReminders(List<IEvent> events)
        {
            List<IEvent> nearbyEvents = events.Where(e => e.Date.Date.Date == DateTime.Now.Date || e.Date.Date.Date == DateTime.Now.Date.AddDays(1)).ToList();
            nearbyEvents.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            if (nearbyEvents.Count > 0)
            {
                foreach(IEvent e in nearbyEvents)
                {
                    if(e.Date.Date.Date == DateTime.Now.Date)
                    {
                        reminders.Add(e.GetReminderMessage() + " *Today*");
                    }
                    else
                    {
                        reminders.Add(e.GetReminderMessage() + " *Tomorrow*");
                    }
                    
                }
                NotifyObservers();
            }
        }
        public void AddObserver(IReminderObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IReminderObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            Console.WriteLine(observers.Count);
            foreach (var observer in observers)
            {

                observer.Update(reminders);
            }
        }

    }
}
