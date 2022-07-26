using Intermix.Commands;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class RateWindowViewModel : BaseViewModel
    {

        public ICommand SendCommand { get; set; }
        public RateWindowViewModel()
        {
            SendCommand = new RelayCommand(
                o => SendFeedBack(OpinionText),
                o => !string.IsNullOrEmpty(OpinionText) && !string.IsNullOrEmpty(Name) && Rating > 0 && Config.Default.IsFeedbackSent == false ) ;
        }

        private void SendFeedBack(string opinion)
        {
            var fromAddress = new MailAddress("feedback112342@gmail.com", Name);
            var toAddress = new MailAddress("dodrzywolski5@gmail.com", "Daniel Odrzywolski");
            const string fromPassword = "ychtlvybzgjnbdml";
            const string subject = "Intermix Feedback";

            try
            {

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = opinion
                };
                
                smtp.Send(message);
                MessageBox.Show("Thank You");
                Config.Default.IsFeedbackSent = true;
                Config.Default.Save();
            }
            catch(Exception) { }
        }

        private string _opinionText;

        public string OpinionText
        {
            get { return _opinionText; }
            set { _opinionText = value;
                OnPropertyChanged("OpinionText");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set { _rating = value;
                OnPropertyChanged("Rating");
            }
        }
    }
}
