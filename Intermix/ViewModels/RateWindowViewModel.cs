using Intermix.Commands;
using Intermix.Enums;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System;
using System.Net;
using System.Net.Mail;
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
                o => !string.IsNullOrEmpty(OpinionText) && !string.IsNullOrEmpty(Name) && Rating > 0 && Config.Default.IsFeedbackSent == false);
        }

        #region Methods

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
                _ = new CustomizedMessageBox("Thank for your input in making this perfection", MessageType.Success, MessageButton.Ok).ShowDialog();
                Config.Default.IsFeedbackSent = true;
                Config.Default.Save();
            }
            catch (Exception) { }
        }

        #endregion

        #region Properties
        private string _opinionText;

        public string OpinionText
        {
            get { return _opinionText; }
            set
            {
                _opinionText = value;
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
            set
            {
                _rating = value;
                OnPropertyChanged("Rating");
            }
        }
        #endregion
    }
}
