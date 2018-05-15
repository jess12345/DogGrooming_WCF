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
        public DSuccess CreateDog(string idClient, string name, string birthDate, string idBreed)
        {
            if (int.TryParse(idClient, out int idC))
            {
                if (DateTime.TryParse(birthDate, out DateTime birth))
                {
                    if (int.TryParse(idBreed, out int idB))
                    {
                        int idDog = Create(idC, name, birth, idB);
                        if (idDog > 0) return new DSuccess(idDog);
                        else throw new FaultException<string>("Dog already exist", "Dog already exist");
                    }
                    else { throw new FaultException<string>("Invalid idBreed", "Invalid idBreed"); }
                }
                else { throw new FaultException<string>("Invalid birthDate", "Invalid birthDate"); }
            }
            else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
        }

        public DSuccess DeleteDog(string idDog)
        {
            if (int.TryParse(idDog, out int id))
            {
                if (Delete(id)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot delete dog", "Cannot delete dog");
            }
            else { throw new FaultException<string>("Invalid idDog", "Invalid idDog"); }
        }

        public DDog GetDogById(string idDog)
        {
            if (int.TryParse(idDog, out int id)) return GetById(id);
            else throw new FaultException<string>("Invalid idDog", "Invalid idDog");
        }

        public List<DDog> GetDogByOwner(string idClient)
        {
            if (int.TryParse(idClient, out int id)) return GetByOwner(id);
            else throw new FaultException<string>("Invalid idClient", "Invalid idClient");
        }


        public List<DDog> GetDogList()
        {
            return GetAll();
        }

        public DSuccess UpdateDog(string idDog, string idClient, string name, string birthDate, string idBreed)
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
                                return new DSuccess(idD);
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

        private List<DDog> GetAll()
        {
            try
            {
                var query = "SELECT d.idDog, d.idClient, c.FirstName+' '+c.Surname ClientName, d.`Name` DogName, d.BirthDate, d.idBreed, b.`Name` BreedName FROM Dog d INNER JOIN Breed b ON b.idBreed = d.idBreed INNER JOIN `Client` c ON c.idClient = d.idClient";
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 4)) return null;

                var allDogs = new List<DDog>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allDogs.Add(new DDog(
                        int.Parse(result.Rows[i]["idDog"].ToString()),
                        int.Parse(result.Rows[i]["idClient"].ToString()),
                        result.Rows[i]["ClientName"].ToString(),
                        result.Rows[i]["DogName"].ToString(),
                        result.Rows[i]["BirthDate"].ToString(),            
                        int.Parse(result.Rows[i]["idBreed"].ToString()),
                        result.Rows[i]["BreedName"].ToString()
                        ));
                }
                return allDogs;
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }


        private DDog GetById(int idDog)
        {
            return new DDog(1, 1, "Tom Groomer", "Happy", "2016-08-24", 1, "Dog"); // Dummy Data
        }


        private List<DDog> GetByOwner(int idClient)
        {
            try
            {
                var query = string.Concat("SELECT d.idDog, d.idClient, c.FirstName+' '+c.Surname ClientName, d.`Name` DogName, d.BirthDate, d.idBreed, b.`Name` BreedName FROM Dog d INNER JOIN Breed b ON b.idBreed = d.idBreed INNER JOIN `Client` c ON c.idClient = d.idClient WHERE d.idClient = ", idClient);
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count != 4)) return null;
                
                var allDogs = new List<DDog>();
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    allDogs.Add(new DDog(
                       int.Parse(result.Rows[i]["idDog"].ToString()),
                       int.Parse(result.Rows[i]["idClient"].ToString()),
                       result.Rows[i]["ClientName"].ToString(),
                       result.Rows[i]["DogName"].ToString(),
                       result.Rows[i]["BirthDate"].ToString(),
                       int.Parse(result.Rows[i]["idBreed"].ToString()),
                       result.Rows[i]["BreedName"].ToString()
                       ));
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
