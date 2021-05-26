using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpetual_calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            //非閏年的各月天數
            int[] month_day = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            //月份
            string[] month = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            //西元1年各月1日是星期幾
            int[] w = new int[] { 00, 1, 4, 4, 0, 2, 5, 0, 3, 6, 1, 4, 6 };
            //輸入年份,閏年數
            int year, count = 0;
            const int s = 7, t = 3;
            //判斷輸入年份是否有效
            while (true)
            {
                Console.Write("請輸入年份:");
                year = int.Parse(Console.ReadLine());
                //輸入年份>0才會跳出
                if (year > 0)
                    break;
                Console.WriteLine("error");
            }        
            //計算西元1年~輸入年份的閏年數
            for (int i = 1; i <= year; i++)
            {
                if ((i % 4 == 0 && i % 100 != 0) || (i % 400 == 0 && i % 3200 != 0))
                    count++;
            }
            Console.Write("\r\n");
            //計算輸入年份各月1日為星期幾
            for (int k = 1; k <= 12; k++)
            {
                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0 && year % 3200 != 0))
                {
                    if (k <= 2)
                    {
                        //西元1年+輸入年數+閏年數-1(閏年1 2月會多算+1) -1(西元1年)
                        w[k] += (year + count - 1) - 1;
                        w[k] %= 7;

                    }
                    if (k >= 3)
                    {
                        //西元1年+輸入年數+閏年數 -1(西元1年)
                        w[k] += (year + count) - 1;
                        w[k] %= 7;
                    }
                }
                else
                {
                    //西元1年+輸入年數+閏年數 -1(西元1年)
                    w[k] += (year + count) - 1;
                    w[k] %= 7;
                }
            }
            //顯示1~4月 5~8月 9~12月的輸出
            for (int ouput = 1; ouput <= 9; ouput += 4)
            {
                //顯示1~4月 5~8月 9~12月的輸出(4*3)
                for (int m = ouput; m <= ouput + t; m++)
                {
                    //顯示月份(4x3)
                    Console.Write("{0}月份\t\t\t\t\t\t\t", month[m]);
                    //每4個月跳行
                    if (m % 4 == 0)
                    {
                        Console.WriteLine("\r\n");
                        //顯示星期
                        for (int week = ouput; week <= ouput + t; week++)
                        {
                            Console.Write("Sun\tMon\tTue\tWed\tThu\tFri\tSat\t");
                        }
                        Console.WriteLine("\r\n");
                        //日期第一列(1~4月 5~8月 9~12月的日期)
                        for (int c1 = ouput; c1 <= ouput + t; c1++)
                        {
                            switch (c1)
                            {
                                //月份
                                case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12:
                                    //1日前的空格
                                    for (int d = 1; d <= w[c1]; d++)
                                    {
                                        Console.Write("\t");
                                    }
                                    //跑日期
                                    for (int d = 1; d <= (s - w[c1]); d++)
                                        Console.Write("{0}\t", d);
                                    break;
                            }
                        }
                        Console.WriteLine("\r\n");
                        //日期第二列(1~4月 5~8月 9~12月的日期)
                        for (int c2 = ouput; c2 <= ouput + t; c2++)
                        {
                            //跑日期
                            for (int d = (s - w[c2] + 1); d < (s - w[c2] + 1 + s); d++)
                            {
                                Console.Write("{0}\t", d);
                            }
                        }
                        Console.WriteLine("\r\n");
                        //日期第三列(1~4月 5~8月 9~12月的日期)
                        for (int c3 = ouput; c3 <= ouput + t; c3++)
                        {
                            //跑日期
                            for (int d = (s - w[c3] + 1 + s); d < (s - w[c3] + 1 + 2 * s); d++)
                            {
                                Console.Write("{0}\t", d);
                            }
                        }
                        Console.WriteLine("\r\n");
                        //日期第四列(1~4月 5~8月 9~12月的日期)
                        for (int c4 = ouput; c4 <= ouput + t; c4++)
                        {
                            switch (c4)
                            {
                                //2月以外的月份
                                case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12:
                                    //跑日期
                                    for (int d = (s - w[c4] + 1 + 2 * s); d < (s - w[c4] + 1 + 3 * s); d++)
                                    {
                                        Console.Write("{0}\t", d);
                                    }
                                    break;
                                //2月份
                                case 2:
                                    //閏年2月+1天(預設為28天)
                                    if (c4 == 2 && (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0 && year % 3200 != 0))
                                        month_day[c4]++;
                                    //跑日期
                                    for (int d = (s - w[c4] + 1 + 2 * s); d < (s - w[c4] + 1 + 3 * s); d++)
                                    {
                                        Console.Write("{0}\t", d);
                                    }
                                    break;
                            }
                        }
                        Console.WriteLine("\r\n");
                        //日期第五列(1~4月 5~8月 9~12月的日期)
                        for (int c5 = ouput; c5 <= ouput + t; c5++)
                        {
                            //紀錄星期日的日期
                            int num = ((s - w[c5] + 1) + 4 * s);
                            switch (c5)
                            {
                                //月份
                                case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12:
                                    //如果星期日的日期>月份最後一日
                                    if (num > month_day[c5])
                                    {
                                        //跑日期(只到最後一日)
                                        for (int d = (s - w[c5] + 1 + 3 * s); d <= month_day[c5]; d++)
                                        {
                                            Console.Write("{0}\t", d);
                                        }
                                        //最後一日後的空格
                                        for (int d = 1; d <= (6 - (((month_day[c5] + (w[c5] - 1)) % s))); d++)
                                            Console.Write("\t");
                                    }
                                    //星期日日期不>月份最後一日
                                    else
                                    {
                                        //跑日期
                                        for (int d = (s - w[c5] + 1 + 3 * s); d < num; d++)
                                        {
                                            Console.Write("{0}\t", d);
                                        }
                                    }
                                    break;
                            }
                        }
                        Console.WriteLine("\r\n");
                        //日期第六列
                        //1~4月 5~8月 9~12月的日期
                        for (int c6 = ouput; c6 <= ouput + t; c6++)
                        {
                            switch (c6)
                            {
                                //月份
                                case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12:
                                    //判斷星期日日期是否<=月份最後一日
                                    if ((s - w[c6] + 1 + 4 * s) <= month_day[c6])
                                        //跑日期
                                        for (int d = (s - w[c6] + 1 + 4 * s); d <= month_day[c6]; d++)
                                            Console.Write("{0}\t", d);
                                    //最後一日後的空格
                                    for (int d = 1; d <= (6 - (((month_day[c6] + (w[c6] - 1)) % s))); d++)
                                        Console.Write("\t");
                                    //最後一日後的空格
                                    if ((s - w[c6] + 1 + 4 * s) > month_day[c6])
                                        for (int d = 1; d <= (s - (6 - (((month_day[c6] + (w[c6] - 1)) % s)))); d++)
                                            Console.Write("\t");
                                    break;
                            }
                        }
                        Console.WriteLine("\r\n");
                    }
                }
            }
            Console.Read();
        }
    }
}