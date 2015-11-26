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
using Microsoft.Phone.Shell;

namespace Studentus
{
    public partial class NewTask : PhoneApplicationPage
    {
        private int subjectCurrentID;
        private int rSubjectCurrentID;
        private int teacherCurrentID;
        public Boolean ListPickerClosed = true;

        public NewTask()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void ListPicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ListPicker ListPickerControl = (ListPicker)sender;

            if (ListPickerClosed)
            {
                ListPickerControl.Open();

                //In full mode this event is not triggered
                if (ListPickerControl.Items.Count <= ListPickerControl.ItemCountThreshold)
                    ListPickerClosed = false;
            }
            else
            {
                ListPickerClosed = true;
            }
        }

        private void OnChangePivot(object sender, SelectionChangedEventArgs e)
        {

            switch (settings.SelectedIndex)
            {
                case 0:
                    ApplicationBar = new ApplicationBar();

                    ApplicationBarIconButton appBarButtonCancel = new ApplicationBarIconButton();
                    appBarButtonCancel.IconUri = new Uri("/Images/appbar.back.rest.png", UriKind.Relative);
                    appBarButtonCancel.Text = "Cancel";
                    ApplicationBar.Buttons.Add(appBarButtonCancel);
                    appBarButtonCancel.Click += new EventHandler(Cancel_Click);

                    ApplicationBarIconButton appBarButtonAddTask = new ApplicationBarIconButton();
                    appBarButtonAddTask.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative);
                    appBarButtonAddTask.Text = "Add Task";
                    ApplicationBar.Buttons.Add(appBarButtonAddTask);
                    appBarButtonAddTask.Click += new EventHandler(NewTask_Click);

                    break;

                case 1:
                    ApplicationBar = new ApplicationBar();

                    ApplicationBarIconButton appBarButtonrCancel = new ApplicationBarIconButton();
                    appBarButtonrCancel.IconUri = new Uri("/Images/appbar.back.rest.png", UriKind.Relative);
                    appBarButtonrCancel.Text = "Cancel";
                    ApplicationBar.Buttons.Add(appBarButtonrCancel);
                    appBarButtonrCancel.Click += new EventHandler(Cancel_Click);

                    ApplicationBarIconButton appBarButtonAddRTask = new ApplicationBarIconButton();
                    appBarButtonAddRTask.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative);
                    appBarButtonAddRTask.Text = "Add Class";
                    ApplicationBar.Buttons.Add(appBarButtonAddRTask);
                    appBarButtonAddRTask.Click += new EventHandler(NewRTask_Click);

                    break;
            }

        }
        private void NewTask_Click(object sender, EventArgs e)
        {
            
            taskName.Text = taskName.Text.Trim();
            if (taskName.Text == "")
            {
                MessageBox.Show("Please enter a name for your new task.");
                return;
            }

            Task taskToAdd = new Task
            {
                NazivZadatka = taskName.Text,
                OpisZadatka = taskDescription.Text,
                //PocetakZadatka = (DateTime)startTask.Value,
                DeadLine = (DateTime)deadLine.Value,
                OznakaPredmeta = subjectCurrentID
            };

            App.ViewModel.AddTask(taskToAdd);

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        private void NewRTask_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(teacherCurrentID.ToString());
            //MessageBox.Show(rSubjectCurrentID.ToString());
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void taskSubjectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (subjectPick.SelectedIndex != -1)
            {
                var subject = subjectPick.SelectedItem as Subject;
                subjectCurrentID = subject.OznakaPredmeta;
            }
        }

        private void rTaskTeacherChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacherPick.SelectedIndex != -1)
            {
                var teacher = teacherPick.SelectedItem as Teacher;
                teacherCurrentID = teacher.OznakaNastavnika;
            }

        }

        private void rTaskSubjectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rSubjectPick.SelectedIndex!=-1)
            {
                var subject = rSubjectPick.SelectedItem as Subject;
                rSubjectCurrentID = subject.OznakaPredmeta;
            }

        }

    }
}