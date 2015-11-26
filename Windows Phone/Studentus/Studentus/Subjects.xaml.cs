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

using Coding4Fun.Phone.Controls;
using System.Windows.Controls.Primitives;

namespace Studentus
{
    public partial class Subjects : PhoneApplicationPage
    {
        ApplicationBarIconButton appBarButtonAdd;
        private StudentusDB.Model.Teacher tch;
        private StudentusDB.Model.Subject sub;
        private MessagePrompt edit_delete_popup;

        public Subjects()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void Today_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Calendar_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Calendar.xaml", UriKind.Relative));

        }

        private void Subjects_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Subjects.xaml?settings=0", UriKind.Relative));
        }

        private void OnChangePivot(object sender, SelectionChangedEventArgs e)
        {

            switch (settings.SelectedIndex)
            {
                case 0:
                    if (ApplicationBar.Buttons.Count == 4)
                    {
                        ApplicationBar.Buttons.Remove(appBarButtonAdd);
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Subject" };
                        appBarButtonAdd.Click += SubjectAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }
                    else if (ApplicationBar.Buttons.Count == 3)
                    {
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Subject" };
                        appBarButtonAdd.Click += SubjectAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }
                    break;

                case 1:
                    if (ApplicationBar.Buttons.Count == 4)
                    {
                        ApplicationBar.Buttons.Remove(appBarButtonAdd);
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Teachers" };
                        appBarButtonAdd.Click += TeachersAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }
                    else if (ApplicationBar.Buttons.Count == 3)
                    {
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Teachers" };
                        appBarButtonAdd.Click += TeachersAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }
                    break;

                case 2:
                    if (ApplicationBar.Buttons.Count == 4)
                    {
                        ApplicationBar.Buttons.Remove(appBarButtonAdd);
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Semesters" };
                        appBarButtonAdd.Click += SemestersAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }
                    else if (ApplicationBar.Buttons.Count == 3)
                    {
                        appBarButtonAdd = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative)) { Text = "Add Semesters" };
                        appBarButtonAdd.Click += SemestersAdd_Click;
                        ApplicationBar.Buttons.Add(appBarButtonAdd);
                    }

                    break;
            }

        } 

        private void deleteSemesterButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Semestar semesterForDelete = button.DataContext as Semestar;

                App.ViewModel.DeleteSemesterItem(semesterForDelete);
            }

            // Put the focus back to the main page.
            this.Focus();
        }

        private void deleteTeachertButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.

            App.ViewModel.DeleteTeacherItem(this.tch);

            //hide popup
            edit_delete_popup.Hide();
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selectedSemester = (SemesterItemsListBox.SelectedItem) as Semestar;
            App.ViewModel.CurrentSemester = selectedSemester;
            App.ViewModel.LoadFiltered(selectedSemester.OznakaSemestra);
            settings.SelectedItem = subPivot;
            
        }

        private void SubjectAdd_Click(object sender, EventArgs e)
        {
            if (App.ViewModel.AllSemesterItems.Count != 0)
                NavigationService.Navigate(new Uri("/NewSubject.xaml", UriKind.Relative));
            else
            {
                MessageBox.Show("You don't have any semesters created. " +
                    "Please create a semester first, and then add a subject to it.");
                return;
            }
        }
        private void TeachersAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewTeacher.xaml", UriKind.Relative));
        }
        private void SemestersAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewSemester.xaml", UriKind.Relative));
        }

        private void deleteSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.DeleteSubjectItem(this.sub);

            //hide popup
            edit_delete_popup.Hide();
        }

        private void viewTeachertButton_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var t = TeacherItemsListBox.SelectedItem as Teacher;

            string destination = "/ViewTeacher.xaml";

            //formiranje stringa za predaju podataka na ViewTeacher.xaml
            destination += String.Format("?ime={0}&prezime={1}&titula={2}&email={3}&tel={4}&konzultacije={5}",
                t.ImeNastavnika, t.PrezimeNastavnika, t.TitulaNastavnika, t.EmailNastavnika, t.TelefonNastavnika, t.KonzultacijeNastavnika);

            this.NavigationService.Navigate(new Uri(destination, UriKind.Relative));
        }

        private void popupTeachertButton_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            popup_edit_delete popup = new popup_edit_delete();
            popup.BtnDelete.Click += new RoutedEventHandler(deleteTeachertButton_Click);

            edit_delete_popup = new MessagePrompt();
            edit_delete_popup.Title = "Studentus";
            edit_delete_popup.Body = popup;
            edit_delete_popup.Show();

            var tc = sender as TextBlock;
            this.tch = (StudentusDB.Model.Teacher)tc.DataContext;

        }

        private void popupSubjectButton_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            popup_edit_delete popup = new popup_edit_delete();
            popup.BtnDelete.Click += new RoutedEventHandler(deleteSubjectButton_Click);

            edit_delete_popup = new MessagePrompt();
            edit_delete_popup.Title = "Studentus";
            edit_delete_popup.Body = popup;
            edit_delete_popup.Show();
           
            var su = sender as TextBlock;
            this.sub = (StudentusDB.Model.Subject)su.DataContext;

        }
    }
}
