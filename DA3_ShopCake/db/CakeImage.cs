using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*CAKEIMAGE: CAKE_ID (PK), IMAGE(PK)*/
namespace DA3_ShopCake.db
{
    class CakeImage
    {
        public String CakeId { get; set; }
        public String Image { get; set; }

        public CakeImage(string cakeId, string image)
        {
            CakeId = cakeId;
            Image = image;
        }

        public CakeImage()
        {
        }
    }
}
