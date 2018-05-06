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
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetGroomerList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idGroomer}")]
        Dictionary<string, string> GetGroomerById(string idGroomer);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{firstName}/{surname}/{email}/{password}")]
        string CreateGroomer(string firstname, string surname, string email, string password);

        [OperationContract]
        [WebGet(UriTemplate = "Update/{idGroomer}/{firstName}/{surname}/{email}/{password}")]
        string UpdateGroomer(string idGroomer, string firstname, string surname, string email, string password);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idGroomer}")]
        string DeleteGroomer(string idGroomer);

        [OperationContract]
        [WebGet(UriTemplate = "Authenticate/{groomerEmail}/{groomerPassword}")]
        string AuthenticateGroomer(string groomerEmail, string groomerPassword);
    }


    [ServiceContract]
    public interface IAppointment
    {
        [OperationContract]
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetAppointmentList();

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
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetDogList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idDog}")]
        Dictionary<string, string> GetDogById(string idDog);

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
        [WebGet(UriTemplate = "ViewAll")]
        List<Dictionary<string, string>> GetClientList();

        [OperationContract]
        [WebGet(UriTemplate = "View/{idClient}")]
        Dictionary<string, string> GetClientById(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "Add/{firstname}/{surname}/{email}/{password}/{homeAddress}/{mobilePh}/{workPhone}/{homePhone}")]
        string CreateClient(string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone);

        [OperationContract]
        [WebGet(UriTemplate= "Update/{idClient}/{firstname}/{surname}/{email}/{password}/{homeAddress}/{mobilePh}/{workPhone}/{homePhone}")]
        string UpdateClient(string idClient, string firstname, string surname, string email, string password, string homeAddress, string mobilePh, string workPhone, string homePhone);

        [OperationContract]
        [WebGet(UriTemplate = "Delete/{idClient}")]
        string DeleteClient(string idClient);

        [OperationContract]
        [WebGet(UriTemplate = "Authenticate/{clientEmail}/{clientPassword}")]
        string AuthenticateClient(string clientEmail, string clientPassword);
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

}
