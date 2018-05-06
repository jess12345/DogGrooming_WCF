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
            else return "Fail: Grooming type already exist";
        }

        public string DeleteGroomingType(string idGroomingType)
        {
            if (int.TryParse(idGroomingType, out int id))
            {
                if (Delete(id)) return "Success";
                else return "Fail: Cannot delete grooming type";
            }
            else { return "Fail: Invalid idGroomingType"; }
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
            // Retrieve all grooming type
            var allGroomingType = new List<Dictionary<string, string>>();
            // Put information in a list of dictionary

            var typeDetails = new Dictionary<string, string>();
            typeDetails.Add("idGroomingType", "1");
            typeDetails.Add("Name", "Wash only");
            allGroomingType.Add(typeDetails);

            typeDetails = new Dictionary<string, string>();
            typeDetails.Add("idGroomingType", "2");
            typeDetails.Add("Name", "Wash and nail clipping");
            allGroomingType.Add(typeDetails);

            typeDetails = new Dictionary<string, string>();
            typeDetails.Add("idGroomingType", "3");
            typeDetails.Add("Name", "Delux grooming");
            allGroomingType.Add(typeDetails);

            // Send list of dictionary back
            return allGroomingType;
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
