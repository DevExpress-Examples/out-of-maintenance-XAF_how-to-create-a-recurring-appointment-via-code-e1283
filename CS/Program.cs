using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Xml;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            XPDictionary dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(typeof(DevExpress.Persistent.BaseImpl.Event).Assembly);
            string connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=MainDemo";
            IDataStore store = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            XpoDefault.DataLayer = new SimpleDataLayer(dict, store);
            XpoDefault.Session = null;
            CreateRecurringAppointment();
        }
        private static void CreateRecurringAppointment() {
            using (Session session = new Session(XpoDefault.DataLayer)) {
                RecurrenceInfo ri = new RecurrenceInfo(DateTime.Now, TimeSpan.FromMinutes(30));
                //Dennis: set more options of the RecurrenceInfo class here. 
                DevExpress.Persistent.BaseImpl.Event appointment = new DevExpress.Persistent.BaseImpl.Event(session);
                appointment.StartOn = DateTime.Now;
                appointment.Type = (int)AppointmentType.Pattern;
                RecurrenceInfoXmlPersistenceHelper helper = new RecurrenceInfoXmlPersistenceHelper(ri);
                appointment.RecurrenceInfoXml = helper.ToXml();
                session.Save(appointment);
            }
        }
    }
}
