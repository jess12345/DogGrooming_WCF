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
                        else throw new FaultException<string>("Dog already exist", "Dog already exist");
                    }
                    else { throw new FaultException<string>("Invalid idBreed", "Invalid idBreed"); }
                }
                else { throw new FaultException<string>("Invalid birthDate", "Invalid birthDate"); }
            }
            else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
        }

        public string DeleteDog(string idDog)
        {
            if (int.TryParse(idDog, out int id))
            {
                if (Delete(id)) return "Success";
                else throw new FaultException<string>("Cannot delete dog", "Cannot delete dog");
            }
            else { throw new FaultException<string>("Invalid idDog", "Invalid idDog"); }
        }

        public Dictionary<string, string> GetDogById(string idDog)
        {
            if (int.TryParse(idDog, out int id)) return GetById(id);
            else throw new FaultException<string>("Invalid idDog", "Invalid idDog");
        }

        public List<Dictionary<string, string>> GetDogByOwner(string idClient)
        {
            if (int.TryParse(idClient, out int id)) return GetByOwner(id);
            else throw new FaultException<string>("Invalid idClient", "Invalid idClient");
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
                            else throw new FaultException<string>("Cannot update dog", "Cannot update dog");
                        }
                        else { throw new FaultException<string>("Invalid idBreed", "Invalid idBreed"); }
                    }
                    else { throw new FaultException<string>("Invalid birthDate", "Invalid birthDate"); }
                }
                else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
            }
            else { throw new FaultException<string>("Invalid idDog", "Invalid idDog"); }
        }




        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//

        private int Create(int idClient, string name, DateTime birthDate, int idBreed)
        {
            try
            {
                var query = string.Concat("INSERT INTO Dog (idClient,`Name`,BirthDate,idBreed) VALUES (",idClient,",'", name, "', '", birthDate.ToString("yyyy-MM-dd HH:mm:ss"), "',", idBreed, "); SELECT LAST_INSERT_ID(); ");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count < 1)) throw new FaultException<string>("Could not create dog", "Could not create dog");
                if (int.TryParse(result.Rows[0][0].ToString(), out int idDog)){
                    return idDog;
                }
                else { throw new FaultException<string>("Could not create dog", "Could not create dog"); }
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private bool Delete(int idDog)
        {
            try
            {
                var result = MySqlDatabase.RunQuery("DELETE FROM Dog WHERE idDog = "+idDog);
            }
            catch (Exception e) {}
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


        private List<Dictionary<string,string>> GetByOwner(int idClient)
        {
            try
            {
                var query = string.Concat("SELECT d.idDog, d.`Name` DogName, d.BirthDate, b.`Name` Breed FROM Dog d INNER JOIN Breed b ON d.idBreed = b.idBreed WHERE idClient = ", idClient);
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 4)) return null;
                
                var allDogs = new List<Dictionary<string, string>>();
                Dictionary<string, string> dogDetails;
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    dogDetails = new Dictionary<string, string>
                    {
                        { "idDog", result.Rows[i]["idDog"].ToString() },
                        { "DogName", result.Rows[i]["DogName"].ToString() },
                        { "BirthDate", result.Rows[i]["BirthDate"].ToString() },
                        { "Breed", result.Rows[i]["Breed"].ToString() }
                    };
                    allDogs.Add(dogDetails);
                }
                
                return allDogs;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }


        private bool Update(int idDog, int idClient, string name, DateTime birthDate, int idBreed)
        {
            // Update dog details
            // Sent status
            return true;
        }

    }
}
