﻿using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using Westwind.Utilities;

namespace DatabaseMigration.Core
{
    public class DbConnector
    {
        private readonly IDbProvider _dbProvider;
        private readonly string _connectionString;

        public DbConnector(IDbProvider dbProvider, string connectionString)
        {
            this._dbProvider = dbProvider;
            this._connectionString = connectionString;
        }

        public DbConnector(IDbProvider dbProvider, IConnectionBuilder connectionBuilder, ConnectionInfo connectionInfo)
        {
            this._dbProvider = dbProvider;
            this._connectionString = connectionBuilder.BuildConntionString(connectionInfo);
        }

        public DbConnection CreateConnection()
        {
            DbProviderFactory factory = null;

            if (this._dbProvider.ProviderName.ToLower().Contains("oracle"))
            {
                factory = new OracleClientFactory();                
            }
            else
            {
                factory = DataUtils.GetDbProviderFactory(this._dbProvider.ProviderName);
            }            
           
            DbConnection connection = factory.CreateConnection();
            if (connection != null)
            {
                connection.ConnectionString = this._connectionString;
                return connection;
            }
            else
            {
                return null;
            }
        }
    }
}
