﻿using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using HslCommunication.Profinet.Panasonic;
using System;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.IODriver.Panasonic
{
    public class PanasonicSerialReader : IDriverAdapter
    {

        private SerialPort serialPort;
        private PanasonicMewtocol panasonicMewtocol = null;

        public bool _IsConnected = false;
        private object LockObject = new object();
        public bool IsConnected { get; set; }
        public PanasonicSerialReader(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }
        public byte Station
        {
            get { return station; }
            set { station = value; }
        }

        /// <summary>
        /// Returns true if a connection to the PLC can be established
        /// </summary>
        public bool IsAvailable
        {
            //TODO: Fix This
            get
            {
                try
                {
                    Connection();

                    return IsConnected;


                }
                catch
                {
                    return false;
                }
            }
        }
        private byte station = 5;
        public void Connection()
        {

            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                lock (LockObject)
                {


                    panasonicMewtocol?.Close();
                    panasonicMewtocol = new PanasonicMewtocol(station);

                    try
                    {
                        panasonicMewtocol.SerialPortInni(sp =>
                        {
                            sp.PortName = serialPort.PortName;
                            sp.BaudRate = serialPort.BaudRate;
                            sp.DataBits = serialPort.DataBits;
                            sp.StopBits = serialPort.StopBits;
                            sp.Parity = serialPort.Parity;
                        });
                        panasonicMewtocol.Open();
                        IsConnected = true;
                    }
                    catch (Exception ex)
                    {
                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                    }

                    stopwatch.Stop();
                }
            }
            catch (TimeoutException ex)
            {
                stopwatch.Stop();

                IsConnected = false;
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }

        public void Disconnection()
        {

            try
            {
                panasonicMewtocol.Close();

                IsConnected = false;


            }
            catch (TimeoutException ex)
            {



                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
        }



        public bool Write(string address, dynamic value)
        {
            if (value is bool)
            {
                panasonicMewtocol.Write(address, value);
            }
            else
            {
                panasonicMewtocol.Write(address, value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            if (typeof(TValue) == typeof(bool))
            {
                var b = panasonicMewtocol.ReadBool(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = panasonicMewtocol.ReadUInt16(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = panasonicMewtocol.ReadInt32(address, length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = panasonicMewtocol.ReadUInt32(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = panasonicMewtocol.ReadInt64(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = panasonicMewtocol.ReadUInt64(address, length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = panasonicMewtocol.ReadInt16(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = panasonicMewtocol.ReadDouble(address, length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = panasonicMewtocol.ReadFloat(address, length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = panasonicMewtocol.ReadString(address, length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public byte[] BuildReadByte(byte station, string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildWriteByte(byte station, string address, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadDiscrete(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public ConnectionState GetConnectionState()
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(string[] address)
        {
            throw new NotImplementedException();
        }

        public TValue[] Read<TValue>(DataBlock db)
        {
            throw new NotImplementedException();
        }
    }
}
