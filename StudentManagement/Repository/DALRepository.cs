using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentManagement.Repository
{
    public class DALRepository : IDisposable
    {

        SqlConnection sqlConnection = null;
        System.Data.SqlClient.SqlTransaction sqlTransaction = null;
        bool IsSqlTransactionUse = false;
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[Common.ConnectionStringKey].ConnectionString;
        }
        public void OpenConnection(bool SqlTransactionUse)
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(GetConnectionString());
                sqlConnection.Open();
                IsSqlTransactionUse = SqlTransactionUse;
                if (IsSqlTransactionUse)
                {
                    sqlTransaction = sqlConnection.BeginTransaction();
                }
            }
        }
        public void CloseConnection()
        {
            if (sqlConnection != null)
            {
                if (IsSqlTransactionUse)
                {
                    if (sqlTransaction != null)
                    {
                        try
                        {
                            sqlTransaction.Commit();
                        }
                        catch (Exception)
                        {
                        }
                        sqlTransaction = null;
                    }
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }
        void IDisposable.Dispose()
        {
            CloseConnection();
            GC.SuppressFinalize(this);
        }
        public void Rollback()
        {
            if (sqlConnection != null)
            {
                if (IsSqlTransactionUse)
                {
                    if (sqlTransaction != null)
                    {
                        try
                        {
                            sqlTransaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }
                        sqlTransaction = null;
                    }
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }
        public int ExecuteStatement(SqlCommand pObjCommand)
        {
            try
            {
                if (IsSqlTransactionUse)
                    pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                pObjCommand.CommandTimeout = Common.Sql_CommandTimeout;
                int Id = pObjCommand.ExecuteNonQuery();
                CloseConnection();
                return Id;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetData(SqlCommand pObjCommand)
        {
            try
            {
                if (IsSqlTransactionUse)
                    pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                pObjCommand.CommandTimeout = Common.Sql_CommandTimeout;
                DataTable dataTable = new DataTable();
                SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
                objAdapter.Fill(dataTable);
                objAdapter.Dispose();
                CloseConnection();
                return dataTable;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataSet GetDataSet(SqlCommand pObjCommand)
        {
            try
            {
                if (IsSqlTransactionUse)
                    pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                pObjCommand.CommandTimeout = Common.Sql_CommandTimeout;
                DataSet sataSet = new DataSet();
                SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
                objAdapter.Fill(sataSet);
                objAdapter.Dispose();
                CloseConnection();
                return sataSet;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }

}
