﻿using AdvancedScada.DriverBase;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using static AdvancedScada.IBaseService.Common.XCollection;
namespace AdvancedScada.IODriver.Delta.ASCII
{
    public class DeltaASCIIMaster : IDriverAdapter
    {
        private SerialPort serialPort;

        public bool IsConnected { get; set; }
        public byte Station { get; set; }
        public DeltaASCIIMaster(short slaveId, SerialPort serialPort)
        {
            Station = (byte)slaveId;
            this.serialPort = serialPort;
        }

        private ModbusAscii busAsciiClient = null;

        public bool Connection()
        {

            busAsciiClient?.Close();
            busAsciiClient = new ModbusAscii(Station);
            busAsciiClient.AddressStartWithZero = true;

            busAsciiClient.IsStringReverse = false;
            try
            {

                busAsciiClient.SerialPortInni(sp =>
                {
                    sp.PortName = serialPort.PortName;
                    sp.BaudRate = serialPort.BaudRate;
                    sp.DataBits = serialPort.DataBits;
                    sp.StopBits = serialPort.StopBits;
                    sp.Parity = serialPort.Parity;
                });
                busAsciiClient.Open();
                return true;


            }
            catch (TimeoutException ex)
            {


                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return false;
            }
        }

        public bool Disconnection()
        {
            try
            {
                busAsciiClient.Close();
                return true;
            }
            catch (TimeoutException ex)
            {

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return false;
            }
        }






        public bool[] ReadDiscrete(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            return busAsciiClient.ReadDiscrete($"{Address}", length).Content;
        }

        public bool Write(string address, dynamic value)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (value is bool)
            {
                busAsciiClient.Write($"{Address}", value);
            }
            else
            {
                busAsciiClient.Write($"{Address}", value);
            }

            return true;
        }

        public TValue[] Read<TValue>(string address, ushort length)
        {
            var Address = DMT.DevToAddrW("DVP", address, Station);
            if (typeof(TValue) == typeof(bool))
            {
                var b = busAsciiClient.ReadCoil($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ushort))
            {
                var b = busAsciiClient.ReadUInt16($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(int))
            {
                var b = busAsciiClient.ReadInt32($"{Address}", length).Content;

                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(uint))
            {
                var b = busAsciiClient.ReadUInt32($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(long))
            {
                var b = busAsciiClient.ReadInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(ulong))
            {
                var b = busAsciiClient.ReadUInt64($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            if (typeof(TValue) == typeof(short))
            {
                var b = busAsciiClient.ReadInt16($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(double))
            {
                var b = busAsciiClient.ReadDouble($"{Address}", length).Content;
                return (TValue[])(object)b;
            }
            if (typeof(TValue) == typeof(float))
            {
                var b = busAsciiClient.ReadFloat($"{Address}", length).Content;
                return (TValue[])(object)b;

            }
            if (typeof(TValue) == typeof(string))
            {
                var b = busAsciiClient.ReadString($"{Address}", length).Content;
                return (TValue[])(object)b;
            }

            throw new InvalidOperationException(string.Format("type '{0}' not supported.", typeof(TValue)));
        }

        public bool[] ReadSingle(string address, ushort length)
        {
            throw new NotImplementedException();
        }
    }
}