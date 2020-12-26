using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.db
{
    interface CakeDao
    {
        List<Cake> GetCakes();
        void insertCake(Cake cake);
        void deleteCake(Cake cake);
        void updateCake(Cake cake);
    }
}
