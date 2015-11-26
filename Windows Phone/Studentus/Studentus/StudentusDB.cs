using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace StudentusDB.Model
{

    public class StudentusDataContext : DataContext
    {
        /// <summary>
        /// Pass the connection string to the base class.
        /// </summary>
        /// <param name="connectionString"></param>
        public StudentusDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify tables for the data.

        public Table<Semestar> Items;
        public Table<Subject> SubjectItems;
        public Table<Teacher> TeacherItems;
        public Table<Task> TaskItems;

    }

    #region Tablica Semestar
    /// <summary>
    /// Definira se tablica "Semestar"
    /// </summary>
    [Table]
    public class Semestar : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _oznakaSemestra;

        /// <summary>
        /// Definira ID semestra, tip podatka je INT, NOT NULL te je primarni ključ
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int OznakaSemestra
        {
            get { return _oznakaSemestra; }
            set
            {
                if (_oznakaSemestra != value)
                {
                    NotifyPropertyChanging("OznakaSemestra");
                    _oznakaSemestra = value;
                    NotifyPropertyChanged("OznakaSemestra");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _nazivSemestra;

        /// <summary>
        /// NazivSemestra definira naziv semestra, tip podatka je string
        /// </summary>
        [Column]
        public string NazivSemestra
        {
            get { return _nazivSemestra; }
            set
            {
                if (_nazivSemestra != value)
                {
                    NotifyPropertyChanging("NazivSemestra");
                    _nazivSemestra = value;
                    NotifyPropertyChanged("NazivSemestra");
                }
            }
        }

        private DateTime _pocetak;

        /// <summary>
        /// Definira početak semestra, tipa je DateTime
        /// </summary>
        [Column]
        public DateTime Pocetak
        {
            get { return _pocetak; }
            set
            {
                if (_pocetak != value)
                {
                    NotifyPropertyChanging("Pocetak");
                    _pocetak = value;
                    NotifyPropertyChanged("Pocetak");
                }
            }
        }


        private DateTime _kraj;

        /// <summary>
        /// Definira kraj semestra, tipa je DateTime
        /// </summary>
        [Column]
        public DateTime Kraj
        {
            get { return _kraj; }
            set
            {
                if (_kraj != value)
                {
                    NotifyPropertyChanging("Kraj");
                    _kraj = value;
                    NotifyPropertyChanged("Kraj");
                }
            }
        }

        //private EntitySet<Subject> subjectsRef;

        //[Association(ThisKey = "OznakaSemestra", OtherKey = "OznakaSemestra", Name = "FK_Subjects_Semesters", Storage = "subjectsRef")]
        //public EntitySet<Subject> Subjects
        //{
        //    get
        //    {
        //        return this.subjectsRef;
        //    }
        //    set
        //    {
        //        subjectsRef = value;
        //    }
        //}

        //public Semestar()
        //{
        //        subjectsRef = new EntitySet<Subject>();
        //}

        /*// Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;*/


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event koji se okida kod promjene u tablici
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        /// <summary>
        /// Event koji se okida kada je potrebno nešto promijeniti u tablici
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
    #endregion

    #region Tablica Subject

    /// <summary>
    /// Definira se tablica Subject
    /// </summary>
    [Table]
    public class Subject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _oznakaPredmeta;

        /// <summary>
        /// Definira oznaku, tj. ID predmeta, Tipa je INT, NOT NULL te je primarni ključ
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int OznakaPredmeta
        {
            get { return _oznakaPredmeta; }
            set
            {
                if (_oznakaPredmeta != value)
                {
                    NotifyPropertyChanging("OznakaPredmeta");
                    _oznakaPredmeta = value;
                    NotifyPropertyChanged("OznakaPredmeta");
                }
            }
        }

        private string _nazivPredmeta;

        /// <summary>
        /// Definira se naziv predmeta, tipa je string
        /// </summary>
        [Column]
        public string NazivPredmeta
        {
            get { return _nazivPredmeta; }
            set
            {

                if (_nazivPredmeta != value)
                {
                    NotifyPropertyChanging("nazivPredmeta");
                    _nazivPredmeta = value;
                    NotifyPropertyChanged("nazivPredmeta");
                }

            }
        }

        private int _oznakaSemestra;

        /// <summary>
        /// Definira se oznaka semestra, tipa je int. Time se označuje u kojem semestru navedeni predmet pripada.
        /// </summary>
        [Column]
        public int OznakaSemestra
        {
            get { return _oznakaSemestra; }
            set
            {

                if (_oznakaSemestra != value)
                {
                    NotifyPropertyChanging("OznakaSemestra");
                    _oznakaSemestra = value;
                    NotifyPropertyChanged("OznakaSemestra");
                }

            }
        }

        //private EntityRef<Semestar> semestersRef = new EntityRef<Semestar>();

        //[Association(ThisKey = "OznakaSemestra", OtherKey = "OznakaSemestra", IsForeignKey=true, 
        //    Name = "FK_Subjects_Semesters", Storage = "semestersRef", DeleteRule="CASCADE")]
        //public Semestar Semester
        //{
        //    get
        //    {
        //        return this.semestersRef.Entity;
        //    }
        //    set
        //    {
        //        Semestar previousValue = this.semestersRef.Entity;
        //        if (((previousValue != value) || (this.semestersRef.HasLoadedOrAssignedValue == false)))
        //        {
        //            if ((previousValue != null))
        //            {
        //                this.semestersRef.Entity = null;
        //                previousValue.Subjects.Remove(this);
        //            }
        //            this.semestersRef.Entity = value;
        //            if ((value != null))
        //            {
        //                value.Subjects.Add(this);
        //                this.OznakaSemestra = value.OznakaSemestra;
        //            }
        //            else
        //            {
        //                this.OznakaSemestra = default(Nullable<int>);
        //            }
        //        }
        //    }
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
    #endregion

    #region Tablica Teacher
    [Table]
    public class Teacher : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _oznakaNastavnika;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int OznakaNastavnika
        {
            get { return _oznakaNastavnika; }
            set
            {
                if (_oznakaNastavnika != value)
                {
                    NotifyPropertyChanging("OznakaNastavnika");
                    _oznakaNastavnika = value;
                    NotifyPropertyChanged("OznakaNastavnika");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _imeNastavnika;

        [Column(CanBeNull = false)]
        public string ImeNastavnika
        {
            get { return _imeNastavnika; }
            set
            {
                if (_imeNastavnika != value)
                {
                    NotifyPropertyChanging("ImeNastavnika");
                    _imeNastavnika = value;
                    NotifyPropertyChanged("ImeNastavnika");
                }
            }
        }
        private string _prezimeNastavnika;

        [Column(CanBeNull = false)]
        public string PrezimeNastavnika
        {
            get { return _prezimeNastavnika; }
            set
            {
                if (_prezimeNastavnika != value)
                {
                    NotifyPropertyChanging("PrezimeNastavnika");
                    _prezimeNastavnika = value;
                    NotifyPropertyChanged("PrezimeNastavnika");
                }
            }
        }
        private string _titulaNastavnika;

        [Column]
        public string TitulaNastavnika
        {
            get { return _titulaNastavnika; }
            set
            {
                if (_titulaNastavnika != value)
                {
                    NotifyPropertyChanging("TitulaNastavnika");
                    _titulaNastavnika = value;
                    NotifyPropertyChanged("TitulaNastavnika");
                }
            }
        }
        private string _emailNastavnika;

        [Column]
        public string EmailNastavnika
        {
            get { return _emailNastavnika; }
            set
            {
                if (_emailNastavnika != value)
                {
                    NotifyPropertyChanging("EmailNastavnika");
                    _emailNastavnika = value;
                    NotifyPropertyChanged("EmailNastavnika");
                }
            }
        }
        private string _telefonNastavnika;

        [Column]
        public string TelefonNastavnika
        {
            get { return _telefonNastavnika; }
            set
            {
                if (_telefonNastavnika != value)
                {
                    NotifyPropertyChanging("TelefonNastavnika");
                    _telefonNastavnika = value;
                    NotifyPropertyChanged("TelefonNastavnika");
                }
            }
        }
        private string _konzultacijeNastavnika;

        [Column]
        public string KonzultacijeNastavnika
        {
            get { return _konzultacijeNastavnika; }
            set
            {
                if (_konzultacijeNastavnika != value)
                {
                    NotifyPropertyChanging("KonzultacijeNastavnika");
                    _konzultacijeNastavnika = value;
                    NotifyPropertyChanged("KonzultacijeNastavnika");
                }
            }
        }


        /*// Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;*/


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
    #endregion

    #region Tablica Task
    [Table]
    public class Task : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int _oznakaZadatka;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int OznakaZadatka
        {
            get { return _oznakaZadatka; }
            set
            {
                if (_oznakaZadatka != value)
                {
                    NotifyPropertyChanging("OznakaZadatka");
                    _oznakaZadatka = value;
                    NotifyPropertyChanged("OznakaZadatka");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _nazivZadatka;

        [Column]
        public string NazivZadatka
        {
            get { return _nazivZadatka; }
            set
            {
                if (_nazivZadatka != value)
                {
                    NotifyPropertyChanging("NazivZadatka");
                    _nazivZadatka = value;
                    NotifyPropertyChanged("NazivZadatka");
                }
            }
        }

        private string _opisZadatka;

        [Column]
        public string OpisZadatka
        {
            get { return _opisZadatka; }
            set
            {
                if (_opisZadatka != value)
                {
                    NotifyPropertyChanging("OpisZadatka");
                    _opisZadatka = value;
                    NotifyPropertyChanged("OpisZadatka");
                }
            }
        }

        private DateTime _deadLine;

        [Column]
        public DateTime DeadLine
        {
            get { return _deadLine; }
            set
            {
                if (_deadLine != value)
                {
                    NotifyPropertyChanging("DeadLine");
                    _deadLine = value;
                    NotifyPropertyChanged("DeadLine");
                }
            }
        }


        private int _oznakaPredmeta;

        [Column]
        public int OznakaPredmeta
        {
            get { return _oznakaPredmeta; }
            set
            {
                if (_oznakaPredmeta != value)
                {
                    NotifyPropertyChanging("OznakaPredmeta");
                    _oznakaPredmeta = value;
                    NotifyPropertyChanged("OznakaPredmeta");
                }
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
    #endregion

}
