using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using SmartHome_WPF.Auxiliares;
using SmartHome_WPF.Collections;
using SmartHome_WPF.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO.Ports;

namespace SmartHome_WPF.ViewModel
{
    public class MonitorViewModel : ModelBase
    {
        #region fields

        public SerialPort s;
        public string _serial;

        #endregion

        #region constructor
        public MonitorViewModel(ResourceDictionary resourceDictionary) : base() { }
        #endregion

        public void Load()
        {

            new Thread(Executar).Start();

        }

        public void Executar()
        {
            s = new SerialPort();
            s.PortName = "COM7"; // Mude conforme a porta serial do seu Arduino
            s.BaudRate = 9600;

            if (!s.IsOpen) // Se a porta serial não estiver disponível
            {
                try
                {
                    s.Open(); // Abre
                    _serial = s.ReadLine();
                    s.Close(); // Fecha
                }
                catch
                {
                }
            }

            Inserir(_serial);
            Thread.Sleep(1000);
            new Thread(Executar).Start();
        }

        public void Inserir(string serial)
        {
            try
            {

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=SMARTHOME;User ID=admin;Password=admin";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                cnn.Close();

                String[] valores = serial.Split('|');

                if (valores[0] == "001")
                {
                    var replace01 = valores[1].Replace("\r", "");
                    var replace02 = replace01.Replace(".", ",");
                    var replace03 = replace02.Replace("-", "0");

                    double valorInsert = Convert.ToDouble(replace03);
                    valorInsert = valorInsert / 1000;

                    SqlCommand command = new SqlCommand("[dbo].[sp_inserir_consumo]", cnn);
                    command.CommandType = CommandType.StoredProcedure;
                    cnn.Open();
                    command.Parameters.Add(new SqlParameter("@id_Residencia", SqlDbType.Int)).Value = valores[0];
                    command.Parameters.Add(new SqlParameter("@valor", SqlDbType.Float)).Value = valorInsert;

                    command.ExecuteNonQuery();

                    cnn.Close();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}

