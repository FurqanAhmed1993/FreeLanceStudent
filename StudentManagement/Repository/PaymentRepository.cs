using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManagement.Repository
{
    public class PaymentRepository:DALRepository
    {
        public DataTable Crud_PaymentDetail(int OperationId = Common.Select, int PaymentDetailId = 0, string RefNo = null, int StudentId = 0
            ,decimal Amount=0,decimal UnitAmountIncTax=0,int BankId=0,int TransactionTypeId=0,string PaymentFinalDate=null, bool IsActive = true, int UserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_PaymentDetail"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@PaymentDetailId", SqlDbType.Int).Value = PaymentDetailId;
                    cmd.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = RefNo;
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = StudentId;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
                    cmd.Parameters.Add("@UnitAmountIncTax", SqlDbType.Decimal).Value = UnitAmountIncTax;
                    cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
                    cmd.Parameters.Add("@TransactionTypeId", SqlDbType.Int).Value = TransactionTypeId;
                    cmd.Parameters.Add("@PaymentFinalDate", SqlDbType.VarChar).Value = PaymentFinalDate!=null?Convert.ToDateTime(PaymentFinalDate).ToString("yyyy-MM-dd"):null;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Crud_PaymentDetail : {0}", ex.Message), ex);
            }
            return dt;
        }

        public DataTable Crud_TransactionType(int OperationId = Common.Select,  int TransactionTypeId = 0,string TransactionTypeName=null
            ,string TransactionTypeCode=null, bool IsActive = true, int UserId = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Crud_TransactionType"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
                    cmd.Parameters.Add("@TransactionTypeId", SqlDbType.Int).Value = TransactionTypeId;
                    cmd.Parameters.Add("@TransactionTypeName", SqlDbType.VarChar).Value = TransactionTypeName;
                    cmd.Parameters.Add("@TransactionTypeCode", SqlDbType.VarChar).Value = TransactionTypeCode;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Crud_TransactionType : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}