using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class MessageInfo
    {
        public  void MsgEventHandle(string message, MessageLV lv = MessageLV.Default, params object[] args)
        {  
           if (ShowMessaged!=null)
           {
               ShowMessaged(message, lv.ToString(), args);
           }
        }

        public event MsgEventHandle ShowMessaged;
    }

    
    public enum MessageLV
    {
        Low = 0,
        Default,
        Mid,
        High,

    }

    public delegate void MsgEventHandle(string e, string messLV, params object[] args);

}
