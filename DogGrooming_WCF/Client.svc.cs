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
        public string AuthenticateClient(string clientEmail, string clientPassword)
        {
            return Authenticate(clientEmail, clientPassword) ? "Success" : "Fail";
        }

        public string CreateClient(string firstname, string surname, string email, string password,
            string homeAddress, string mobilePh, string workPhone, string homePhone)
        {
            if (int.TryParse(mobilePh, out int mPh))
            {
                if (int.TryParse(workPhone, out int wPh))
                {
                    if (int.TryParse(homePhone, out int hPh))
                    {
                        int idClient = Create(firstname, surname, email, password, homeAddress, mPh, wPh, hPh);
                        if (idClient > 0) return "Success: " + idClient;
                        else throw new FaultException<string>("User already exist", "User already exist");
                    }
                    else { throw new FaultException<string>("Invalid homePhone", "Invalid homePhone"); }
                }
                else { throw new FaultException<string>("Invalid workPhone", "Invalid workPhone"); }
            }
            else { throw new FaultException<string>("Invalid mobilePh", "Invalid mobilePh"); }
        }

        public string DeleteClient(string idClient)
        {
            if (int.TryParse(idClient, out int id))
            {
                if (Delete(id)) return "Success";
                else throw new FaultException<string>("Cannot delete client", "Cannot delete client");
            }
            else { throw new FaultException<string>("Invalid idClient", "Invalid idClient"); }
        }

        public Dictionary<string, string> GetClientById(string idClient)
        {
            if (int.TryParse(idClient, out int id)) return GetById(id);
            else throw new FaultException<string>("Cannot find client", "Cannot find client");
        }

        public List<Dictionary<string, string>> GetClientList()
        {
            return GetAll();
        }

        public string UpdateClient(string idClient, string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone)
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
                            return "Success";
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


        private bool Authenticate(string email, string password)
        {
            return true;
        }

        private int Create(string firstname, string surname, string email, string password, string homeAddress, int mobilePh, int workPhone, int homePhone)
        {
            // Insert into database
            // Retreve idClient and send it back
            return 1;
        }

        private bool Delete(int idClient)
        {
            // Delete client and appointments from the database
            return true;
        }

        private Dictionary<string, string> GetById(int idClient)
        {
            // Retreve idGroomer
            // Put information into a dictionary
            var clientDetails = new Dictionary<string, string>();
            clientDetails.Add("idClient", "1");
            clientDetails.Add("FirstName", "Dog");
            clientDetails.Add("Surname", "Lover");
            clientDetails.Add("Email", "doglover@gmail.com");
            clientDetails.Add("HomeAddress", "12 Puppy Street, Carlton, Melbourne");
            clientDetails.Add("MobilePh", "1234567890");
            clientDetails.Add("WorkPh", "");
            clientDetails.Add("HomePh", "");

            // Send dictionary back
            return clientDetails;
        }


        private List<Dictionary<string, string>> GetAll()
        {
            // Retrieve all clients
            var allClients = new List<Dictionary<string, string>>();
            // Put information in a list of dictionary

            var clientDetails = new Dictionary<string, string>();
            clientDetails.Add("idClient", "1");
            clientDetails.Add("FirstName", "Dog");
            clientDetails.Add("Surname", "Lover");
            clientDetails.Add("Email", "doglover@gmail.com");
            clientDetails.Add("HomeAddress", "12 Puppy Street, Carlton, Melbourne");
            clientDetails.Add("MobilePh", "1234567890");
            clientDetails.Add("WorkPh", "");
            clientDetails.Add("HomePh", "");
            allClients.Add(clientDetails);

            // Send list of dictionary back
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
