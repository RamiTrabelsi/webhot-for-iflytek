using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    public class MessageHelper
    {
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="msgLV">消息级别</param>
        public static void ShowMessage(string message, MessageLV msgLV)
        {
            if (MsgEvent != null)
            {
                MsgEvent(null, new MessageEventArgs(message, msgLV));
            }
        }

        public static event EventHandler MsgEvent;
    }
}
