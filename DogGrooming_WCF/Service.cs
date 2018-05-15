using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DogGrooming_WCF
{
    [ServiceContract]
    public interface IGroomer
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll", ResponseFormat = WebMessageFormat.Json)]
        List<DGroomer> GetGroomerList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idGroomer}", ResponseFormat = WebMessageFormat.Json)]
        DGroomer GetGroomerById(string idGroomer);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{firstName}/{surname}/{email}/{password}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess CreateGroomer(string firstname, string surname, string email, string password);

        [OperationContract]
        [WebGet(UriTemplate = "Update/{idGroomer}/{firstName}/{surname}/{email}/{password}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess UpdateGroomer(string idGroomer, string firstname, string surname, string email, string password);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idGroomer}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess DeleteGroomer(string idGroomer);

        [OperationContract]
        [WebGet(UriTemplate = "Authenticate/{groomerEmail}/{groomerPassword}", ResponseFormat = WebMessageFormat.Json)]
        DGroomer AuthenticateGroomer(string groomerEmail, string groomerPassword);
    }


    [ServiceContract]
    public interface IAppointment
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetAppointmentList();

        [OperationContract]
        [WebGet(UriTemplate = "ViewAllClient/{idClient}")]
        List<Dictionary<string, string>> GetAppointmentsByClient(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "ViewAllGroomer/idGroomer")]
        List<Dictionary<string, string>> GetAppointmentByGroomer(string idGroomer);

        [OperationContract]
        [WebGet(UriTemplate = "View/{idGroomer}/{idDog}/{startTime}")]
        Dictionary<string, string> GetAppointmentById(string idGroomer, string idDog, string startTime);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{idGroomer}/{idDog}/{startTime}/{idGroomingType}/{duration}/{comments}")]
        string CreateAppointment(string idGroomer, string idDog, string startTime, string idGroomingType, string duration, string comments);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idGroomer}/{idDog}/{startTime}")]
        string DeleteAppointment(string idGroomer, string idDog, string startTime);
    }


    [ServiceContract]
    public interface IDog
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll", ResponseFormat = WebMessageFormat.Json)]
        List<Dictionary<string, string>> GetDogList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idDog}")]
        Dictionary<string, string> GetDogById(string idDog);

        [OperationContract]
        [WebGet(UriTemplate = "ViewAllClient/{idClient}")]
        List<Dictionary<string, string>> GetDogByOwner(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{idClient}/{name}/{birthDate}/{idBreed}")]
        string CreateDog(string idClient, string name, string birthDate, string idBreed);

        [OperationContract]
        [WebGet(UriTemplate = "Update/{idDog}/{idClient}/{name}/{birthDate}/{idBreed}")]
        string UpdateDog(string idDog, string idClient, string name, string birthDate, string idBreed);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idDog}")]
        string DeleteDog(string idDog);
    }

    [ServiceContract]
    public interface IClient
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll", ResponseFormat = WebMessageFormat.Json)]
        List<DClient> GetClientList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idClient}", ResponseFormat = WebMessageFormat.Json)]
        DClient GetClientById(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{firstname}/{surname}/{email}/{password}/{homeAddress}/{mobilePh}/{workPhone}/{homePhone}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess CreateClient(string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone);

        [OperationContract]
        [WebGet(UriTemplate= "Update/{idClient}/{firstname}/{surname}/{email}/{password}/{homeAddress}/{mobilePh}/{workPhone}/{homePhone}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess UpdateClient(string idClient, string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idClient}", ResponseFormat = WebMessageFormat.Json)]
        DSuccess DeleteClient(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "Authenticate/{clientEmail}/{clientPassword}", ResponseFormat = WebMessageFormat.Json)]
        DClient AuthenticateClient(string clientEmail, string clientPassword);
    }


    [ServiceContract]
    public interface IBreed
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetBreedList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idBreed}")]
        Dictionary<string, string> GetBreedById(string idBreed);

        [OperationContract]
        [WebGet(UriTemplate= "Add/{name}")]
        string CreateBreed( string name);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idBreed}")]
        string DeleteBreed(string idBreed);
    }


    [ServiceContract]
    public interface IGroomingType
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GeGroomingTypeList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idGroomingType}")]
        Dictionary<string, string> GetGroomingById(string idGroomingType);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{name}")]
        string CreateGroomingType(string name);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idGroomingType}")]
        string DeleteGroomingType(string idGroomingType);
    }





    [DataContract]
    public class DClient
    {
        [DataMember]
        public int IdClient { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string HomeAddress { get; set; }

        [DataMember]
        public int MobilePh { get; set; }

        [DataMember]
        public int WorkPh { get; set; }

        [DataMember]
        public int HomePh { get; set; }
        
        public DClient(int idClient, string firstname, string surname, string email, string homeAddress, int mobilePh, int workPh, int homePh)
        {
            IdClient = IdClient;
            FirstName = firstname;
            Surname = surname;
            Email = email;
            HomeAddress = homeAddress;
            MobilePh = mobilePh;
            WorkPh = workPh;
            HomePh = homePh;
        }
    }

    [DataContract]
    public class DGroomer
    {
        [DataMember]
        public int IdGroomer { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Email { get; set; }

        public DGroomer(int idGroomer, string firstname, string surname, string email)
        {
            IdGroomer = idGroomer;
            FirstName = firstname;
            Surname = surname;
            Email = email;
        }
    }

    [DataContract]
    public class DSuccess
    {
        [DataMember]
        public int Id { get; set; }

        public DSuccess(int id)
        {
            Id = id;
        }
    }


}
