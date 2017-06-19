using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ClassGenerator;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Service
{
    public class MainDAL : IMainDalInterface
    {
        private static ProcessData ByteToProcessData(byte[] array)
        {
            using (Stream stream = new MemoryStream(array))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Binder = new Binder();
                return (ProcessData) formatter.Deserialize(stream);
            }
        }

        static byte[] GetBytes(OdbcDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int) bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        public List<ProcessData> GetData()
        {
            string baseName =
                "C:\\Users\\Andrei Yakubovich\\Documents\\Visual Studio 2017\\Projects\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\16BITM03.sqlite";
            ProcessData pd =
                new ProcessData(
                    "C:\\Users\\Andrei Yakubovich\\Documents\\Visual Studio 2017\\Projects\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\16BITM03.xml");
            pd.AddRecipe();

            OdbcConnection dbConnection = new OdbcConnection("DRIVER=SQLite3 ODBC Driver;Database=" + baseName +
                                                             ";LongNames=0;Timeout=1000;NoTXN=0;SyncPragma = NORMAL; StepAPI = 0;");
            List<ProcessData> process = new List<ProcessData>();
            try
            {
                OdbcCommand dbCommand = new OdbcCommand();
                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                byte[] array = new byte[0];
                dbCommand.CommandText = "Select * from [Process]";
                List<int> arr = new List<int>();
                using (var rdr = dbCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        arr.Add((int)rdr["ID"]);
                       }

                }

                foreach(int id in arr)
                { 
                dbCommand.CommandText = "Select [Data] from [Process] Where ID =" + id;

                if (dbCommand.ExecuteScalar() != null)
                {
                    using (var reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            array = GetBytes(reader);
                        }
                    }
                    var proc1 = ByteToProcessData(array);
                    process.Add(proc1);
                }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
            return process;
        }

        public CountandDatesDAL GetCountandDates()
        {
            CountandDatesDAL CountAndDates = new CountandDatesDAL();
            string baseName =
                "C:\\Users\\Andrei Yakubovich\\Documents\\Visual Studio 2017\\Projects\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\16BITM03.sqlite";
            SQLiteFactory factory = (SQLiteFactory) DbProviderFactories.GetFactory("System.Data.SQLite");
            using (var connection = (SQLiteConnection) factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source = " + baseName;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"SELECT MAX[ID] FROM [Process]";

                    command.CommandType = CommandType.Text;
                    var a = command.ExecuteReader();
                    connection.Close();
                    return CountAndDates;
                }
            }
        }
    }


    public class Binder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type tyType = null;

            string sShortAssemblyName = assemblyName.Split(',')[0];
            Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly ayAssembly in ayAssemblies)
            {
                if (sShortAssemblyName == ayAssembly.FullName.Split(',')[0])
                {
                    tyType = ayAssembly.GetType(typeName);
                    break;
                }
            }
            return tyType;
        }
    }
}




