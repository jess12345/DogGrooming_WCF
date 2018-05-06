using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    public class Dog : IDog
    {
        public string CreateDog(string idClient, string name, string birthDate, string idBreed)
        {
            if (int.TryParse(idClient, out int idC))
            {
                if (DateTime.TryParse(birthDate, out DateTime birth))
                {
                    if (int.TryParse(idBreed, out int idB))
                    {
                        int idDog = Create(idC, name, birth, idB);
                        if (idDog > 0) return "Success: " + idDog;
                        else return "Fail: Dog already exist";
                    }
                    else { return "Fail: Invalid idBreed"; }
                }
                else { return "Fail: Invalid birthDate"; }
            }
            else { return "Fail: Invalid idClient"; }
        }

        public string DeleteDog(string idDog)
        {
            if (int.TryParse(idDog, out int id))
            {
                if (Delete(id)) return "Success";
                else return "Fail: Cannot delete dog";
            }
            else { return "Fail: Invalid idDog"; }
        }

        public Dictionary<string, string> GetDogById(string idDog)
        {
            if (int.TryParse(idDog, out int id)) return GetById(id);
            else return null;
        }

        public List<Dictionary<string, string>> GetDogList()
        {
            return GetAll();
        }

        public string UpdateDog(string idDog, string idClient, string name, string birthDate, string idBreed)
        {
            if (int.TryParse(idDog, out int idD))
            {
                if (int.TryParse(idClient, out int idC))
                {
                    if (DateTime.TryParse(birthDate, out DateTime birth))
                    {
                        if (int.TryParse(idBreed, out int idB))
                        {
                            if (Update(idD, idC, name, birth, idB))
                                return "Success";
                            else return "Fail: Cannot update dog";
                        }
                        else { return "Fail: Invalid idBreed"; }
                    }
                    else { return "Fail: Invalid birthDate"; }
                }
                else { return "Fail: Invalid idClient"; }
            }
            else { return "Fail: Invalid idDog"; }
        }




        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//

        private int Create(int idClient, string name, DateTime birthDate, int idBreed)
        {
            // Insert into database
            // Retreve idDog and send it back
            return 1;
        }

        private bool Delete(int idDog)
        {
            // Delete dogs and appointments from the database
            return true;
        }

        private List<Dictionary<string, string>> GetAll()
        {
            // Retrieve all breeds
            var allDog = new List<Dictionary<string, string>>();
            // Put information in a list of dictionary

            var dogDetails = new Dictionary<string, string>();
            dogDetails.Add("idDog", "1");
            dogDetails.Add("idClient", "1");
            dogDetails.Add("Name", "Happy");
            dogDetails.Add("BirthDate", "2016-08-19");
            dogDetails.Add("idBreed", "1");
            allDog.Add(dogDetails);


            // Send list of dictionary back
            return allDog;
        }


        private Dictionary<string, string> GetById(int idDog)
        {
            // Retreve idDog
            // Put information into a dictionary
            var dogDetails = new Dictionary<string, string>();
            dogDetails.Add("idDog", "1");
            dogDetails.Add("idClient", "1");
            dogDetails.Add("Name", "Happy");
            dogDetails.Add("BirthDate", "2016-08-19");
            dogDetails.Add("idBreed", "1");

            // Send dictionary back
            return dogDetails;
        }


        private bool Update(int idDog, int idClient, string name, DateTime birthDate, int idBreed)
        {
            // Update dog details
            // Sent status
            return true;
        }

    }
}
