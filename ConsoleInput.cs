using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    public class ConsoleInput
    {
        public static string SelectEventType()
        {
            List<Type> eventTypes = GetEventTypes();
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < eventTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {eventTypes[i].Name}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > eventTypes.Count)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }

            Type selectedEventType = eventTypes[choice - 1];
            IEvent selectedEvent = Activator.CreateInstance(selectedEventType) as IEvent;

            string selectedEventString = selectedEvent.GetType().Name.ToLower();

            return selectedEventString;
        }

        static List<Type> GetEventTypes()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> eventTypes = assembly.GetTypes()
                .Where(t => typeof(IEvent).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && t != typeof(SaveFileEvent))
                .ToList();

            return eventTypes;
        }

        public static DateTime SelectDate()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Please enter a date in the format MM/dd/yyyy: ");

            string input = Console.ReadLine();
            DateTime selectedDate;

            while (!DateTime.TryParseExact(input, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out selectedDate))
            {
                Console.WriteLine("Invalid date format, or the date do not exist");
                Console.Write("Please enter a date in the format MM/dd/yyyy: ");
                input = Console.ReadLine();
            }

            return selectedDate;
        }
    }
}
