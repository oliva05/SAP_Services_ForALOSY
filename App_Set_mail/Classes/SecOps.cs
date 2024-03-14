using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Collections;
//using Microsoft.Exchange.WebServices.Data;

namespace App_Set_mail
{
    class SecOps
    {
        public DataSet ValidateUser(string user, string password)
        {
            DataSet toreturn = new DataSet();
            DataOperations dp = new DataOperations();

//            toreturn = dp.ACS_GetSelectData(@"  SELECT TOP 1 [id]
//			                                            ,[nombre]
//			                                            ,[tipo]
//			                                            ,[defaultheme]
//                                              FROM [dbo].[conf_usuarios]
//                                             WHERE [usuario] = '" + user + @"'
//                                               AND [encpass] = HASHBYTES('SHA1', '" + password + @"');");

            toreturn = dp.ACS_GetSelectData(@"  SELECT TOP 1 [id]
			                                            ,[nombre]
			                                            ,[tipo]
			                                            ,[defaultheme]
                                                        ,[ADUser]
                                              FROM [dbo].[conf_usuarios]
                                             WHERE [ADUser] = '" + user + @"';");

            if (toreturn.Tables[0].Rows.Count > 0)
            {
                return toreturn;
            }
            else
            {
                return null;
            }
        }
        public void SendEmailAlert_GD_v2(int puerto, ArrayList pReserveirs, int ColumnNumber, string Subject, string Body, bool isHTML, MailPriority priority)
        {
            try
            {
                
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("apps@aquafeedhn.net", "Aquafeed Apps");

                foreach(string row in pReserveirs)
                {
                    message.To.Add(new MailAddress(row));
                }
                


                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = isHTML;
                message.Priority = priority;

                smtp.EnableSsl = true;
                smtp.Port = puerto;
                //smtp.Host = "smtpout.secureserver.net";
                smtp.Host = "outlook.office365.com";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("apps@aquafeedhn.net", "$Applications1620&$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient cliente = new SmtpClient();

                smtp.Send(message);
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps2@aquafeedhn.net", "Aquafeed Apps");

                        //message.To.Add(new MailAddress());
                        foreach (string row in pReserveirs)
                        {
                            message.To.Add(new MailAddress(row));
                        }

                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps2@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
                catch (Exception ex2)
                {
                    if (ex2.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps3@aquafeedhn.net", "Aquafeed Apps");

                        //message.To.Add(new MailAddress(Receivers));
                        foreach (string row in pReserveirs)
                        {
                            message.To.Add(new MailAddress(row));
                        }

                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps3@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
            }
        }

        public void SendEmailTrackingCompras(int puerto,  ArrayList recipients, string Subject, string Body, bool isHTML, MailPriority priority)
        {
            try
            {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("apps@aquafeedhn.net", "Aquafeed Apps");

                foreach (string row in recipients)
                {
                    message.To.Add(new MailAddress(row));
                }
                //message.To.Add("reuceda05@hotmail.com");
                //message.To.Add("danys.oliva@aquafeedhn.com");



                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = isHTML;
                message.Priority = priority;

                smtp.EnableSsl = true;
                smtp.Port = puerto;
                //smtp.Host = "smtpout.secureserver.net";
                smtp.Host = "outlook.office365.com";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("apps@aquafeedhn.net", "$Applications1620&$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient cliente = new SmtpClient();

                smtp.Send(message);
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps2@aquafeedhn.net", "Aquafeed Apps");


                        foreach (string row in recipients)
                        {
                            message.To.Add(new MailAddress(row));
                        }

                        //message.To.Add("reuceda05@hotmail.com");

                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps2@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
                catch (Exception ex2)
                {
                    if (ex2.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps3@aquafeedhn.net", "Aquafeed Apps");

                        foreach (string row in recipients)
                        {
                            message.To.Add(new MailAddress(row));
                        }

                        //message.To.Add("reuceda05@hotmail.com");


                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps3@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
            }
        }


        public void SendEmailAlert_GD(int puerto,string Receivers, int ColumnNumber, string Subject, string Body, bool isHTML, MailPriority priority)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("apps@aquafeedhn.net", "Aquafeed Apps");


                message.To.Add(new MailAddress(Receivers));
                

                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = isHTML;
                message.Priority = priority;

                smtp.EnableSsl = true;
                smtp.Port = puerto;
                //smtp.Host = "smtpout.secureserver.net";
                smtp.Host = "outlook.office365.com";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("apps@aquafeedhn.net", "$Applications1620&$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient cliente = new SmtpClient();

                smtp.Send(message);
                smtp.Dispose();
            }
            catch (Exception ex) 
            {
                try
                {
                    if (ex.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps2@aquafeedhn.net", "Aquafeed Apps");

                        message.To.Add(new MailAddress(Receivers));

                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps2@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
                catch (Exception ex2) 
                {
                    if (ex2.HResult == -2146233088)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress("apps3@aquafeedhn.net", "Aquafeed Apps");

                        message.To.Add(new MailAddress(Receivers));

                        message.Subject = Subject;
                        message.Body = Body;
                        message.IsBodyHtml = isHTML;
                        message.Priority = priority;

                        smtp.EnableSsl = true;
                        smtp.Port = puerto;
                        //smtp.Host = "smtpout.secureserver.net";
                        smtp.Host = "outlook.office365.com";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("apps3@aquafeedhn.net", "A1dd1cf460&");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);
                        smtp.Dispose();
                    }
                }
            }
        }

        public void SendEmailAlert_SingleContact_GD(string Receiver, int ColumnNumber, string Subject, string Body, bool isHTML)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("apps@aquafeedhn.net", "Aquafeed Apps");

            message.To.Add(new MailAddress(Receiver));

            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = isHTML;

            smtp.EnableSsl = true;
            smtp.Port = 3535;
            //smtp.Host = "smtpout.secureserver.net";
            smtp.Host = "outlook.office365.com";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("apps@aquafeedhn.net", "$Applications1620&$");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message);
        }

        //public void SendEmail_EX(/*DataSet Receivers, int ColumnNumber, string Subject, string Body, bool isHTML*/)
        //{
        //    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

        //    service.Credentials = new WebCredentials("david.riega@aquafeedhn.com", "3vilM0nk3y07&");

        //    service.TraceEnabled = true;
        //    service.TraceFlags = TraceFlags.All;

        //    service.AutodiscoverUrl("david.riega@aquafeedhn.com", RedirectionUrlValidationCallback);

        //    EmailMessage email = new EmailMessage(service);

        //    email.ToRecipients.Add("test@aquafeedhn.com");

        //    email.Subject = "HelloWorld";
        //    email.Body = new MessageBody("This is the first email I've sent by using the EWS Managed API");

        //    email.Send();
        //}

        private static bool RedirectionUrlValidationCallback(string redirectionURL) 
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionURL);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        public string GetTableHTML(DataSet Information)
        {
            try
            {
                string messageBody = "<font>El Siguiente es un correo de prueba, por favor hacer caso omiso. </font><br><br>";

                if (Information.Tables[0].Rows.Count == 0)
                    return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;

                foreach (DataColumn column in Information.Tables[0].Columns)
                {
                    messageBody += htmlTdStart + column.ColumnName + " " + htmlTdEnd;
                }

                messageBody += htmlHeaderRowEnd;

                foreach (DataRow Row in Information.Tables[0].Rows)
                {
                    messageBody = messageBody + htmlTrStart;

                    foreach (DataColumn column in Information.Tables[0].Columns)
                    {
                        messageBody += htmlTdStart + Row[column.ColumnName] + " " + htmlTdEnd;
                    }

                    messageBody = messageBody + htmlTrEnd;
                }

                messageBody = messageBody + htmlTableEnd;


                return messageBody;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
