using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GroomingType" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GroomingType.svc or GroomingType.svc.cs at the Solution Explorer and start debugging.
    public class GroomingType : IGroomingType
    {
        public string CreateGroomingType(string name)
        {
            int idGroomingType = Create(name);
            if (idGroomingType > 0) return "Success: " + idGroomingType;
            else throw new FaultException<string>("Grooming type already exist", "Grooming type already exist");
        }

        public string DeleteGroomingType(string idGroomingType)
        {
            if (int.TryParse(idGroomingType, out int id))
            {
                if (Delete(id)) return "Success";
                else throw new FaultException<string>("Cannot delete grooming type", "Cannot delete grooming type");
            }
            else { throw new FaultException<string>("Invalid idGroomingType", "Invalid idGroomingType"); }
        }

        public List<Dictionary<string, string>> GeGroomingTypeList()
        {
            return GetAll();
        }

        public Dictionary<string, string> GetGroomingById(string idGroomingType)
        {
            if (int.TryParse(idGroomingType, out int id)) return GetById(id);
            else return null;
        }




        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//

        private int Create(string name)
        {
            // Insert into database
            // Retreve idBreed and send it back
            return 1;
        }


        private bool Delete(int idGroomingType)
        {
            // Delete grooming types and all appointments of this type from the database
            return true;
        }


        private List<Dictionary<string, string>> GetAll()
        {
            try
            {
                // Retrieve all appointments
                var query = string.Concat("SELECT idGroomingType, `Name` FROM GroomingType");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 2)) return null;

                // Put information into a dictionary list
                var allGroomingTypes = new List<Dictionary<string, string>>();
                Dictionary<string, string> groomingDetails;
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    groomingDetails = new Dictionary<string, string>
                    {
                        { "idGroomingType", result.Rows[i]["idGroomingType"].ToString() },
                        { "Name", result.Rows[i]["Name"].ToString() }
                    };
                    allGroomingTypes.Add(groomingDetails);
                }

                // Send list of dictionary back
                return allGroomingTypes;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private Dictionary<string, string> GetById(int idGroomingType)
        {
            // Retreve idGroomingType
            // Put information into a dictionary
            var typeDetails = new Dictionary<string, string>();
            typeDetails.Add("idGroomingType", "3");
            typeDetails.Add("Name", "Delux grooming");

            // Send dictionary back
            return typeDetails;
        }



    }
}
