using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Intermix.Models.LibrarySystemModels
{
    public class Notification
    {
        public string NotificationType { get; set; }
        public string Title { get; set; }
        public string AdditionalDescription { get; set; }
        public SolidColorBrush Foreground { get; set; }
    }
}
