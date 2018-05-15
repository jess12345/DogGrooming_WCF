using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DogGrooming_WCF
{
    public class Groomer : IGroomer
    {
        public Dictionary<string, string> AuthenticateGroomer(string groomerEmail, string groomerPassword)
        {
            return Authenticate(groomerEmail, groomerPassword);
        }

        public string CreateGroomer(string firstname, string surname, string email, string password)
        {
            int idGroomer = Create(firstname, surname, email, password);
            if (idGroomer > 0) return "Success: " + idGroomer;
            else throw new FaultException<string>("User already exist", "User already exist");
        }

        public string DeleteGroomer(string idGroomer)
        {
            if (int.TryParse(idGroomer, out int id))
            {
                if (Delete(id)) return "Success";
                else throw new FaultException<string>("Cannot delete groomer", "Cannot delete groomer");
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public Dictionary<string, string> GetGroomerById(string idGroomer)
        {
            if (int.TryParse(idGroomer, out int id)) return GetById(id);
            else return null;
        }

        public List<Dictionary<string, string>> GetGroomerList()
        {
            return GetAll();
        }

        public string UpdateGroomer(string idGroomer, string firstname, string surname, string email, string password)
        {
            if (int.TryParse(idGroomer, out int id))
            {
                if (Update(id, firstname, surname, email, password)) return "Success";
                else throw new FaultException<string>("Cannot update groomer", "Cannot update groomer");
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }











        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//


        private Dictionary<string, string> Authenticate(string email, string password)
        {
            try
            {
                var result = MySqlDatabase.RunQuery("SELECT idGroomer, FirstName, Surname, Email FROM Groomer WHERE `email`='" + email + "' AND `password`='" + password + "'");
                if (result == null) throw new FaultException<string>("User does not exist", "User does not exist");
                if ((result.Rows.Count < 1) & (result.Columns.Count != 4)) return null;

                var userDetails = new Dictionary<string, string>
                {
                    { "idGroomer", result.Rows[0]["idGroomer"].ToString() },
                    { "FirstName", result.Rows[0]["FirstName"].ToString() },
                    { "Surname", result.Rows[0]["Surname"].ToString() },
                    { "Email", result.Rows[0]["Email"].ToString() }
                };

                return userDetails;
            }
            catch (Exception e) { throw new FaultException<string>("User does not exist", "User does not exist"); }
        }

        private int Create(string firstname, string surname, string email, string password)
        {
            // Insert into database
            // Retreve idGroomer and send it back
            return 1;
        }

        private bool Delete(int idGroomer)
        {
            // Delete groomer and appointments from the database
            return true;
        }

        private Dictionary<string, string> GetById(int idGroomer)
        {
            // Retreve idGroomer
            // Put information into a dictionary
            var groomerDetails = new Dictionary<string, string>();
            groomerDetails.Add("idGroomer", "1");
            groomerDetails.Add("FirstName", "Tom");
            groomerDetails.Add("Surname", "Groomer");
            groomerDetails.Add("Email", "tomgroomer@gmail.com");

            // Send dictionary back
            return groomerDetails;
        }

        private List<Dictionary<string, string>> GetAll()
        {
            // Retrieve all groomers
            var allGroomers = new List<Dictionary<string, string>>();
            // Put information in a list of dictionary

            var groomerDetails = new Dictionary<string, string>();
            groomerDetails.Add("idGroomer", "1");
            groomerDetails.Add("FirstName", "Tom");
            groomerDetails.Add("Surname", "Groomer");
            groomerDetails.Add("Email", "tomgroomer@gmail.com");

            allGroomers.Add(groomerDetails);

            // Send list of dictionary back
            return allGroomers;
        }

        private bool Update(int idGroomer, string firstname, string surname, string email, string password)
        {
            // Update groomer details
            // Sent status
            return true;
        }

    }
}
