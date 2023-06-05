using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManagement.Repository
{
    public class StudentRepository:DALRepository
    {
        public DataTable Crud_Student(int OperationId = Common.Select, int StudentId=0,string FirstName = null, string LastName = null
            , string ParentName = null, int? TermId=null,int? SemesterId=null,string EmailAddress=null,bool IsActive=true,int UserId=0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_Student"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = StudentId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@ParentName", SqlDbType.VarChar).Value = ParentName;
                    cmd.Parameters.Add("@TermId", SqlDbType.Int).Value = TermId;
                    cmd.Parameters.Add("@SemesterId", SqlDbType.Int).Value = SemesterId;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress;
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

        public DataTable Crud_Semester(int OperationId = Common.Select, int SemesterId = 0, string SemesterName = null,int SemesterYear=0, bool IsActive = true, int UserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_Semester"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@SemesterId", SqlDbType.Int).Value = SemesterId;
                    cmd.Parameters.Add("@SemesterName", SqlDbType.VarChar).Value = SemesterName;
                    cmd.Parameters.Add("@SemesterYear", SqlDbType.Int).Value = SemesterYear;
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
        public DataTable Crud_Terms(int OperationId = 1, int TermId = 0, string TermName = null,DateTime? TermStart=null,DateTime? TermEnd=null, bool IsActive = true, int UserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_Term"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@TermId", SqlDbType.Int).Value = TermId;
                    cmd.Parameters.Add("@TermName", SqlDbType.VarChar).Value = TermName;
                    cmd.Parameters.Add("@TermStart", SqlDbType.Date).Value = TermStart;
                    cmd.Parameters.Add("@TermEnd", SqlDbType.Date).Value = TermEnd;
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

        public DataTable GetEmailData(int StudentId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetEmailData"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = StudentId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during GetEmailData : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}