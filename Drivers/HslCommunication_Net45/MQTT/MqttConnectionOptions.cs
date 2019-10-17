﻿using System;

namespace HslCommunication.MQTT
{
    /// <summary>
    /// 连接服务器的选项
    /// </summary>
    public class MqttConnectionOptions
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public MqttConnectionOptions()
        {
            ClientId = string.Empty;
            IpAddress = "127.0.0.1";
            Port = 1883;
            KeepAlivePeriod = TimeSpan.FromSeconds(100);
            KeepAliveSendInterval = TimeSpan.FromSeconds(30);
            CleanSession = true;
            ConnectTimeout = 5000;
        }

        #endregion

        /// <summary>
        /// Mqtt服务器的ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口号。默认1883
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 客户端的id的标识
        /// </summary>
        /// <remarks>
        /// 实际在传输的时候，采用的是UTF8编码的方式来实现。
        /// </remarks>
        public string ClientId { get; set; }

        /// <summary>
        /// 连接到服务器的超时时间，默认是2秒，单位是毫秒
        /// </summary>
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// 登录服务器的凭证
        /// </summary>
        public MqttCredential Credentials { get; set; }

        /// <summary>
        /// 设置的参数，最小单位为1s，当超过设置的时间间隔没有发送数据的时候，必须发送PINGREQ报文，否则服务器认定为掉线。
        /// </summary>
        /// <remarks>
        /// 保持连接（Keep Alive）是一个以秒为单位的时间间隔，表示为一个16位的字，它是指在客户端传输完成一个控制报文的时刻到发送下一个报文的时刻，
        /// 两者之间允许空闲的最大时间间隔。客户端负责保证控制报文发送的时间间隔不超过保持连接的值。如果没有任何其它的控制报文可以发送，
        /// 客户端必须发送一个PINGREQ报文，详细参见 [MQTT-3.1.2-23]
        /// </remarks>
        public TimeSpan KeepAlivePeriod { get; set; }

        /// <summary>
        /// 获取或是设置心跳时间的发送间隔。默认30秒钟
        /// </summary>
        public TimeSpan KeepAliveSendInterval { get; set; }

        /// <summary>
        /// 是否清理会话，如果清理会话（CleanSession）标志被设置为1，客户端和服务端必须丢弃之前的任何会话并开始一个新的会话。
        /// 会话仅持续和网络连接同样长的时间。与这个会话关联的状态数据不能被任何之后的会话重用 [MQTT-3.1.2-6]。默认为清理会话。
        /// </summary>
        public bool CleanSession { get; set; }

    }
}
