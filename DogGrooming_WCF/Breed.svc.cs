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
        public string CreateBreed(string name)
        {
            int idBreed = Create(name);
            if (idBreed > 0) return "Success: " + idBreed;
            else return "Fail: Breed already exist";
        }

        public string DeleteBreed(string idBreed)
        {
            if (int.TryParse(idBreed, out int id))
            {
                if (Delete(id)) return "Success";
                else return "Fail: Cannot delete breed";
            }
            else { return "Fail: Invalid idBreed"; }
        }

        public List<Dictionary<string, string>> GetBreedList()
        {
            return GetAll();
        }

        public Dictionary<string, string> GetBreedById(string idBreed)
        {
            if (int.TryParse(idBreed, out int id)) return GetById(id);
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

        private bool Delete(int idBreed)
        {
            // Delete breed and all dogs of this breed from the database
            return true;
        }


        private List<Dictionary<string, string>> GetAll()
        {
            // Retrieve all breeds
            var allBreed = new List<Dictionary<string, string>>();
            // Put information in a list of dictionary

            var breedDetails = new Dictionary<string, string>();
            breedDetails.Add("idBreed", "1");
            breedDetails.Add("Name", "Retriever (Labrador)");
            allBreed.Add(breedDetails);

            breedDetails = new Dictionary<string, string>();
            breedDetails.Add("idBreed", "2");
            breedDetails.Add("Name", "German Shepherd");
            allBreed.Add(breedDetails);

            breedDetails = new Dictionary<string, string>();
            breedDetails.Add("idBreed", "3");
            breedDetails.Add("Name", "French Bulldog");
            allBreed.Add(breedDetails);

            // Send list of dictionary back
            return allBreed;
        }

        private Dictionary<string, string> GetById(int idBreed)
        {
            // Retreve idBreed
            // Put information into a dictionary
            var breedDetails = new Dictionary<string, string>();
            breedDetails.Add("idBreed", "1");
            breedDetails.Add("Name", "Retriever (Labrador)");

            // Send dictionary back
            return breedDetails;
        }

    }
}
