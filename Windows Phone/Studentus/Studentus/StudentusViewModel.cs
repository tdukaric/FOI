using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StudentusDB.Model;

namespace Studentus.ViewModel
{
    public class StudentusViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private StudentusDataContext studentusDB;
        private Semestar _currentSemester;

        public Semestar CurrentSemester
        {
            get { return _currentSemester; }
            set { _currentSemester = value; }
        }

        // Class constructor, create the data context object.
        public StudentusViewModel(string StudentusDBConnectionString)
        {
            studentusDB = new StudentusDataContext(StudentusDBConnectionString);
            CurrentSemester = new Semestar();
        }

        //All semesters
        private ObservableCollection<Semestar> _allSemesterItems;
        public ObservableCollection<Semestar> AllSemesterItems
        {
            get { return _allSemesterItems; }
            set
            {
                _allSemesterItems = value;
                NotifyPropertyChanged("AllSemesterItems");
            }
        }

        private ObservableCollection<Subject> _allSubjectItems;
        public ObservableCollection<Subject> AllSubjectItems
        {
            get { return _allSubjectItems; }
            set
            {
                _allSubjectItems = value;
                NotifyPropertyChanged("AllSubjectItems");
            }

        }
        //All Teachers
        private ObservableCollection<Teacher> _allTeacherItems;
        public ObservableCollection<Teacher> AllTeacherItems
        {
            get { return _allTeacherItems; }
            set
            {
                _allTeacherItems = value;
                NotifyPropertyChanged("AllTeacherItems");
            }

        }

        private ObservableCollection<Task> _allTaskItems;
        public ObservableCollection<Task> AllTaskItems
        {
            get { return _allTaskItems; }
            set
            {
                _allTaskItems = value;
                NotifyPropertyChanged("AllTaskItems");
            }

        }

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            studentusDB.SubmitChanges();
        }

        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {
            App.ViewModel.CurrentSemester = FindCurrentSemester();

            // Specify the query for all semesters in the database.
            var SemesterItemsInDB = from Semestar sem in studentusDB.Items
                                    select sem;

            var SubjectItemsInDB = from Subject sub in studentusDB.SubjectItems
                                   where sub.OznakaSemestra==App.ViewModel.CurrentSemester.OznakaSemestra select sub;

            var TeacherItemsInDB = from Teacher sub in studentusDB.TeacherItems
                                   select sub;

            var TaskItemsInDB = from Task sub in studentusDB.TaskItems
                                   select sub;

            // Query the database and load all semesters and subjects.

            AllSemesterItems = new ObservableCollection<Semestar>();
            AllSubjectItems = new ObservableCollection<Subject>();
            AllTeacherItems = new ObservableCollection<Teacher>();
            AllTaskItems = new ObservableCollection<Task>();

            foreach (Semestar Semester in SemesterItemsInDB)
                AllSemesterItems.Add(Semester);

            foreach (Subject Sub in SubjectItemsInDB)
                AllSubjectItems.Add(Sub);

            foreach (Teacher Sub in TeacherItemsInDB)
                AllTeacherItems.Add(Sub);

            foreach (Task Sub in TaskItemsInDB)
                AllTaskItems.Add(Sub);

        }

        public void LoadFiltered(int id)
        {
            var subjectItems = from Subject sub in studentusDB.SubjectItems
                               where sub.OznakaSemestra == id
                               select sub;
            AllSubjectItems = new ObservableCollection<Subject>();
            foreach (Subject Sub in subjectItems)
                AllSubjectItems.Add(Sub);
        }

        public void getTasksForSevenDays(int dayIndex)
        {
            var today = DateTime.Today;
            int todayIndex = (int)today.DayOfWeek-1;
            var tasks = from Task tus in studentusDB.TaskItems
                        where (int)(tus.DeadLine.DayOfWeek-1) == dayIndex &&
                        tus.DeadLine==today.AddDays(dayIndex-todayIndex)
                        select tus;
            AllTaskItems.Clear();
            foreach (Task Tus in tasks)
                AllTaskItems.Add(Tus);
        }

        public Semestar FindCurrentSemester()
        {
            var semesters = from Semestar sem in studentusDB.Items
                            select sem;
            DateTime today = DateTime.Now;
            Semestar currentSemester = new Semestar();
            foreach (Semestar s in semesters)
            {
                if (today >= s.Pocetak && today <= s.Kraj)
                {
                    currentSemester = s;
                    break;
                }
            }
            return currentSemester;
        }

        // Add an object to the database and collections.
        public void AddSemester(Semestar newSemesterItem)
        {

          
            // Add a to-do item to the data context.
            studentusDB.Items.InsertOnSubmit(newSemesterItem);

            // Add a to-do item to the "all" observable collection.
            AllSemesterItems.Add(newSemesterItem);

            // Save changes to the database.
            studentusDB.SubmitChanges();

        }

        public void AddSubject(Subject newSubjectItem)
        {
            studentusDB.SubjectItems.InsertOnSubmit(newSubjectItem);
            AllSubjectItems.Add(newSubjectItem);
            studentusDB.SubmitChanges();
        }

        public void AddTeacher(Teacher newTeacherItem)
        {
            studentusDB.TeacherItems.InsertOnSubmit(newTeacherItem);
            AllTeacherItems.Add(newTeacherItem);
            studentusDB.SubmitChanges();
        }

        public void AddTask(Task newTaskItem)
        {
            studentusDB.TaskItems.InsertOnSubmit(newTaskItem);
            AllTaskItems.Add(newTaskItem);
            studentusDB.SubmitChanges();
        }

        public void DeleteSubjectItem(Subject subjectForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllSubjectItems.Remove(subjectForDelete);

            // Remove the to-do item from the data context.
            studentusDB.SubjectItems.DeleteOnSubmit(subjectForDelete);

            // Save changes to the database.
            studentusDB.SubmitChanges();

        }

        // Remove a to-do task item from the database and collections.
        public void DeleteSemesterItem(Semestar semesterForDelete)
        {
            ObservableCollection<Subject> marks = new ObservableCollection<Subject>();

            foreach (Subject subject in AllSubjectItems)
            {
                if (semesterForDelete.OznakaSemestra == subject.OznakaSemestra)
                {
                    marks.Add(subject);
                }
                
            }

            foreach (Subject m in marks)
                this.DeleteSubjectItem(m);

            //studentusDB.SubmitChanges();

            // Remove the to-do item from the "all" observable collection.
            AllSemesterItems.Remove(semesterForDelete);

            // Remove the to-do item from the data context.
            studentusDB.Items.DeleteOnSubmit(semesterForDelete);

            // Save changes to the database.
            studentusDB.SubmitChanges();
        }

        public void DeleteTeacherItem(Teacher teacherForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllTeacherItems.Remove(teacherForDelete);

            // Remove the to-do item from the data context.
            studentusDB.TeacherItems.DeleteOnSubmit(teacherForDelete);

            // Save changes to the database.
            studentusDB.SubmitChanges();
        }

        public void DeleteTaskItem(Task taskForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllTaskItems.Remove(taskForDelete);

            // Remove the to-do item from the data context.
            studentusDB.TaskItems.DeleteOnSubmit(taskForDelete);

            // Save changes to the database.
            studentusDB.SubmitChanges();
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
