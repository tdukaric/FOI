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

using Studentus.ViewModel;
using StudentusDB.Model;

namespace Studentus
{
    public partial class NewSubject : PhoneApplicationPage
    {

        public NewSubject()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            if (App.ViewModel.CurrentSemester.NazivSemestra != "")
                selectedSemester.Text = App.ViewModel.CurrentSemester.NazivSemestra;
            else
                selectedSemester.Text = "There are no created semesters.";
        }

        //private void SemesterChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (semesterPick.SelectedIndex != -1)
        //    {
        //        semesterPick.SelectedItem = App.ViewModel.CurrentSemester;
        //    }
            
        //}

        private void newSubjectBarButton_Click(object sender, EventArgs e)
        {
            if (subjectName.Text == "")
            {
                MessageBox.Show("Please enter a name for your new subject.");
                return;
            }

            Subject subjectToAdd = new Subject
            {
                NazivPredmeta = subjectName.Text,
                OznakaSemestra = App.ViewModel.CurrentSemester.OznakaSemestra
            };

            App.ViewModel.AddSubject(subjectToAdd);

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void cancelBarButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}