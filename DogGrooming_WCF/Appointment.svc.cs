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
        public string CreateAppointment(string idGroomer, string idDog, string startTime, string idGroomingType, string duration, string comments)
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
                                var error = Create(idG, idD, startT, idGr, du, comments);
                                if (error == "") return "Success";
                                else return "Fail: " + error;
                            }
                            else { return "Fail: Invalid duration"; }
                        }
                        else { return "Fail: Invalid idGroomingType"; }
                    }
                    else { return "Fail: Invalid startTime"; }
                }
                else { return "Fail: Invalid idDog"; }
            }
            else { return "Fail: Invalid idGroomer"; }
        }

        public string DeleteAppointment(string idGroomer, string idDog, string startTime)
        {
            if (int.TryParse(idGroomer, out int idG))
            {
                if (int.TryParse(idDog, out int idD))
                {
                    var startT = ParseDateTime(startTime);
                    if (startT != new DateTime(1900, 1, 1))
                    {
                        Delete(idG, idD, startT);
                        return "Success";
                    }
                    else { return "Fail: Invalid startTime"; }
                }
                else { return "Fail: Invalid idDog"; }
            }
            else { return "Fail: Invalid idGroomer"; }
        }

        public Dictionary<string, string> GetAppointmentById(string idGroomer, string idDog, string startTime)
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
                }
            }
            return null;
        }

        public List<Dictionary<string, string>> GetAppointmentList()
        {
            return GetAll();
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

        private string Create(int idGroomer, int idDog, DateTime startTime, int idGroomingType, int duration, string comments)
        {
            // Insert into database
            try
            {
                var query = string.Concat("INSERT INTO Appointment VALUES(", idGroomer, ",", idDog, ",'", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "',", idGroomingType, ",", 90, ",'", comments, "')");
                MySqlDatabase.RunQuery(query);
                return "";
            }
            catch (Exception e) { return e.ToString(); }
        }

        private void Delete(int idGroomer, int idDog, DateTime startTime)
        {
            // Delete appointment from the database
            var query = string.Concat("DELETE FROM Appointment WHERE idGroomer = ", idGroomer, " AND idDog = ", idDog, " AND StartTime = '", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "'");
            MySqlDatabase.RunQuery(query);
        }

        private Dictionary<string, string> GetById(int idGroomer, int idDog, DateTime startTime)
        {
            try
            {
                // Retreve idGroomer, idDog, startTime
                var query = string.Concat("SELECT * FROM Appointment WHERE idGroomer = ", idGroomer, " AND idDog = ", idDog, " AND StartTime = '", startTime.ToString("yyyy-MM-dd HH:mm:ss"), "'");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 6)) return null;

                // Put information into a dictionary
                var appointmentDetails = new Dictionary<string, string>
                {
                    { "idGroomer", result.Rows[0]["idGroomer"].ToString() },
                    { "idDog", result.Rows[0]["idDog"].ToString() },
                    { "StartTime", result.Rows[0]["StartTime"].ToString() },
                    { "idGroomingType", result.Rows[0]["idGroomingType"].ToString() },
                    { "Duration", result.Rows[0]["Duration"].ToString() },
                    { "Comments", result.Rows[0]["Comments"].ToString() }
                };

                // Send dictionary back
                return appointmentDetails;
            }
            catch (Exception e) { return null; }
        }


        private List<Dictionary<string, string>> GetAll()
        {
            try
            {
                // Retrieve all appointments
                var query = string.Concat("SELECT * FROM Appointment");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 6)) return null;

                // Put information into a dictionary list
                var allAppointment = new List<Dictionary<string, string>>();
                Dictionary<string, string> appointmentDetails;
                for (int i=0; i<result.Rows.Count;i++)
                {
                    appointmentDetails = new Dictionary<string, string>
                    {
                        { "idGroomer", result.Rows[i]["idGroomer"].ToString() },
                        { "idDog", result.Rows[i]["idDog"].ToString() },
                        { "StartTime", result.Rows[i]["StartTime"].ToString() },
                        { "idGroomingType", result.Rows[i]["idGroomingType"].ToString() },
                        { "Duration", result.Rows[i]["Duration"].ToString() },
                        { "Comments", result.Rows[i]["Comments"].ToString() }
                    };
                    allAppointment.Add(appointmentDetails);
                }

                // Send list of dictionary back
                return allAppointment;
            }
            catch (Exception e) { return null; }
        }

    }
}
