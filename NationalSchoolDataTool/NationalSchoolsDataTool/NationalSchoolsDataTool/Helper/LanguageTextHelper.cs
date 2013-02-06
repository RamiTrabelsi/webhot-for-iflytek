using System;
using System.Collections.Generic; 
using System.Text;
using System.Text.RegularExpressions;

namespace NationalSchoolsDataTool
{
    public sealed class LanguageTextHelper
    {

        /// <summary>
        /// 提取中文字符
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="ignoreChar">不参与匹配的字符</param>
        /// <returns>中文字符串</returns>
        public static string GetChineseCharactors(string text, char ignoreChar)
        {
            StringBuilder sb = new StringBuilder();
            string[] strArray = text.Split(ignoreChar);
            foreach (string str in strArray)
            {
                sb.Append(GetChineseCharactors(str));
                sb.Append(ignoreChar);
            }
            return sb.ToString().TrimEnd(ignoreChar);
        }

        /// <summary>
        /// 提取中文字符
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>中文字符串</returns>
        public static string GetChineseCharactors(string text)
        {
            string pattern = @"[\u4E00-\u9FA5\uF900-\uFA2D\x20，。？、；（）“”《》！：·——……￥‘’－╗╚┐└]+";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                sb.Append(match.Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 提取中文字符
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>中文字符串</returns>
        public static string GetChineseCharactorsAndNums(string text)
        {
            string pattern = @"[\u4E00-\u9FA5\uF900-\uFA2D\x20，。？、；（）“”《》！：·——……￥‘’－╗╚┐└0123456789]+";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                sb.Append(match.Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 提取中文字符，不包括标点符号
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>中文字符串</returns>
        public static string GetChineseCharactorsWithoutPunctuation(string text)
        {
            string pattern = @"[\u4E00-\u9FA5\n]+";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                sb.Append(match.Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 提取中文字符，不包括标点符号
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>中文字符串</returns>
        public static string GetChCharsAndNumsWithoutPunctuation(string text)
        {
            string pattern = @"[\u4E00-\u9FA5\n0123456789]+";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                sb.Append(match.Value);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 判断所输入的字符串是否为中文，保留回车符
        /// </summary>
        /// <param name="text"> 所要匹配的字符串 </param>
        /// <returns> 返回真假值， true：匹配；false ：不匹配 </returns>
        public static bool IsChinese(string text)
        {
            Regex myReg = new Regex(@"^[\u4E00-\u9FA5\uF900-\uFA2D\x20\n，。？、；（）“”《》！：·——……￥‘’－╗╚┐└]+$");
            return myReg.IsMatch(text);
        }

        /// <summary>
        /// 判断输入的字符串是否全是数字
        /// </summary>
        /// <param name="text"> 所要匹配的字符串 </param>
        /// <returns> 返回真假值， true：匹配；false ：不匹配 </returns>
        public static bool IsNum(string text)
        {
            Regex myReg = new Regex(@"^[0123456789]+$");
            return myReg.IsMatch(text);
        }

        /// <summary>
        /// 提取英文字符
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>英文字符串</returns>
        public static string GetEnglishCharactors(string text)
        {
            StringBuilder sb = new StringBuilder();
            bool flag = false;//用于标记是否应该插入空格，以分隔单词
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] <= 'Z' && text[i] >= 'A') || (text[i] <= 'z' && text[i] >= 'a') || text[i] == ' ')
                {
                    sb.Append(text[i]);
                    flag = true;
                }
                else
                {
                    //如果是英文的末尾没有空格，则插入空格
                    if (flag)
                    {
                        sb.Append(' ');
                        flag = false;
                    }
                }

            }
            return sb.ToString();
        }

        
    }
}
