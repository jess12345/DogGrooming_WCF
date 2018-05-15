using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    public class Client : IClient
    {
        public DClient AuthenticateClient(string clientEmail, string clientPassword)
        {
            return Authenticate(clientEmail, clientPassword);
        }

        public DSuccess CreateClient(string firstname, string surname, string email, string password,
            string homeAddress, string mobilePh, string workPhone, string homePhone)
        {
            if (int.TryParse(mobilePh, out int mPh))
            {
                if (int.TryParse(workPhone, out int wPh))
                {
                    if (int.TryParse(homePhone, out int hPh))
                    {
                        int idClient = Create(firstname, surname, email, password, homeAddress, mPh, wPh, hPh);
                        if (idClient > 0) return new DSuccess(idClient);
                        else throw new FaultException<string>("User already exist", "User already exist");
                    }
                    else { throw new FaultException<string>("Invalid homePhone", "Invalid homePhone"); }
                }
                else { throw new FaultException<string>("Invalid workPhone", "Invalid workPhone"); }
            }
            else { throw new FaultException<string>("Invalid mobilePh", "Invalid mobilePh"); }
        }

        public DSuccess DeleteClient(string idClient)
        {
            if (int.TryParse(idClient, out int id))
            {
                if (Delete(id)) return new DSuccess(id);
                else throw new FaultException<string>("Cannot delete client", "Cannot delete client");
            }
            else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
        }

        public DClient GetClientById(string idClient)
        {
            if (int.TryParse(idClient, out int id)) return GetById(id);
            else throw new FaultException<string>("Cannot find client", "Cannot find client");
        }

        public List<DClient> GetClientList()
        {
            return GetAll();
        }

        public DSuccess UpdateClient(string idClient, string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone)
        {
            if (int.TryParse(idClient, out int id))
            {
                if (int.TryParse(mobilePh, out int mPh))
                {
                    if (int.TryParse(workPhone, out int wPh))
                    {
                        if (int.TryParse(homePhone, out int hPh))
                        {
                            if (Update(id, firstname, surname, 
                                email, password, homeAddress,
                                mPh, wPh, hPh)) 
                            return new DSuccess(id);
                            else throw new FaultException<string>("Cannot update client", "Cannot update client");
                        }
                        else { throw new FaultException<string>("Invalid homePhone", "Invalid homePhone"); }
                    }
                    else { throw new FaultException<string>("Invalid workPhone", "Invalid workPhone"); }
                }
                else { throw new FaultException<string>("Invalid mobilePh", "Invalid mobilePh"); }
            }
            else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
        }




        //====================================================//
        //======================= LOGIC ======================//
        //====================================================//


        private DClient Authenticate(string email, string password)
        {
            try
            {
                var query = string.Concat("SELECT idClient, FirstName, Surname, Email, HomeAddress, MobilePh, IFNULL(WorkPh,0) WorkPh, IFNULL(HomePh,0) HomePh FROM `CLIENT` WHERE Email='",email,"' AND `Password`='", password, "'");
                var result = MySqlDatabase.RunQuery(query);
                if (result == null) throw new FaultException<string>("User does not exist", "User does not exist");
                if ((result.Rows.Count != 1) & (result.Columns.Count != 8)) return null;

                var client = new DClient(
                    int.Parse(result.Rows[0]["idClient"].ToString()),
                    result.Rows[0]["FirstName"].ToString(),
                    result.Rows[0]["Surname"].ToString(),
                    result.Rows[0]["Email"].ToString(),
                    result.Rows[0]["HomeAddress"].ToString(),
                    int.Parse(result.Rows[0]["MobilePh"].ToString()),
                    int.Parse(result.Rows[0]["WorkPh"].ToString()),
                    int.Parse(result.Rows[0]["HomePh"].ToString())
                    );
                
                return client;
            }
            catch (Exception e) { throw new FaultException<string>("User does not exist", "User does not exist"); }
        }

        private int Create(string firstname, string surname, string email, string password, string homeAddress, int mobilePh, int workPhone, int homePhone)
        {
            try
            {
                var query = string.Concat("INSERT INTO `client`(`FirstName`,`Surname`,`Email`,`Password`,`HomeAddress`,`MobilePh`,`WorkPh`,`HomePh`)VALUES('",firstname,"','",surname,"','",email,"','",password,"','", homeAddress, "',",mobilePh,",",workPhone,",",homePhone,");SELECT LAST_INSERT_ID();");
                var result = MySqlDatabase.RunQuery(query);
                if ((result.Rows.Count < 1) & (result.Columns.Count < 1)) throw new FaultException<string>("Could not create client", "Could not create client");
                if (int.TryParse(result.Rows[0][0].ToString(), out int idClient))
                {
                    return idClient;
                }
                else { throw new FaultException<string>("Could not create client", "Could not create client"); }
            }
            catch (Exception e) { throw new FaultException<string>(e.Message, e.Message); }
        }

        private bool Delete(int idClient)
        {
            // Delete client and appointments from the database
            return true;
        }

        private DClient GetById(int idClient)
        {
            return new DClient(1,"Dog", "Lover", "doglover@gmail.com", "12 Puppy Street, Carlton, Melbourne", 1234567890,0,0); // Dummy Data
        }
        
        private List<DClient> GetAll()
        {
            var allClients = new List<DClient>();
            allClients.Add(new DClient(1, "Dog", "Lover", "doglover@gmail.com", "12 Puppy Street, Carlton, Melbourne", 1234567890, 0, 0)); // Dummy Data
            return allClients;
        }


        private bool Update(int idClient, string firstname, string surname, string email, string password, string homeAddress, int mobilePh, int workPhone, int homePhone)
        {
            // Update client details
            // Sent status
            return true;
        }

    }
}
