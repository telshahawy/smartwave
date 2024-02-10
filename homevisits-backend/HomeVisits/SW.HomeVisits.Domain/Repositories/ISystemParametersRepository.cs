using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Repositories
{
   public interface ISystemParametersRepository: IDisposableRepository
    {
       void PresistNewSystemParameter(SystemParameter systemParameter);
       void UpdateSystemParameter(SystemParameter systemParameter);
        //void GetLatestSystemParamter();
        List<SystemParameter> GetSystemParameter();
        SystemParameter GetSystemParameterByClientId(Guid ClientId);
        //SystemParameter GetSystemParameterById(Guid systemParameterId);
    }
}
