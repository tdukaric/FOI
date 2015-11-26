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
using Microsoft.Phone.Controls.LocalizedResources;


using Studentus.ViewModel;
using StudentusDB.Model;

namespace Studentus
{
    public partial class NewSemester : PhoneApplicationPage
    {
        public NewSemester()
        {
            InitializeComponent();

            this.DataContext = App.ViewModel;
            
        }


        private void newSemesterBarButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (textBox1.Text == "")
            {
                MessageBox.Show("You haven't entered a name of the semester!");
                return;
            }
            Semestar semester = new Semestar
            {
                
                NazivSemestra = textBox1.Text,
                Pocetak = (DateTime)datePickerStart.Value,
                Kraj = (DateTime)datePickerEnd.Value
            };

            App.ViewModel.AddSemester(semester);

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }

            App.ViewModel.CurrentSemester = semester;

        }

        private void cancelBarButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        void startDate_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (this.datePickerEnd.Value < this.datePickerStart.Value) 
                this.datePickerEnd.Value = datePickerStart.Value;
        }

    }
}