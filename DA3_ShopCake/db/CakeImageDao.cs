using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA3_ShopCake.db
{
    interface CakeImageDao
    {
        List<CakeImage> GetCakeImages();
        void insertCakeImage(CakeImage cakeImage);
        void deleteCakeImage(CakeImage cakeImage);
    }
}
