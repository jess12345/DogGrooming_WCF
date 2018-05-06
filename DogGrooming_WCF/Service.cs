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
        [WebGet(UriTemplate="ViewAll")]
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
        List<Dictionary<string, string>> GetAppointmentList();

        [OperationContract]
        Dictionary<string, string> GetAppointmentById(string idGroomer, string idDog, string startTime);

        [OperationContract]
        int CreateAppointment(Dictionary<string, string> appointmentDetails);

        [OperationContract]
        bool DeleteAppointment(string idGroomer, string idDog, string startTime);
    }


    [ServiceContract]
    public interface IDog
    {
        [OperationContract]
        List<Dictionary<string, string>> GetDogList();

        [OperationContract]
        Dictionary<string, string> GetDogById(string idDog);

        [OperationContract]
        int CreateDog(Dictionary<string, string> dogDetails);

        [OperationContract]
        bool UpdateDog(Dictionary<string, string> dogDetailsAndId);

        [OperationContract]
        bool DeleteDog(string idDog);
    }

    [ServiceContract]
    public interface IClient
    {
        [OperationContract]
        List<Dictionary<string, string>> GetClientList();

        [OperationContract]
        Dictionary<string, string> GetClientById(string idClient);

        [OperationContract]
        int CreateClient(Dictionary<string, string> clientDetails);

        [OperationContract]
        bool UpdateClient(Dictionary<string, string> clientDetailsAndId);

        [OperationContract]
        bool DeleteClient(string idClient);

        [OperationContract]
        bool AuthenticateClient(Dictionary<string, string> clientEmailAndPassword);
    }


    [ServiceContract]
    public interface IBreed
    {
        [OperationContract]
        List<Dictionary<string, string>> GeBreedList();

        [OperationContract]
        Dictionary<string, string> GetBreedById(string idBreed);

        [OperationContract]
        int CreateBreed(Dictionary<string, string> breedDetails);

        [OperationContract]
        bool DeleteBreed(string idDog);
    }


    [ServiceContract]
    public interface IGroomingType
    {
        [OperationContract]
        List<Dictionary<string, string>> GeGroomingTypeList();

        [OperationContract]
        Dictionary<string, string> GetGroomingById(string idGroomingType);

        [OperationContract]
        int CreateGroomingType(Dictionary<string, string> groomingTypeDetails);

        [OperationContract]
        bool DeleteGroomingType(string idGroomingType);
    }

}
