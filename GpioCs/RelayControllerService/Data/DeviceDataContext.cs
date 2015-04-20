using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

namespace RelayControllerService.Data
{
	public class DeviceDataContext : IDisposable
	{
		IDbConnection _connection = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="RelayControllerService.DeviceDataContext"/> class.
		/// <description>DeviceDataContext connects to the SQLite database and provides
		/// an abstraction to store and retrieve, DeviceData </description>
		/// </summary>
		public DeviceDataContext ()
		{
			string connectionString = "URI=file:piofthings.db";
			bool initialize = false;
			if (!System.IO.File.Exists ("piofthings.db")) 
			{
				initialize = true;
			}
			_connection = (IDbConnection)new SqliteConnection (connectionString);
			_connection.Open ();
			if (initialize) 
			{
				InitializeDb ();
			}
		}

		private void InitializeDb ()
		{
			try 
			{
				IDbCommand createDeviceData = Connection.CreateCommand ();
				createDeviceData.CommandText = "CREATE TABLE DeviceData (Id INTEGER PRIMARY KEY, DeviceId TEXT NOT NULL, Active INTEGER NOT NULL)";
				createDeviceData.CommandType = CommandType.Text;
				createDeviceData.ExecuteNonQuery ();

				IDbCommand insertDataCommand = Connection.CreateCommand ();
				insertDataCommand.CommandText = "INSERT INTO DeviceData VALUES (null, '" + Guid.NewGuid ().ToString () + "', 1)";
				insertDataCommand.CommandType = CommandType.Text;
				insertDataCommand.ExecuteNonQuery ();

				createDeviceData.Dispose ();
				createDeviceData = null;
				insertDataCommand.Dispose ();
				insertDataCommand = null;

			} 
			catch (Exception ex) 
			{
				Console.WriteLine ("Error instantiating database " + ex.Message +
				"\n" + ex.StackTrace);
			}
		}

		public IDbConnection Connection 
		{
			get 
			{
				return _connection;
			}
		}

		public DeviceData GetActiveDeviceData ()
		{
			IDbCommand getDeviceData = this.Connection.CreateCommand ();
			getDeviceData.CommandText = "SELECT Id, DeviceId FROM DeviceData WHERE Active=1";
			getDeviceData.CommandType = CommandType.Text;
			var reader = getDeviceData.ExecuteReader ();
			List<DeviceData> devices = new List<DeviceData> ();
			while (reader.Read ()) 
			{
				DeviceData current = new DeviceData ();
				current.Id = reader.GetInt16 (0);
				current.DeviceId = reader.GetString (1);
				current.IsActive = true;
				devices.Add (current);
			}
			reader.Close ();
			reader.Dispose ();
			getDeviceData.Dispose ();
			return devices[0];
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			if (_connection != null) 
			{
				_connection.Dispose ();
			}
		}

		#endregion
	}
}