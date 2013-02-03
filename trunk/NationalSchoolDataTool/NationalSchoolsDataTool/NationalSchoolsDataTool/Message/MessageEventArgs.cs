using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageLV MessageLV { get; set; }

        public MessageEventArgs()
        {

        }

        public MessageEventArgs(string msg, MessageLV msgLV)
            : this()
        {
            Message = msg;
            MessageLV = msgLV;
        }
    }

    public enum MessageLV
    {
        Default = 0,
        Low,
        Mid,
        High
    }
}
