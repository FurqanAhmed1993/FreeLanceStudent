using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace StudentManagement.Repository
{
    public static class Common
    {
        public const string ConnectionStringKey = "ConnectionString";
        public static int Sql_CommandTimeout = 36000;

        public const int Select = 1;
        public const int Insert = 2;
        public const int Update = 3;
        public const int Delete = 4;
        public const int Login = 5;
        public const string StudentPaymentEmailTemplate = "StudentPaymentEmailTemplate";

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        object value = dr[pro.Name];
                        if (value == DBNull.Value)
                        {
                            value = null;
                            pro.SetValue(obj, value, null);
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        private static string GetTemplateString(string templateCode)
        {
            StreamReader objStreamReader;
            string emailText = "";
            switch (templateCode)
            {
                case Common.StudentPaymentEmailTemplate:
                    objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"EmailTemplate\StudentPaymentTemplate.html");
                    emailText = objStreamReader.ReadToEnd();
                    objStreamReader.Close();
                    objStreamReader = null;
                    break;
            }
            return emailText;
        }
        public static string GetMessage(string TemplateCode, DataTable dtEmailData)
        {

            try
            {
                if (dtEmailData.Rows.Count > 0)
                {
                    string FirstName = dtEmailData.Rows[0]["FirstName"].ToString();
                    string LastName = dtEmailData.Rows[0]["LastName"].ToString();
                    string StudentFullName = dtEmailData.Rows[0]["StudentFullName"].ToString();
                    string CustomerName = dtEmailData.Rows[0]["CustomerName"].ToString();
                    string TermName = dtEmailData.Rows[0]["TermName"].ToString();
                    string TermStart = dtEmailData.Rows[0]["TermStart"].ToString();
                    string PaymentFinalDate = dtEmailData.Rows[0]["PaymentFinalDate"].ToString();
                    string RefNo = dtEmailData.Rows[0]["RefNo"].ToString();
                    string Amount = dtEmailData.Rows[0]["Amount"].ToString();
                    string TransactionTypeCode = dtEmailData.Rows[0]["TransactionTypeCode"].ToString();
                    string ParentName = dtEmailData.Rows[0]["ParentName"].ToString();
                    string UnitAmountIncTax = dtEmailData.Rows[0]["UnitAmountIncTax"].ToString();
                    string SemesterName = dtEmailData.Rows[0]["SemesterName"].ToString();
                    string SemesterYear = dtEmailData.Rows[0]["SemesterYear"].ToString();
                    string BankName = dtEmailData.Rows[0]["BankName"].ToString();
                    string AccountNumber = dtEmailData.Rows[0]["AccountNumber"].ToString();
                    string AccountName = dtEmailData.Rows[0]["AccountName"].ToString();
                    string BSBNumber = dtEmailData.Rows[0]["BSBNumber"].ToString();
                    string BankReferenceNumber = dtEmailData.Rows[0]["BankReferenceNumber"].ToString();
                    string TermStartYear = dtEmailData.Rows[0]["TermStartYear"].ToString();

                    string Msg = GetTemplateString(StudentPaymentEmailTemplate);

                    Msg = Msg.Replace("##FirstName##", FirstName);
                    Msg = Msg.Replace("##LastName##", LastName);
                    Msg = Msg.Replace("##StudentFullName##", StudentFullName);
                    Msg = Msg.Replace("##CustomerName##", CustomerName);
                    Msg = Msg.Replace("##TermName##", TermName);
                    Msg = Msg.Replace("##TermStart##", TermStart);
                    Msg = Msg.Replace("##PaymentFinalDate##", PaymentFinalDate);
                    Msg = Msg.Replace("##RefNo##", RefNo);
                    Msg = Msg.Replace("##Amount##", Amount);
                    Msg = Msg.Replace("##TransactionTypeCode##", TransactionTypeCode);
                    Msg = Msg.Replace("##ParentName##", ParentName);
                    Msg = Msg.Replace("##UnitAmountIncTax##", UnitAmountIncTax);
                    Msg = Msg.Replace("##SemesterName##", SemesterName);
                    Msg = Msg.Replace("##SemesterYear##", SemesterYear);
                    Msg = Msg.Replace("##BankName##", BankName);
                    Msg = Msg.Replace("##AccountNumber##", AccountNumber);
                    Msg = Msg.Replace("##AccountName##", AccountName);
                    Msg = Msg.Replace("##BSBNumber##", BSBNumber);
                    Msg = Msg.Replace("##BankReferenceNumber##", BankReferenceNumber);
                    Msg = Msg.Replace("##TermStartYear##", TermStartYear);


                    return Msg;
                }
            }
            catch (Exception ex)
            {

                return "";
            }


            return "";

        }

    }
}