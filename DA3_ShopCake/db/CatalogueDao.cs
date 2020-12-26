using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.db
{
    interface CatalogueDao
    {
        List<Catalogue> GetCatalogues();
        void insertCatalogue(Catalogue catalogue);
        void deleteCatalogue(Catalogue catalogue);
        void updateCatalogue(Catalogue catalogue);
    }
}
