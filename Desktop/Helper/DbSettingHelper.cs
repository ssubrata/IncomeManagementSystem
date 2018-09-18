
using DAL.Repository.Implement;
using System;
using System.Data.Entity.Core.EntityClient;

namespace Desktop.Helper
{
    public  static class DbSettingHelper
    {
        public static UnitOfWork UnitOfWork { get { return new UnitOfWork(new TruckDbContext(BuildConnectionString())); }  }

      
        public static String BuildConnectionString()
        {
                // Build the connection string from the provided datasource and database
                String connString = @"data source=" + Properties.Settings.Default.DataSource + ";initial catalog=" + Properties.Settings.Default.Database + ";user id=" + Properties.Settings.Default.UserId + ";password=" + Properties.Settings.Default.Password + ";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";
                // Build the MetaData... feel free to copy/paste it from the connection string in the config file.
                EntityConnectionStringBuilder esb = new EntityConnectionStringBuilder();
                esb.Metadata = "res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl";
                esb.Provider = "System.Data.SqlClient";
                esb.ProviderConnectionString = connString;

                // Generate the full string and return it
                return esb.ToString();
            
        }
        public static String RawBuildConnectionString()
        {
            String connString = @"data source=" + Properties.Settings.Default.DataSource + ";initial catalog=" + Properties.Settings.Default.Database + ";user id=" + Properties.Settings.Default.UserId + ";password=" + Properties.Settings.Default.Password + ";integrated security=True;";
          //  string connectionString = builder.DataSource + ";" + builder.InitialCatalog + ";" + builder.UserID+";"+builder.Password+";"+builder.IntegratedSecurity;
            return connString;
        }
    }
}
