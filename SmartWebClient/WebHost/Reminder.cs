using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebHost
{
    /// <summary>
    /// 提醒实体
    /// </summary>
    [Serializable]
    public struct Reminder
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 提醒类型
        /// </summary>
        public RemindType Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 提醒地址
        /// </summary>
        public string Address { get; set; }

        public Reminder(DateTime beginTime, DateTime endTime, RemindType type, string title, string content, string address)
            : this()
        {
            BeginTime = beginTime;
            EndTime = endTime;
            Type = type;
            Title = title;
            Content = content;
            Address = address;
        }
    }

    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum RemindType
    {
        ByDay = 0,
        ByWeek,
        Once,
        Forever
    }
}
