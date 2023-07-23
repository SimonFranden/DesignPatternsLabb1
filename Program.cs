using CalendarApp;
using CalendarApp.Factories;
using CalendarApp.Interfaces;
using System.Reflection;

namespace DesignPatternsLabb1
{
    internal class Program
    {

        // Det här är en kalenderapplikation som använder sig av designmönstrena Singleton, Factory Method och Observer.
        // Singleton används för att säkerställa att endast en instans av kalendern finns.
        // Factory Method används för att skapa händelser (IEvent) till kalendern.
        // Observer används för att påminna om en händelse som händer imorgon eller idag.

        
        static void Main(string[] args)
        {
            Calendar calendar = Calendar.GetInstance();//This uses the singleton design pattern and is an observer

            //This uses the Observer pattern
            ReminderManager reminderManager = new();
            reminderManager.AddObserver(calendar);
            reminderManager.UpdateReminders(calendar.eventList);
            
            calendar.Start();

        }

        

    
    }
}