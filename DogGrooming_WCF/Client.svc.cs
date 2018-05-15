﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DogGrooming_WCF
{
    public class Client : IClient
    {
        public Dictionary<string, string> AuthenticateClient(string clientEmail, string clientPassword)
        {
            return Authenticate(clientEmail, clientPassword);
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


        private Dictionary<string,string> Authenticate(string email, string password)
        {
            try
            {
                var query = string.Concat("SELECT idClient, FirstName, Surname, Email, HomeAddress, MobilePh, IFNULL(WorkPh,0) WorkPh, IFNULL(HomePh,0) HomePh FROM `CLIENT` WHERE Email='",email,"' AND `Password`='", password, "'");
                var result = MySqlDatabase.RunQuery(query);
                if (result == null) throw new FaultException<string>("User does not exist", "User does not exist");
                if ((result.Rows.Count != 1) & (result.Columns.Count != 8)) return null;

                var userDetails = new Dictionary<string, string>
                {
                    { "idClient", result.Rows[0]["idClient"].ToString() },
                    { "FirstName", result.Rows[0]["FirstName"].ToString() },
                    { "Surname", result.Rows[0]["Surname"].ToString() },
                    { "Email", result.Rows[0]["Email"].ToString() },
                    { "HomeAddress", result.Rows[0]["HomeAddress"].ToString() },
                    { "MobilePh", result.Rows[0]["MobilePh"].ToString() },
                    { "WorkPh", result.Rows[0]["WorkPh"].ToString() },
                    { "HomePh", result.Rows[0]["HomePh"].ToString() }
                };
                return userDetails;
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
