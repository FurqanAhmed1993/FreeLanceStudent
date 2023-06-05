using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Threading;

namespace StudentManagement.Repository
{
    public class Email
    {
        public static string EmailErrorFilePath = System.Configuration.ConfigurationManager.AppSettings["EmailErrorFilePath"];
        public static string SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmailAddress"];
        public static string SenderEmailPassword = System.Configuration.ConfigurationManager.AppSettings["SenderEmailPassword"];
        public static string SenderSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SenderSMTPServer"];
        public static string Port = System.Configuration.ConfigurationManager.AppSettings["Port"];
        public static string clientEnableSsl = System.Configuration.ConfigurationManager.AppSettings["clientEnableSsl"];


        public static string SendMails(string to, string subject, string msg, string cc, string bcc, string Attachment)
        {
            try
            {
                if (SenderSMTPServer != "" && SenderEmailAddress != "")
                {
                    to = to.Replace(",", ";");
                    cc = cc.Replace(",", ";");
                    bcc = bcc.Replace(",", ";");

                    


                    string displayName = "Student Semester Payment";
                    MailMessage message = new MailMessage();
                    string[] addresses = to.Split(';');
                    if (addresses.Length > 0)
                    {
                        foreach (string address in addresses)
                        {
                            if (address.Trim() != "")
                            {
                                message.To.Add(new MailAddress(address.Trim()));
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(cc) == false)
                    {
                        string[] addressesCC = cc.Split(';');
                        if (addressesCC.Length > 0)
                        {
                            foreach (string address in addressesCC)
                            {
                                if (address.Trim() != "")
                                {
                                    message.CC.Add(new MailAddress(address.Trim()));
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(bcc) == false)
                    {
                        string[] addressesBCC = bcc.Split(';');
                        if (addressesBCC.Length > 0)
                        {
                            foreach (string address in addressesBCC)
                            {
                                if (address.Trim() != "")
                                {
                                    message.Bcc.Add(new MailAddress(address.Trim()));
                                }
                            }
                        }
                    }
                    if (Attachment != string.Empty)
                    {
                        string[] attachmentArray = Attachment.Split(';');
                        foreach (string attachmentsSplit in attachmentArray)
                        {
                            if (attachmentsSplit.Trim() != "")
                            {
                                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachmentsSplit);
                                message.Attachments.Add(attach);
                            }
                        }
                    }

                    message.From = new MailAddress(SenderEmailAddress, displayName);
                    message.Subject = subject;
                    message.Body = msg;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.Port = Convert.ToInt32(Port);
                    client.Host = SenderSMTPServer;
                    System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
                    client.EnableSsl = Convert.ToBoolean(clientEnableSsl);
                    client.UseDefaultCredentials = false;
                    client.Credentials = nc;
                    client.Send(message);
                   
                }
            }
            catch (Exception ex)
            {
                
            }

            return "";
        }

    }
}