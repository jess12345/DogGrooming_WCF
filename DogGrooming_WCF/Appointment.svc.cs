using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    public class Appointment : IAppointment
    {
        public DSuccess CreateAppointment(string idGroomer, string idDog, string startTime, string idGroomingType, string duration, string comments)
        {
            if (int.TryParse(idGroomer, out int idG))
            {
                if (int.TryParse(idDog, out int idD))
                {
                    var startT = ParseDateTime(startTime);
                    if (startT != new DateTime(1900, 1, 1))
                    {
                        if (int.TryParse(idGroomingType, out int idGr))
                        {
                            if (int.TryParse(duration, out int du))
                            {
                                Create(idG, idD, startT, idGr, du, comments);
                                return new DSuccess(idG);
                            }
                            else { throw new FaultException<string>("Invalid duration", "Invalid duration"); }
                        }
                        else { throw new FaultException<string>("Invalid idGroomingType", "Invalid idGroomingType"); }
                    }
                    else { throw new FaultException<string>("Invalid startTime", "Invalid startTime"); }
                }
                else { throw new FaultException<string>("Fail: Invalid idDog", "Fail: Invalid idDog"); }
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public DSuccess DeleteAppointment(string idGroomer, string idDog, string startTime)
        {
            if (int.TryParse(idGroomer, out int idG))
            {
                if (int.TryParse(idDog, out int idD))
                {
                    var startT = ParseDateTime(startTime);
                    if (startT != new DateTime(1900, 1, 1))
                    {
                        Delete(idG, idD, startT);
                        return new DSuccess(idG);
                    }
                    else { throw new FaultException<string>("Invalid startTime", "Invalid startTime"); }
                }
                else { throw new FaultException<string>("Invalid idDog", "Invalid idDog"); }
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public DAppointment GetAppointmentById(string idGroomer, string idDog, string startTime)
        {
            if (int.TryParse(idGroomer, out int idG))
            {
                if (int.TryParse(idDog, out int idD))
                {
                    var startT = ParseDateTime(startTime);
                    if (startT != new DateTime(1900, 1, 1))
                    {
                        return GetById(idG, idD, startT);
                    }
                    else { throw new FaultException<string>("Cannot find appointment", "Cannot find appointment"); }
                }
                else { throw new FaultException<string>("Invalid idDog", "Invalid idDog"); }
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public List<DAppointment> GetAppointmentsByClient(string idClient)
        {
            if (int.TryParse(idClient, out int idC))
            {
                return GetAllForClient(idC);
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public List<DAppointment> GetAppointmentByGroomer(string idGroomer)
        {
            if (int.TryParse(idGroomer, out int idG))
            {
                return GetAllForGroomer(idG);
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public List<DAppointment> GetAppointmentList()
        {
            return GetAll();
        }

        public DSuccess SendReminderForTomorrow()
        {
            int NumEmailsToSend;
            int NumPassed = 0;
            string failedEmails = "";
            string errorMessage = "";

            var reminders = GetAppointmentReminderForClients(DateTime.Today.AddDays(1));
            NumEmailsToSend = reminders.Count();
            foreach (var r in reminders)
            {
                try
                {
                    Email.Send(r.Email, "Dog Gooming Appointment Reminder", r.Message);
                    NumPassed++;
                }
                catch (Exception e)
                {
                    failedEmails = string.Concat(failedEmails, " ", r.Email, ",");
                    errorMessage = string.Concat(errorMessage, " <<|>> " + e.Message);
                }
            }

            if (failedEmails == "")
                return new DSuccess(1);
            else
                throw new FaultException<string>("Cannot send to: " + failedEmails, "Cannot send to: " + failedEmails + " | Because: "+errorMessage);
            
        }


        //====================================================//
        //================== Reminder System =================//
        //====================================================//


        public static List<DAppointmentReminder> GetAppointmentReminderForClients(DateTime dateTime)
        {
            try
            {
                var query = @"SELECT c.Email, CONCAT('Hi ',c.FirstName,',<br><br>This is a reminder that you have a ',gt.`Name`,
						                            ' grooming service with ',g.FirstName,' ',g.Surname,' at ',a.StartTime,
                                                    ' for ',a.Duration,' minutes. This appointment is for ',d.`Name`,' your ',b.`Name`,
                                                    '. This service will be provided at ',c.HomeAddress,
                                                    '. If you have any queries, please contact ',g.FirstName,' ',g.Surname,
                                                    ' at ',g.Email,'.<br><br>We look forward to seeing you.<br><br>Kind regards,<br>Tom''s Dog Grooming Business') Message
                            FROM Appointment a 
                            INNER JOIN Groomer g ON g.idGroomer = a.idGroomer 
                            INNER JOIN Dog d ON d.idDog = a.idDog 
                            INNER JOIN GroomingType gt ON gt.idGroomingType = a.idGroomingType 
                            INNER JOIN Breed b ON b.idBreed = d.idBreed
                            INNER JOIN `Client` c ON c.idClient = d.idClient
                            WHERE DATE(a.StartTime)='" + dateTime.ToString("yyyy-MM-dd") + "';";
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 2)) throw new FaultException<string>("Cannot get appointments", "Cannot get appointments"); ;

                var allReminders = new List<DAppointmentReminder>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allReminders.Add(new DAppointmentReminder(
                        result.Rows[i]["Email"].ToString(),
                        result.Rows[i]["Message"].ToString()
                        ));
                }
                return allReminders;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }


        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//

        private DateTime ParseDateTime(string value) // Value is in the format of YYYY-MM-DD-HH-MM-SS
        {
            var split = value.Split('-');
            bool success;
            int counter = 0;
            int year = 0;
            int month = 0;
            int day = 0;
            int hr = 0;
            int min = 0;
            int sec = 0;
            foreach (var s in split)
            {
                success = int.TryParse(s, out int n);
                if (!success) break;
                switch (counter)
                {
                    case 0: year = n; break;
                    case 1: month = n; break;
                    case 2: day = n; break;
                    case 3: hr = n; break;
                    case 4: min = n; break;
                    case 5: sec = n; break;
                }
                counter++;
            }
            if (counter < 6) return new DateTime(1900, 1, 1);
            return new DateTime(year, month, day, hr, min, sec);
        }

        private void Create(int idGroomer, int idDog, DateTime startTime, int idGroomingType, int duration, string comments)
        {
            // Insert into database
            try
            {
                var query = string.Concat("INSERT INTO Appointment VALUES(", idGroomer, ",", idDog, ",'", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "',", idGroomingType, ",", 90, ",'", comments, "')");
                MySqlDatabase.RunQuery(query);
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private void Delete(int idGroomer, int idDog, DateTime startTime)
        {
            // Delete appointment from the database
            var query = string.Concat("DELETE FROM Appointment WHERE idGroomer = ", idGroomer, " AND idDog = ", idDog, " AND StartTime = '", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "'");
            MySqlDatabase.RunQuery(query);
        }

        private DAppointment GetById(int idGroomer, int idDog, DateTime startTime)
        {
            try
            {
                var query = string.Concat("SELECT a.idGroomer, CONCAT(g.FirstName,' ',g.Surname) GroomerName, a.idDog, d.`Name` DogName, d.idClient, CONCAT(c.FirstName,' ',c.Surname) ClientName, a.StartTime, a.idGroomingType, gt.`Name` GroomingTypeName, a.Duration, c.HomeAddress Location, IFNULL(a.Comments,'') Comments FROM Appointment a INNER JOIN Groomer g ON g.idGroomer = a.idGroomer INNER JOIN Dog d ON d.idDog = a.idDog INNER JOIN GroomingType gt ON gt.idGroomingType = a.idGroomingType INNER JOIN `Client` c ON c.idClient = d.idClient WHERE a.idGroomer = ", idGroomer, " AND a.idDog = ", idDog, " AND a.StartTime = '", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "'");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 12)) throw new FaultException<string>("Cannot get appointments", "Cannot get appointments"); ;

                return new DAppointment(
                    int.Parse(result.Rows[0]["idGroomer"].ToString()),
                    result.Rows[0]["GroomerName"].ToString(),
                    int.Parse(result.Rows[0]["idDog"].ToString()),
                    result.Rows[0]["DogName"].ToString(),
                    int.Parse(result.Rows[0]["idClient"].ToString()),
                    result.Rows[0]["ClientName"].ToString(),
                    result.Rows[0]["StartTime"].ToString(),
                    int.Parse(result.Rows[0]["idGroomingType"].ToString()),
                    result.Rows[0]["GroomingTypeName"].ToString(),
                    int.Parse(result.Rows[0]["Duration"].ToString()),
                    result.Rows[0]["Location"].ToString(),
                    result.Rows[0]["Comments"].ToString()
                    );
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }


        private List<DAppointment> GetAll()
        {
            try
            {
                var query = "SELECT a.idGroomer, CONCAT(g.FirstName,' ',g.Surname) GroomerName, a.idDog, d.`Name` DogName, d.idClient, CONCAT(c.FirstName,' ',c.Surname) ClientName, a.StartTime, a.idGroomingType, gt.`Name` GroomingTypeName, a.Duration, c.HomeAddress Location, IFNULL(a.Comments,'') Comments FROM Appointment a INNER JOIN Groomer g ON g.idGroomer = a.idGroomer INNER JOIN Dog d ON d.idDog = a.idDog INNER JOIN GroomingType gt ON gt.idGroomingType = a.idGroomingType INNER JOIN `Client` c ON c.idClient = d.idClient";
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 12)) throw new FaultException<string>("Cannot get appointments", "Cannot get appointments"); ;

                var allAppointment = new List<DAppointment>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allAppointment.Add(new DAppointment(
                        int.Parse(result.Rows[i]["idGroomer"].ToString()),
                        result.Rows[i]["GroomerName"].ToString(),
                        int.Parse(result.Rows[i]["idDog"].ToString()),
                        result.Rows[i]["DogName"].ToString(),
                        int.Parse(result.Rows[i]["idClient"].ToString()),
                        result.Rows[i]["ClientName"].ToString(),
                        result.Rows[i]["StartTime"].ToString(),
                        int.Parse(result.Rows[i]["idGroomingType"].ToString()),
                        result.Rows[i]["GroomingTypeName"].ToString(),
                        int.Parse(result.Rows[i]["Duration"].ToString()),
                        result.Rows[i]["Location"].ToString(),
                        result.Rows[i]["Comments"].ToString()
                        ));
                }
                return allAppointment;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private List<DAppointment> GetAllForClient(int idClient)
        {
            try
            {
                var query = "SELECT a.idGroomer, CONCAT(g.FirstName,' ',g.Surname) GroomerName, a.idDog, d.`Name` DogName, d.idClient, CONCAT(c.FirstName,' ',c.Surname) ClientName, a.StartTime, a.idGroomingType, gt.`Name` GroomingTypeName, a.Duration, c.HomeAddress Location, IFNULL(a.Comments,'') Comments FROM Appointment a INNER JOIN Groomer g ON g.idGroomer = a.idGroomer INNER JOIN Dog d ON d.idDog = a.idDog INNER JOIN GroomingType gt ON gt.idGroomingType = a.idGroomingType INNER JOIN `Client` c ON c.idClient = d.idClient WHERE d.idClient = " + idClient;
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 12)) throw new FaultException<string>("Cannot get appointments", "Cannot get appointments"); ;

                var allAppointment = new List<DAppointment>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allAppointment.Add(new DAppointment(
                        int.Parse(result.Rows[i]["idGroomer"].ToString()),
                        result.Rows[i]["GroomerName"].ToString(),
                        int.Parse(result.Rows[i]["idDog"].ToString()),
                        result.Rows[i]["DogName"].ToString(),
                        int.Parse(result.Rows[i]["idClient"].ToString()),
                        result.Rows[i]["ClientName"].ToString(),
                        result.Rows[i]["StartTime"].ToString(),
                        int.Parse(result.Rows[i]["idGroomingType"].ToString()),
                        result.Rows[i]["GroomingTypeName"].ToString(),
                        int.Parse(result.Rows[i]["Duration"].ToString()),
                        result.Rows[i]["Location"].ToString(),
                        result.Rows[i]["Comments"].ToString()
                        ));
                }
                return allAppointment;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private List<DAppointment> GetAllForGroomer(int idGroomer)
        {
            try
            {
                var query = "SELECT a.idGroomer, CONCAT(g.FirstName,' ',g.Surname) GroomerName, a.idDog, d.`Name` DogName, d.idClient, CONCAT(c.FirstName,' ',c.Surname) ClientName, a.StartTime, a.idGroomingType, gt.`Name` GroomingTypeName, a.Duration, c.HomeAddress Location, IFNULL(a.Comments,'') Comments FROM Appointment a INNER JOIN Groomer g ON g.idGroomer = a.idGroomer INNER JOIN Dog d ON d.idDog = a.idDog INNER JOIN GroomingType gt ON gt.idGroomingType = a.idGroomingType INNER JOIN `Client` c ON c.idClient = d.idClient WHERE a.idGroomer = " + idGroomer;
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 12)) throw new FaultException<string>("Cannot get appointments", "Cannot get appointments"); ;

                var allAppointment = new List<DAppointment>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allAppointment.Add(new DAppointment(
                        int.Parse(result.Rows[0]["idGroomer"].ToString()),
                        result.Rows[0]["GroomerName"].ToString(),
                        int.Parse(result.Rows[0]["idDog"].ToString()),
                        result.Rows[0]["DogName"].ToString(),
                        int.Parse(result.Rows[0]["idClient"].ToString()),
                        result.Rows[0]["ClientName"].ToString(),
                        result.Rows[0]["StartTime"].ToString(),
                        int.Parse(result.Rows[0]["idGroomingType"].ToString()),
                        result.Rows[0]["GroomingTypeName"].ToString(),
                        int.Parse(result.Rows[0]["Duration"].ToString()),
                        result.Rows[0]["Location"].ToString(),
                        result.Rows[0]["Comments"].ToString()
                        ));
                }
                return allAppointment;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

    }
}
