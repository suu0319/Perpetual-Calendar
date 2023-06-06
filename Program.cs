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
            int[] monthsDays = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            //月份
            string[] months = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            //西元1年各月1日是星期幾
            int[] dayOfWeekInMonths1AD = new int[] { 0, 1, 4, 4, 0, 2, 5, 0, 3, 6, 1, 4, 6 };
            //輸入年份,閏年數
            int inputYear, leapYearCount = 0;
            const int s = 7;
            //判斷輸入年份是否有效
            while (true)
            {
                Console.Write("請輸入年份:");
                inputYear = int.Parse(Console.ReadLine());
                //輸入年份>0才會跳出
                if (inputYear > 0)
                {
                    break;
                }

                Console.WriteLine("error");
            }


            //計算西元1年~輸入年份的閏年數
            for (int i = 1; i <= inputYear; i++)
            {
                if (((i % 4 == 0) && (i % 100 != 0)) || ((i % 400 == 0) && (i % 3200 != 0)))
                {
                    leapYearCount++;
                }
            }
            Console.WriteLine("\r\n");

            //計算輸入年份各月1日為星期幾
            for (int i = 1; i <= 12; i++)
            {
                //西元1年+輸入年數+閏年數 -1(西元1年)
                dayOfWeekInMonths1AD[i] += (inputYear + leapYearCount) - 1;

                if (i <= 2)
                {
                    //西元1年+輸入年數+閏年數-1(閏年1 2月會多算+1) -1(西元1年)
                    dayOfWeekInMonths1AD[i] -= 1;
                }

                dayOfWeekInMonths1AD[i] %= 7;
            }

            //顯示1~4月 5~8月 9~12月的輸出
            for (int columnMonth = 1; columnMonth <= 9; columnMonth += 4)
            {
                //顯示1~4月 5~8月 9~12月的輸出(4*3)
                for (int month = columnMonth; month <= columnMonth + 3; month++)
                {
                    //顯示月份(4x3)
                    Console.Write($"{months[month]}月份\t\t\t\t\t\t\t");

                    //每4個月跳行
                    if (month % 4 == 0)
                    {
                        Console.WriteLine("\r\n");

                        //顯示星期
                        for (int i = 0; i <= 3; i++)
                        {
                            Console.Write("Sun\tMon\tTue\tWed\tThu\tFri\tSat\t");
                        }
                        Console.WriteLine("\r\n");

                        //日期第一列(1~4月 5~8月 9~12月的日期)
                        for (int c1 = columnMonth; c1 <= columnMonth + 3; c1++)
                        {
                            //1日前的空格
                            for (int d = 1; d <= dayOfWeekInMonths1AD[c1]; d++)
                            {
                                Console.Write("\t");
                            }
                            //跑日期
                            for (int d = 1; d <= (s - dayOfWeekInMonths1AD[c1]); d++)
                            {
                                Console.Write($"{d}\t");
                            }
                        }
                        Console.WriteLine("\r\n");

                        //日期第二列(1~4月 5~8月 9~12月的日期)
                        for (int c2 = columnMonth; c2 <= columnMonth + 3; c2++)
                        {
                            //跑日期
                            for (int d = (s - dayOfWeekInMonths1AD[c2] + 1); d < (s - dayOfWeekInMonths1AD[c2] + 1 + s); d++)
                            {
                                Console.Write($"{d}{0}\t");
                            }
                        }
                        Console.WriteLine("\r\n");

                        //日期第三列(1~4月 5~8月 9~12月的日期)
                        for (int c3 = columnMonth; c3 <= columnMonth + 3; c3++)
                        {
                            //跑日期
                            for (int d = (s - dayOfWeekInMonths1AD[c3] + 1 + s); d < (s - dayOfWeekInMonths1AD[c3] + 1 + 2 * s); d++)
                            {
                                Console.Write($"{d}{0}\t");
                            }
                        }
                        Console.WriteLine("\r\n");

                        //日期第四列(1~4月 5~8月 9~12月的日期)
                        for (int c4 = columnMonth; c4 <= columnMonth + 3; c4++)
                        {
                            if (c4 == 2)
                            {
                                //閏年2月+1天(預設為28天)
                                if (((c4 == 2) && (inputYear % 4 == 0) && (inputYear % 100 != 0)) || ((inputYear % 400 == 0) && (inputYear % 3200 != 0)))
                                {
                                    monthsDays[c4]++;
                                }
         
                                //跑日期
                                for (int d = (s - dayOfWeekInMonths1AD[c4] + 1 + 2 * s); d < (s - dayOfWeekInMonths1AD[c4] + 1 + 3 * s); d++)
                                {
                                    Console.Write($"{d}{0}\t");
                                }
                            }
                            else
                            {
                                for (int d = (s - dayOfWeekInMonths1AD[c4] + 1 + 2 * s); d < (s - dayOfWeekInMonths1AD[c4] + 1 + 3 * s); d++)
                                {
                                    Console.Write($"{d}{0}\t");
                                }
                            }
                        }
                        Console.WriteLine("\r\n");

                        //日期第五列(1~4月 5~8月 9~12月的日期)
                        for (int c5 = columnMonth; c5 <= columnMonth + 3; c5++)
                        {
                            //紀錄星期日的日期
                            int sunDate = (s - dayOfWeekInMonths1AD[c5] + 1 + 4 * s);

                            //如果星期日的日期>月份最後一日
                            if (sunDate > monthsDays[c5])
                            {
                                //跑日期(只到最後一日)
                                for (int d = (s - dayOfWeekInMonths1AD[c5] + 1 + 3 * s); d <= monthsDays[c5]; d++)
                                {
                                    Console.Write($"{d}{0}\t");
                                }
                                //最後一日後的空格
                                for (int d = 1; d <= (6 - ((monthsDays[c5] + (dayOfWeekInMonths1AD[c5] - 1)) % s)); d++)
                                {
                                    Console.Write("\t");
                                }
                            }
                            //星期日日期不>月份最後一日
                            else
                            {
                                //跑日期
                                for (int d = (s - dayOfWeekInMonths1AD[c5] + 1 + 3 * s); d < sunDate; d++)
                                {
                                    Console.Write($"{d}{0}\t");
                                }
                            }
                        }
                        Console.WriteLine("\r\n");

                        //日期第六列
                        //1~4月 5~8月 9~12月的日期
                        for (int c6 = columnMonth; c6 <= columnMonth + 3; c6++)
                        {
                            //判斷星期日日期是否<=月份最後一日
                            if ((s - dayOfWeekInMonths1AD[c6] + 1 + 4 * s) <= monthsDays[c6])
                            {
                                //跑日期
                                for (int d = (s - dayOfWeekInMonths1AD[c6] + 1 + 4 * s); d <= monthsDays[c6]; d++)
                                {
                                    Console.Write($"{d}{0}\t");
                                }
                            }

                            //最後一日後的空格
                            for (int d = 1; d <= (6 - ((monthsDays[c6] + (dayOfWeekInMonths1AD[c6] - 1)) % s)); d++)
                            {
                                Console.Write("\t");
                            }
          
                            //最後一日後的空格
                            if ((s - dayOfWeekInMonths1AD[c6] + 1 + 4 * s) > monthsDays[c6])
                            {
                                for (int d = 1; d <= (s - (6 - (((monthsDays[c6] + (dayOfWeekInMonths1AD[c6] - 1)) % s)))); d++)
                                {
                                    Console.Write("\t");
                                }
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