using System;
using DogGrooming_WCF;

namespace ReminderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var appt = new Appointment();
                Console.WriteLine("Sent " + appt.SendReminderForTomorrow() + " emails");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
