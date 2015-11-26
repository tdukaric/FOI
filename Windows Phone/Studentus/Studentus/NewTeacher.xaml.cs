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

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StudentusDB.Model;
using System.Text;

namespace Studentus
{
    public partial class NewTeacher : PhoneApplicationPage
    {

        public NewTeacher()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void cancelBarButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void newTeacherButton_Click(object sender, EventArgs e)
        {
            teacherName.Text = teacherName.Text.Trim();
            if (teacherName.Text == "")
            {
                MessageBox.Show("Please enter a name for your new teacher.");
                return;
            }

            Teacher teacherToAdd = new Teacher
            {
                ImeNastavnika = teacherName.Text,
                PrezimeNastavnika = teacherSurname.Text,
                TitulaNastavnika = teacherTitle.Text,
                EmailNastavnika = teacherEmail.Text,
                TelefonNastavnika = teacherTel.Text,
                KonzultacijeNastavnika = teacherConsultation.Text
            };

            App.ViewModel.AddTeacher(teacherToAdd);

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        /*
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                if (Empdb.DatabaseExists() == false)
                {
                    Empdb.CreateDatabase();
                    MessageBox.Show("Employee Database Created Successfully!!!");
                }
                else
                {
                    MessageBox.Show("Employee Database already exists!!!");
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                Employee newEmployee = new Employee
                {
                    EmployeeID = Convert.ToInt32(txtEmpid.Text.ToString()),
                    EmployeeAge = txtAge.Text.ToString(),
                    EmployeeName = txtName.Text.ToString()
                };

                Empdb.Employees.InsertOnSubmit(newEmployee);
                Empdb.SubmitChanges();
                MessageBox.Show("Employee Added Successfully!!!");
            }
        }

        public IList<Employee> GetEmployeeList()
        {
            IList<Employee> EmployeeList = null;
            using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                IQueryable<Employee> EmpQuery = from Emp in Empdb.Employees select Emp;
                EmployeeList = EmpQuery.ToList();
            }
            return EmployeeList;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            IList<Employee> EmployeesList = this.GetEmployeeList();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("Employee Details");
            foreach (Employee emp in EmployeesList)
            {
                strBuilder.AppendLine("Name - " + emp.EmployeeName + " Age - " + emp.EmployeeAge);
            }
            MessageBox.Show(strBuilder.ToString());
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                IQueryable<Employee> EmpQuery = from Emp in Empdb.Employees where Emp.EmployeeName == txtName.Text select Emp;
                Employee EmpRemove = EmpQuery.FirstOrDefault();
                Empdb.Employees.DeleteOnSubmit(EmpRemove);
                Empdb.SubmitChanges();
                MessageBox.Show("Employee Deleted Successfully!!!");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                if (Empdb.DatabaseExists())
                {
                    Empdb.DeleteDatabase();
                    MessageBox.Show("Employee Database Deleted Successfully!!!");
                }
            }
        }*/
    }
}