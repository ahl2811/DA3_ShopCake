using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.db;
namespace DA3_ShopCake.ViewModels
{
    class CakeScreenVM
    {
        public List<Cake> cakeList = new List<Cake>();
        CakeScreenVM() { }
        public CakeScreenVM(int catType)
        {
            var cakeDao = new CakeDaoImp();
            cakeList = cakeDao.GetCakesByType(catType);
        }
    }



}
