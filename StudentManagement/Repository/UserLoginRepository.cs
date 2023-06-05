using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManagement.Repository
{
    public class UserLoginRepository:DALRepository
    {
        public DataTable Crud_UserLogin(int OperationId = 1, int UserId = 0, string UserName = null, string LoginId = null, string LoginPassword = null,string EmailAddress=null, bool IsActive = true, int LoginUserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_UserLogin"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar).Value = LoginId;
                    cmd.Parameters.Add("@LoginPassword", SqlDbType.VarChar).Value = LoginPassword;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = EmailAddress;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@LoginUserId", SqlDbType.Int).Value = LoginUserId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Crud_UserLogin : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}