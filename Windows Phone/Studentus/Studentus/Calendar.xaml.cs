using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Studentus
{
    public partial class Calendar : PhoneApplicationPage
    {
        /// <summary>
        /// Konstruktor kalendara
        /// </summary>
        public Calendar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda koja se izvršava kada se tapne na gumb "Today"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Today_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Metoda koja se izvršava kada se tapne na gumb "Calendar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Calendar.xaml", UriKind.Relative));

        }

        /// <summary>
        /// Metoda koja se izvršava kada se tapne na gumb "Subjects".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Subjects_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Subjects.xaml", UriKind.Relative));
        }


        /// <summary>
        /// Metoda koja se izvršava kada korisnik tapne na drugi datum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cal_DateClicked(object sender, WPControls.SelectionChangedEventArgs e)
        {
            //Poziva se stranica /CalendarItem.xaml te se dodaje parametar naziva "datum". Pomoću tog parametra se šalje odabrani datum u obliku stringa stranici.
            NavigationService.Navigate(new Uri("/CalendarItem.xaml?datum=" + e.SelectedDate.ToShortDateString(), UriKind.Relative));
        }
    }
}
