Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Xml

Namespace ConsoleApplication1
	Friend Class Program
		Shared Sub Main(ByVal args() As String)
			Dim dict As XPDictionary = New ReflectionDictionary()
			dict.GetDataStoreSchema(GetType(DevExpress.Persistent.BaseImpl.Event).Assembly)
			Dim connectionString As String = "Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=MainDemo"
			Dim store As IDataStore = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema)
			XpoDefault.DataLayer = New SimpleDataLayer(dict, store)
			XpoDefault.Session = Nothing
			CreateRecurringAppointment()
		End Sub
		Private Shared Sub CreateRecurringAppointment()
			Using session As New Session(XpoDefault.DataLayer)
				Dim ri As New RecurrenceInfo(DateTime.Now, TimeSpan.FromMinutes(30))
				'Dennis: set more options of the RecurrenceInfo class here. 
				Dim appointment As New DevExpress.Persistent.BaseImpl.Event(session)
				appointment.StartOn = DateTime.Now
				appointment.Type = CInt(Fix(AppointmentType.Pattern))
				Dim helper As New RecurrenceInfoXmlPersistenceHelper(ri, DateSavingType.LocalTime)
				appointment.RecurrenceInfoXml = helper.ToXml()
				session.Save(appointment)
			End Using
		End Sub
	End Class
End Namespace
