using CalendarApp.ConcreteEvents;
using CalendarApp.Factories;
using CalendarApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//This class uses the singleton design pattern and the observer pattern
namespace CalendarApp
{
    public class Calendar : IReminderObserver
    {
        private static Calendar instance;
        private SaveFileManager saveFileManager = new();

        public List<IEvent> eventList = new();
        private List<string> reminders = new();
        private Calendar()
        {
            eventList = saveFileManager.LoadEvents();                      
        }

        public static Calendar GetInstance()
        {
            if (instance == null)
            {
                instance = new Calendar();
            }

            return instance;
        }

        public void Start()
        {
            int menuSelection = 0;
            while (true)
            {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string title = " __                     \r\n/   _  |  _ __  _| _  __\r\n\\__(_| | (/_| |(_|(_| | ";
            Console.WriteLine(title);
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine($"║Today's Date: {DateTime.Now.ToString("d")}║");
            Console.WriteLine("╚════════════════════════╝");
            Console.WriteLine();
            DisplayReminders();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════Menu════════╗");
            Console.WriteLine("║1. Upcoming Events ║");
            Console.WriteLine("║2. Add Event       ║");
            Console.WriteLine("║3. Remove Event    ║");
            Console.WriteLine("║4. Exit            ║");
            Console.WriteLine("╚═══════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
               
                switch (menuSelection)
                {
                    case 0:
                        Console.WriteLine("Enter menu choice:");
                        break;
                    case 1:
                        ShowUpcomingEvents();
                        break;
                    case 2:
                        AddEvent();
                        break;
                    case 3:
                        RemoveEvent();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    case > 4:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                while (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.Write("Enter your choice (1-4): ");
                }

            }

        }

        private void AddEvent()
        {

            Console.ForegroundColor = ConsoleColor.Green;
            EventFactory eventFactory = new();

            Console.WriteLine("Select Event type");
            string eventType = ConsoleInput.SelectEventType();
            Console.WriteLine($"Event type: {eventType}");

            Console.Write("Title: ");
            string title = Console.ReadLine();
           
            DateTime date = ConsoleInput.SelectDate();           
            IEvent newEvent = eventFactory.CreateEvent(eventType, title, date);//This uses the Factory Method design pattern to create a new Event
            eventList.Add(newEvent);
            
            Console.WriteLine("Event Added");
            saveFileManager.SaveEvents(eventList);

        }

        private void RemoveEvent()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            if(eventList.Count < 1)
            {
                Console.WriteLine("No events to remove");
                return;
            }

            Console.WriteLine("Select event to remove");
            eventList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            eventList.RemoveAll(e => e.Date < DateTime.Now);
            for (int i = 0; i < eventList.Count; i++)
            {
                Console.WriteLine($"{i}. {eventList[i].GetType().Name}: {eventList[i].Title} - {eventList[i].Date.ToString("d")}");
            }

            int inputSelect;
            while (!int.TryParse(Console.ReadLine(), out inputSelect))
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine($"Are you sure you want to remove event '{eventList[inputSelect].Title}'? (Y/N)");
            bool validInput = false;
            while (!validInput)
            {
                string inputConfirm = Console.ReadLine().ToUpper();
                if (inputConfirm == "Y")
                {
                    Console.WriteLine("Event Removed");

                    eventList.Remove(eventList[inputSelect]);
                    validInput = true;
                }
                else if (inputConfirm == "N")
                {
                    Console.WriteLine("Dont Remove Event");
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                }
            }
        }

        private void ShowUpcomingEvents()
        {          
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Upcoming events");            
            eventList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            eventList.RemoveAll(e => e.Date.Date < DateTime.Now.Date);
            foreach (IEvent e in eventList)
            {
                    Console.WriteLine($"{e.Date.ToString("d")} - {e.GetType().Name}: {e.Title}");              
            }

        }
        private void DisplayReminders()
        {
            Console.WriteLine("════Reminders═══");
            if(reminders.Count == 0)
            {
                Console.WriteLine("No reminders today");
            }
            else
            {
                foreach (string reminder in reminders)
                {
                    Console.WriteLine(reminder);
                }
            }
            
        }

        public void Update(List<string> reminders)
        {
            this.reminders = reminders;
        }

        string filePath = "events.json";

    }
}
