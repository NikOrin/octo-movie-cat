using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Service.Common
{
    public static class ConfigSettings
    {
        public static string DatabaseServer { get; set; }
        public static string DatabaseName { get; set; }

        public static string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder();

                builder.DataSource = DatabaseServer;
                builder.InitialCatalog = DatabaseName;
                builder.IntegratedSecurity = true;

                return builder.ToString();
            }
        }
    }
}
