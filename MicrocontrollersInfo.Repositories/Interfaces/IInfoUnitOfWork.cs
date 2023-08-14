using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repositories.Interfaces;
using MicrocontrollersInfo.Entity;

namespace MicrocontrollersInfo.Repositories.Interfaces{
     public interface IInfoUnitOfWork : IUnitOfWork{
        IRepository<HousingType> HousingTypesRepository { get; }
        IRepository<Microcontrollers> MicrocontrollersRepository { get; }
     }
}
