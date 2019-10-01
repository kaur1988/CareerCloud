using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.WCF
{
    [ServiceContract]
    public interface ISystem
    {

        [OperationContract]
        void AddSystemCountryCode(SystemCountryCodePoco[] items);

        [OperationContract]
        void RemoveSystemCountryCode(SystemCountryCodePoco[] items);

        [OperationContract]
        void UpdateSystemCountryCode(SystemCountryCodePoco[] items);

        [OperationContract]
        List<SystemCountryCodePoco> GetAllSystemCountryCode();

        [OperationContract]
        SystemCountryCodePoco GetSingleSystemCountryCode(string id);


        [OperationContract]
        void AddSystemLanguageCode(SystemLanguageCodePoco[] items);

        [OperationContract]
        void RemoveSystemLanguageCode(SystemLanguageCodePoco[] items);

        [OperationContract]
        void UpdateSystemLanguageCode(SystemLanguageCodePoco[] items);

        [OperationContract]
        List<SystemLanguageCodePoco> GetAllSystemLanguageCode();

        [OperationContract]
        SystemLanguageCodePoco GetSingleSystemLanguageCode(string id);
    }
}
