using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    public class Breed : IBreed
    {
        public DSuccess CreateBreed(string name)
        {
            int idBreed = Create(name);
            if (idBreed > 0) return new DSuccess(idBreed);
            else throw new FaultException<string>("Breed already exist", "Breed already exist");
        }

        public DSuccess DeleteBreed(string idBreed)
        {
            if (int.TryParse(idBreed, out int id))
            {
                if (Delete(id)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot delete breed", "Cannot delete breed");
            }
            else { throw new FaultException<string>("Invalid idBreed", "Invalid idBreed"); }
        }

        public List<DBreed> GetBreedList()
        {
            return GetAll();
        }

        public DBreed GetBreedById(string idBreed)
        {
            if (int.TryParse(idBreed, out int id)) return GetById(id);
            else throw new FaultException<string>("Cannot find breed", "Cannot find breed");
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

        private bool Delete(int idBreed)
        {
            // Delete breed and all dogs of this breed from the database
            return true;
        }


        private List<DBreed> GetAll()
        {
            try
            {
                var query = string.Concat("SELECT idBreed, `Name` FROM Breed");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 2)) return null;

                var allGroomingTypes = new List<DBreed>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allGroomingTypes.Add(new DBreed(
                        int.Parse(result.Rows[i]["idBreed"].ToString()),
                        result.Rows[i]["Name"].ToString()
                        ));
                }
                return allGroomingTypes;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private DBreed GetById(int idBreed)
        {
            try
            {
                var query = string.Concat("SELECT idBreed, `Name` FROM Breed WHERE idBreed = " + idBreed);
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 2)) return null;
                return new DBreed(
                        int.Parse(result.Rows[0]["idBreed"].ToString()),
                        result.Rows[0]["Name"].ToString()
                        );
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

    }
}
