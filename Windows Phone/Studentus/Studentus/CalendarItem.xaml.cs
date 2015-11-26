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
    public partial class CalendarItem : PhoneApplicationPage
    {
        /// <summary>
        /// Konstruktor stranice CalendarItem.
        /// </summary>
        public CalendarItem()
        {
            InitializeComponent();
            this.ApplicationTitle.Text = "STUDENTUS";
        }

        private DateTime dat = new DateTime();
        

        /// <summary>
        /// Override funkcije OnNavigatedTo. U njoj se prima string "datum" te se pohranjuje i ispisuje kao PageTitle.
        /// </summary>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //Kreiranje nove varijable "datum" tipa string, te je njegova početna vrijednost: ""
            string datum = string.Empty;

            // provjerava se postoji li parametar "datum" u pozivu stranice. Ako psotoji, sprema se u varijablu "datum"
            bool datumValueExists = NavigationContext.QueryString.TryGetValue("datum", out  datum);

            if (datumValueExists)
            {
                //U varijablu dat sprema se datum u obliku DateTime-a.
                this.dat = DateTime.Parse(datum);

                //Mijenja se naziv stranice u datum oblika dd.mm.yyyy.
                this.PageTitle.Text = string.Concat(this.dat.Day.ToString() + "." + this.dat.Month.ToString() + "." + this.dat.Year.ToString() + ".");
            }

            var items = from task in App.ViewModel.AllTaskItems
                        where task.DeadLine == this.dat
                        orderby task.NazivZadatka descending
                        select task;
        }
    }
}
