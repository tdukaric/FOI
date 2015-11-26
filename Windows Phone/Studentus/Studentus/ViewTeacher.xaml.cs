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
    public partial class ViewTeacher : PhoneApplicationPage
    {
        public ViewTeacher()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs args)
        {
            IDictionary<string, string> t = this.NavigationContext.QueryString;
            if (t.ContainsKey("ime"))
            {
                this.ime.Text = t["ime"];
                this.prezime.Text = t["prezime"];
                this.email.Text = t["email"];
                this.titula.Text = t["titula"];
                this.tel.Text = t["tel"];
                this.konzultacije.Text = t["konzultacije"];
            }
        }

        private void editTeachertButton_Click_click(object sender, EventArgs e)
        {
            MessageBox.Show("edit");

        }

        private void deleteTeachertButton_Click_click(object sender, EventArgs e)
        {
            MessageBox.Show("delete");
        }

        private void backBarButton_Click(object sender, EventArgs e)
        {
            //go back
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}