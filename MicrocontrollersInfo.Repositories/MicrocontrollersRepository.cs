//using Common.Repositories;
//using MicrocontrollersInfo.Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MicrocontrollersInfo.Repositories
//{
//    public class MicrocontrollersRepository : Repository<Microcontroller>
//    {
//        public MicrocontrollersRepository(ICollection<Microcontroller> collection) : base(collection)
//        {
//        }

//        public override void Add(Microcontroller microcontroller)
//        {
//            if (GetAll().Any())
//            {
//                microcontroller.Id = GetAll().Select(e => e.Id).Max() + 1;
//            }
//            else
//            {
//                microcontroller.Id = 1;
//            }
//            base.Add(microcontroller);
//        }
//    }
//}
