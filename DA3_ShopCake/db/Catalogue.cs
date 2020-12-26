﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 CATALOGUE: ID (PK), CATALOGUE _NAME (NVARCHAR(50))
 */
namespace ConsoleApp2.db
{
    class Catalogue
    {
        public String Id { get; set; }
        public String CatalogueName { get; set; }

        public Catalogue(String newId, String newCatalogueName)
        {
            this.Id = newId;
            this.CatalogueName = newCatalogueName;
        }

        public Catalogue()
        {
            //do nothing
        }
    }
}
