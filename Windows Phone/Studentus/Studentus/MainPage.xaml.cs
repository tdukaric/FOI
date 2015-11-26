using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Studentus.ViewModel;

namespace Studentus
{
    public partial class MainPage : PhoneApplicationPage
    {

        

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }


        private void Today_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml?week=0", UriKind.Relative));
        }

        private void Calendar_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Calendar.xaml", UriKind.Relative));

        }

        private void Subjects_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Subjects.xaml", UriKind.Relative));
        }

        private void NewTask_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewTask.xaml", UriKind.Relative));
        }

        private void week_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            int dayOfWeek = (int)today.DayOfWeek - 1;
            var pivot = sender as Pivot;
            pivot.SelectedIndex = dayOfWeek;
        }

        private void week_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            var pivot = sender as Pivot;
            var today = DateTime.Today;
            App.ViewModel.getTasksForSevenDays(pivot.SelectedIndex);
        }

        private void week_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            var pivot = sender as Pivot;
            var today = DateTime.Today;
            App.ViewModel.getTasksForSevenDays(pivot.SelectedIndex);
        }

    }
}