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
        public DGroomer AuthenticateGroomer(string groomerEmail, string groomerPassword)
        {
            return Authenticate(groomerEmail, groomerPassword);
        }

        public DSuccess CreateGroomer(string firstname, string surname, string email, string password)
        {
            int idGroomer = Create(firstname, surname, email, password);
            if (idGroomer > 0) return new DSuccess(idGroomer);
            else throw new FaultException<string>("User already exist", "User already exist");
        }

        public DSuccess DeleteGroomer(string idGroomer)
        {
            if (int.TryParse(idGroomer, out int id))
            {
                if (Delete(id)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot delete groomer", "Cannot delete groomer");
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }

        public DGroomer GetGroomerById(string idGroomer)
        {
            if (int.TryParse(idGroomer, out int id)) return GetById(id);
            else return null;
        }

        public List<DGroomer> GetGroomerList()
        {
            return GetAll();
        }

        public DSuccess UpdateGroomer(string idGroomer, string firstname, string surname, string email, string password)
        {
            if (int.TryParse(idGroomer, out int id))
            {
                if (Update(id, firstname, surname, email, password)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot update groomer", "Cannot update groomer");
            }
            else { throw new FaultException<string>("Invalid idGroomer", "Invalid idGroomer"); }
        }











        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//


        private DGroomer Authenticate(string email, string password)
        {
            try
            {
                var result = MySqlDatabase.RunQuery("SELECT idGroomer, FirstName, Surname, Email FROM Groomer WHERE `email`='" + email + "' AND `password`='" + password + "'");
                if (result == null) throw new FaultException<string>("User does not exist", "User does not exist");
                if ((result.Rows.Count < 1) & (result.Columns.Count != 4)) throw new FaultException<string>("User does not exist", "User does not exist");
                
                var groomer = new DGroomer(
                    int.Parse(result.Rows[0]["idGroomer"].ToString()),
                    result.Rows[0]["FirstName"].ToString(),
                    result.Rows[0]["Surname"].ToString(),
                    result.Rows[0]["Email"].ToString()
                    );

                return groomer;
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

        private DGroomer GetById(int idGroomer)
        {
            return new DGroomer(1, "Tom", "Groomer", "tomgroomer@gmail.com"); //Dummy Data
        }

        private List<DGroomer> GetAll()
        {
            var allGroomers = new List<DGroomer>();
            allGroomers.Add(new DGroomer(1, "Tom", "Groomer", "tomgroomer@gmail.com")); //Dummy Data
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
