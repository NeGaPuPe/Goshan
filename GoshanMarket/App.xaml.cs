 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.ErrorReport;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;

namespace GoshanMarket
{
    public partial class App : Application
    {
        Models.Tasks tasks;
        public App()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            tasks = new Tasks();
        }
        static List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);
            switch (ClassItemField)
            {
                case "Capacity":
                    int Capacity = 0;
                    foreach (ManagementObject m in searcher.Get()) Capacity += Convert.ToInt32(Math.Round(Convert.ToDouble(m[ClassItemField]) / 1024 / 1024));
                    result.Add(Capacity.ToString() + " Мб");
                    break;
                default:
                    foreach (ManagementObject obj in searcher.Get()) result.Add(obj[ClassItemField].ToString().Trim());
                    break;
            }
            return result;
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string email;
            if(CurrentClient.client != null)
            {
                email = CurrentClient.client.EmailAddress;
            }
            else
            {
                email = "";
            }
            Exception error = e.Exception;
            string ErrorDescription = error.ToString();
            string ErrorMessage = error.Message;
            ErrorReportWindow Window = Application.Current.Windows.OfType<ErrorReportWindow>().FirstOrDefault();
            System.Windows.Forms.DialogResult result = ErrorReportWindow.Show(ErrorReportWindow.ERWindowButton.Да, ErrorReportWindow.ERWindowButton.Нет);
            if (result == System.Windows.Forms.DialogResult.Yes && Email.useremail != "")
            {
                double disksize = Math.Round((double.Parse(GetHardwareInfo("Win32_DiskDrive", "Size").First()) / 1073741824));

                string m1 = "goshanmarket@mail.ru";
                string m2 = "supgoshanmarket@mail.ru";
                string m2SubPassword = "pmjD0cML9JJhzFbuTBnr";
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");
                mySmtpClient.Port = 587;
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential(m2, m2SubPassword);
                mySmtpClient.Credentials = basicAuthenticationInfo;

                string Numbers = "1234567890";
                string CodeTask = "";
                Random random = new Random();
                for (int i = 0; i <= 5; i++)
                {
                    CodeTask += Numbers[random.Next(0, Numbers.Length)];
                }

                MailAddress from = new MailAddress(m2);
                MailAddress to;
                to = new MailAddress(m1);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = $@"<br>
            <br>Здравствуйте, у пользователя {email}  возникла критическая ошибка в приложении!
            <br>
            <br><b>Текст ошибки:</b> {ErrorMessage} 
            <br><b>Подробное описание:</b> {ErrorDescription}
            <br><b>Системные характеристики:</b>
            <br>Объём ОЗУ: {GetHardwareInfo("Win32_PhysicalMemory", "Capacity").First()}
            <br>Процессор: {GetHardwareInfo("Win32_Processor", "Name").First()}
            <br>Производитель процессора: {GetHardwareInfo("Win32_Processor", "Manufacturer").First()}
            <br>Видеокарта: {GetHardwareInfo("Win32_VideoController", "Name").First()}
            <br>Драйвер видеокарты: {GetHardwareInfo("Win32_VideoController", "DriverVersion").First()}
            <br>Диск: {GetHardwareInfo("Win32_DiskDrive", "Caption").First()}
            <br>Объём диска: {disksize}GB.";

                MailAddress replyTo = new MailAddress(m2);
                mailMessage.ReplyToList.Add(replyTo);
                mailMessage.Subject = "Отчёт об ошибке №" + CodeTask;
                mailMessage.Priority = MailPriority.High;
                mailMessage.SubjectEncoding = Encoding.GetEncoding(1251);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mySmtpClient.Send(mailMessage);

                string[] randomPriority = new string[3];
                randomPriority[0] = "Низкий";
                randomPriority[1] = "Средний";
                randomPriority[2] = "Высокий";

                Random r = new Random();

                tasks.Subject = mailMessage.Subject;
                tasks.Priority = randomPriority[r.Next(0, randomPriority.Length - 1)];
                tasks.Text = mailMessage.Body.Replace("<br>", "").Replace("<b>", "").Replace("</b>", "");
                tasks.Date = Convert.ToDateTime(mailMessage.Headers["Date"]);
                tasks.Log = ErrorDescription;
                tasks.EmailUser = Email.useremail;
                tasks.Status = "Активная";

                DBtasks.entities.Tasks.Add(tasks);
                DBtasks.entities.SaveChanges();


                CustomMessageBox.Show("Сообщение об ошибке было отправлено в службу поддержки, ожидайте ответа.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            e.Handled = true;
        }

    }
}
