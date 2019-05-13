namespace DFShop.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using DFShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DontFretEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        

    }
}

    

