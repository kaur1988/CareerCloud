using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    
        public class SystemLanguageCodeLogic : NewBaseLogic<SystemLanguageCodePoco>
        {
            public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) : base(repository)
            {

            }

            public override void Add(SystemLanguageCodePoco[] pocos)
            {
                Verify(pocos);
                base.Add(pocos);
            }

        public virtual SystemLanguageCodePoco Get(string id)
        {
            return _repository.GetSingle(c => c.LanguageID == id);
        }

        public override void Update(SystemLanguageCodePoco[] pocos)
            {
                Verify(pocos);
                base.Update(pocos);
            }

            protected override void Verify(SystemLanguageCodePoco[] pocos)
            {
                List<ValidationException> exceptions = new List<ValidationException>();
                foreach (SystemLanguageCodePoco poco in pocos)
                {
                    if (string.IsNullOrEmpty(poco.LanguageID))
                    {
                        exceptions.Add(new ValidationException(1000, $"Code for SystemLanguageCode{poco.LanguageID} is required."));
                    }
                    if (string.IsNullOrEmpty(poco.Name))
                    {
                        exceptions.Add(new ValidationException(1001, $"Name for SystemLanguageCode{poco.Name} is required"));
                    }
                    if (string.IsNullOrEmpty(poco.NativeName))
                    {
                        exceptions.Add(new ValidationException(1002, $"Name for SystemLanguageCode{poco.NativeName} is required"));
                    }
                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }

}
