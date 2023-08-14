using Common.Repositories;
using Common.Repositories.Interfaces;
using MicrocontrollersInfo.Repositories.Interfaces;
using MicrocontrollersInfo.Entity;
using System;
using System.Linq;

namespace MicrocontrollersInfo.Repositories
{
    public class FileBasedUnitOfWork : UnitOfWork, IInfoUnitOfWork
    {
        protected readonly DataContext dataContext;

        private IRepository<HousingType> housingTypesRepository;
        private IRepository<Microcontrollers> microcontrollerRepository;

        public IRepository<HousingType> HousingTypesRepository => housingTypesRepository;

        public IRepository<Microcontrollers> MicrocontrollersRepository { get => microcontrollerRepository; }
        public FileBasedUnitOfWork(DataContext dataContext)        {
            microcontrollerRepository = new AutoincrementalRepository <Microcontrollers>(dataContext.Microcontrollers);
            housingTypesRepository = new CascadingDeletionRepository<HousingType>(dataContext.HousingTypes, DeleteMicrocontrollersByHousingType);

            this.dataContext = dataContext;

            void DeleteMicrocontrollersByHousingType(HousingType housingType)
            {
                foreach(var e in microcontrollerRepository.GetAll().Where(e => e.housingType == housingType).ToArray())
                {
                    microcontrollerRepository.Delete(e);
                }
            }
        }

        public override void Load()
        {
            dataContext.Load();
        }

        public override void Save()
        {
            dataContext.Save();
        }

        protected override void DoDispose()
        {
            //throw new NotImplementedException();
        }
    }
}