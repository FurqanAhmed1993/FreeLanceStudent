using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManagement.Repository
{
    public  class BankRepository:DALRepository
    {
        public DataTable Crud_BankMaster(int OperationId = Common.Select, int BankId = 0, string BankName = null
            , string AccountName=null,string BSBNumber=null,string AccountNumber=null, bool IsActive = true, int UserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_BankMaster"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
                    cmd.Parameters.Add("@BankName", SqlDbType.VarChar).Value = BankName;
                    cmd.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = AccountName;
                    cmd.Parameters.Add("@BSBNumber", SqlDbType.VarChar).Value = BSBNumber;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Crud_Student : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}