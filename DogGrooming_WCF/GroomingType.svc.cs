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
        public DSuccess CreateGroomingType(string name)
        {
            int idGroomingType = Create(name);
            if (idGroomingType > 0) return new DSuccess(idGroomingType);
            else throw new FaultException<string>("Grooming type already exist", "Grooming type already exist");
        }

        public DSuccess DeleteGroomingType(string idGroomingType)
        {
            if (int.TryParse(idGroomingType, out int id))
            {
                if (Delete(id)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot delete grooming type", "Cannot delete grooming type");
            }
            else { throw new FaultException<string>("Invalid idGroomingType", "Invalid idGroomingType"); }
        }

        public List<DGroomerType> GeGroomingTypeList()
        {
            return GetAll();
        }

        public DGroomerType GetGroomingById(string idGroomingType)
        {
            if (int.TryParse(idGroomingType, out int id)) return GetById(id);
            else throw new FaultException<string>("Invalid idGroomingType", "Invalid idGroomingType"); ;
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


        private List<DGroomerType> GetAll()
        {
            try
            {
                var query = string.Concat("SELECT idGroomingType, `Name` FROM GroomingType");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 2)) return null;
                
                var allGroomingTypes = new List<DGroomerType>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allGroomingTypes.Add(new DGroomerType(
                        int.Parse(result.Rows[i]["idGroomingType"].ToString()),
                        result.Rows[i]["Name"].ToString()
                        ));
                }
                return allGroomingTypes;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private DGroomerType GetById(int idGroomingType)
        {
            return new DGroomerType(3, "Delux grooming"); // Dummy Data
        }



    }
}
