﻿using Microsoft.International.Converters.PinYinConverter;  //nuget
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Sealee.Util
{
    public static class CommonExtensions
    {
        #region String扩展方法

        /// <summary>
        /// 汉字转化为拼音
        /// </summary>
        /// <param name="str">汉字</param>
        /// <returns>全拼</returns>
        public static string GetPinyin(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        /// <summary>
        /// 汉字转化为拼音首字母
        /// </summary>
        /// <param name="str">汉字</param>
        /// <returns>首字母</returns>
        public static string GetFirstPinyin(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        /// <summary>
        ///     String 转换 Int
        /// </summary>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue = int.MinValue)
        {
            if (!int.TryParse(str, out int result))
            {
                if (defaultValue != int.MinValue)
                {
                    result = defaultValue;
                }
                else
                {
                    throw new Exception(str + "无法转换成整数类型！");
                }
            }
            return result;
        }

        /// <summary>
        /// String 转换 Decimal
        /// </summary>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue = decimal.MinValue)
        {
            if (!decimal.TryParse(str, out decimal result))
            {
                if (defaultValue != decimal.MinValue)
                {
                    result = defaultValue;
                }
                else
                {
                    throw new Exception(str + "无法转换成数值类型！");
                }
            }
            return result;
        }

        /// <summary>
        ///     String 转换 Float
        /// </summary>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue = float.MinValue)
        {
            if (!float.TryParse(str, out float result))
            {
                if (defaultValue != float.MinValue)
                {
                    result = defaultValue;
                }
                else
                {
                    throw new Exception(str + "无法转换成数值类型！");
                }
            }
            return result;
        }

        /// <summary>
        ///     String 转换 Double
        /// </summary>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = double.MinValue)
        {
            if (!double.TryParse(str, out double result))
            {
                if (defaultValue != double.MinValue)
                {
                    result = defaultValue;
                }
                else
                {
                    throw new Exception(str + "无法转换成数值类型！");
                }
            }
            return result;
        }

        /// <summary>
        /// String 转换 DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string defaultValue = "")
        {
            if (!DateTime.TryParse(str, out DateTime result))
            {
                if (defaultValue != "")
                {
                    DateTime.TryParse(defaultValue, out result);
                }
                else
                {
                    throw new Exception(str + "不是有效的时间格式");
                }
            }
            return result;
        }

        #endregion String扩展方法

        #region 枚举的扩展方法

        /// <summary>
        /// 根据枚举值获取枚举描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>描述字符串</returns>
        public static string EnumGetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false); //获取描述的集合
                return attributes.Length > 0 ? attributes[0].Description : value.ToString(); //存在取第一个,不存在返回Name
            }
            return "";
        }

        /// <summary>
        /// 获取根据枚举名称获取枚举描述
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumName">枚举名称</param>
        /// <returns></returns>
        public static string EnumGetDescription<T>(this string enumName)
        {
            FieldInfo fi = typeof(T).GetField(enumName);
            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : enumName;
            }
            return "";
        }

        #endregion 枚举的扩展方法

        #region 单个数组变多个

        /// <summary>
        /// 合并数组    把单个的集合变成多个的集合  [1,2,3,4,5]  =>  [[1,2],[3,4],[5]]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        #endregion 单个数组变多个

        /// <summary>
        /// 字符串处理  类名称 (_切分，首字母大写 （.)去除  )
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            string[] arry = value.Split('_');
            string str = "";
            foreach (string item in arry)
            {
                string newstr = item.Replace("(", "").Replace(".", "").Replace(")", "");
                string firstLetter = newstr.Substring(0, 1);
                string rest = newstr.Substring(1, newstr.Length - 1);
                str += firstLetter.ToUpper() + rest.ToLower();
            }
            return str;
        }

        /// <summary>
        /// 字符串处理 类属性 (_切分，首字母小写 （.)去除  )
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToInitialCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            string[] arry = value.Split('_');
            string str = "";
            int i = 0;
            foreach (string item in arry)
            {
                string newstr = item.Replace("(", "").Replace(".", "").Replace(")", "");
                string firstLetter = newstr.Substring(0, 1);
                string rest = newstr.Substring(1, newstr.Length - 1);
                //str += firstLetter.ToUpper() + rest.ToLower();
                if (i == 0)
                    str += firstLetter + rest.ToLower();
                else
                    str += firstLetter.ToUpper() + rest.ToLower();
                i++;
            }
            return str;
        }

        #region 时间

        /// <summary>
        /// 字符转时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDateTime(this long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 时间转时间戳（精确到毫秒）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToUnixFromDate(this DateTime value)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(value - startTime).TotalMilliseconds; // 相差毫秒
            return timeStamp;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToStartTime(this DateTime value)
        {
            DateTime dt = DateTime.Now;
            DateTime.TryParse(value.ToString("yyyy-MM-dd") + " 00:00:00", out dt);
            return dt;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToEndTime(this DateTime value)
        {
            DateTime dt = DateTime.Now;
            DateTime.TryParse(value.ToString("yyyy-MM-dd") + " 23:59:59", out dt);
            return dt;
        }

        /// <summary>
        /// 根据当前时间获取这一周的数据
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static List<DateTime> GetWeek(this DateTime now)
        {
            List<DateTime> dtList = new List<DateTime>();
            DateTime someDay = now;
            int wd = (int)someDay.DayOfWeek;
            for (int j = 1 - wd; j < 8 - wd; j++)
            {
                dtList.Add(someDay.AddDays(j));
            }
            return dtList;
        }

        /// <summary>
        /// 获取4季数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<List<DateTime>> GetQuarter(this DateTime dt)
        {
            List<List<DateTime>> result = new List<List<DateTime>>();

            DateTime Start1 = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
            DateTime End1 = Start1.AddMonths(3).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);  //本季度末
            List<DateTime> times1 = new List<DateTime>();
            times1.Add(Start1);
            times1.Add(End1);
            result.Add(times1);

            dt = dt.AddMonths(3);  //本季度末
            DateTime Start2 = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
            DateTime End2 = Start2.AddMonths(3).AddHours(23).AddMinutes(59).AddSeconds(59);  //本季度末
            List<DateTime> times2 = new List<DateTime>();
            times2.Add(Start2);
            times2.Add(End2);
            result.Add(times2);

            dt = dt.AddMonths(3);  //本季度末
            DateTime Start3 = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
            DateTime End3 = Start3.AddMonths(3).AddHours(23).AddMinutes(59).AddSeconds(59); //本季度末
            List<DateTime> times3 = new List<DateTime>();
            times3.Add(Start3);
            times3.Add(End3);
            result.Add(times3);

            dt = dt.AddMonths(3);  //本季度末
            DateTime Start4 = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
            DateTime End4 = Start4.AddMonths(3).AddHours(23).AddMinutes(59).AddSeconds(59); //本季度末
            List<DateTime> times4 = new List<DateTime>();
            times4.Add(Start4);
            times4.Add(End4);
            result.Add(times4);

            return result;
        }

        #endregion 时间

        #region file操作

        /// <summary>
        /// 返回上级目录
        /// </summary>
        /// <param name="path">初始目录</param>
        /// <param name="j">需要返回多少级</param>
        /// <returns>新目录</returns>
        public static string GetSlnPath(this string path, int j = 0)
        {
            for (int i = 0; i < j; i++)
            {
                path = Path.GetDirectoryName(path);
            }
            return path;
        }

        #endregion file操作

        /// <summary>
        /// 替换 Table列明
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static DataTable ReplaceColumnName(this DataTable dt, Dictionary<string, string> pairs)
        {
            if (pairs != null)
            {
                foreach (DataColumn item in dt.Columns)
                {
                    item.ColumnName = pairs[pairs.Keys.FirstOrDefault(x => x == item.ColumnName)];
                }
            }
            return dt;
        }

        #region 映射获取描述

        /// <summary>
        /// 获取类的描述
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns></returns>
        public static string GetDescription(this Type t)
        {
            DescriptionAttribute[] attributes =
                   (DescriptionAttribute[])t.GetCustomAttributes(
                       typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : "";
        }

        /// <summary>
        /// 根据方法名获取描述
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="t">类型</param>
        /// <param name="types">参数类型</param>
        /// <returns></returns>
        public static string GetDescriptionByMethod(this string method, Type t)
        {
            System.Reflection.MethodInfo fi = t.GetMethod(method);
            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : "";
            }
            return "";
        }

        /// <summary>
        /// 根据属性获取描述
        /// </summary>
        /// <param name="method">属性名称</param>
        /// <param name="t">类型</param>
        /// <returns></returns>
        public static string GetDescriptionByProperty(this string property, Type t)
        {
            System.Reflection.PropertyInfo fi = t.GetProperty(property);
            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : "";
            }
            return "";
        }

        #endregion 映射获取描述

        #region 异步循环

        public static void ForEachAction<T>(this List<T> list, Action<T> func)
        {
            foreach (T value in list)
            {
                func(value);
            }
        }

        /// <summary>
        /// 使用异步遍历处理数据
        /// </summary>
        /// <typeparam name="T">需要遍历的基类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="func">Lambda表达式</param>
        /// <returns></returns>
        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (T value in list)
            {
                await func(value);
            }
        }

        #endregion 异步循环
    }
}
