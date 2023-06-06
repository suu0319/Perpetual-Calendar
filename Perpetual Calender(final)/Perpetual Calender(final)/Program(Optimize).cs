namespace Perpetual_calendar
{
    class Program
    {
        //非閏年的各月天數
        static int[] monthsDays = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        //月份
        static string[] months = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        //西元1年各月1日是星期幾
        static int[] dayOfWeekInMonths1AD = new int[] { 0, 1, 4, 4, 0, 2, 5, 0, 3, 6, 1, 4, 6 };
        //輸入年份,閏年數
        static int inputYear, leapYearCount = 0;

        static void Main(string[] args)
        {       
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
            for (int columnStartMonth = 1; columnStartMonth <= 9; columnStartMonth += 4)
            {
                //顯示1~4月 5~8月 9~12月的輸出(4*3)
                for (int month = columnStartMonth; month <= columnStartMonth + 3; month++)
                {
                    //顯示月份(4x3)
                    Console.Write($"{months[month]}月份\t\t\t\t\t\t\t");

                    //每4個月跳行
                    if (month % 4 == 0)
                    {
                        Console.WriteLine("\r\n");
                        OutputLayout(columnStartMonth);
                    }
                }
            }
            Console.Read();
        }

        #region Layout
        static void OutputLayout(int columnStartMonth)
        {
            const int daysCountAWeek = 7, columnCount = 3;

            //顯示星期
            for (int i = 0; i <= 3; i++)
            {
                Console.Write("Sun\tMon\tTue\tWed\tThu\tFri\tSat\t");
            }
            Console.WriteLine("\r\n");

            //日期第一列(1~4月 5~8月 9~12月的日期)
            for (int c1 = columnStartMonth; c1 <= columnStartMonth + 3; c1++)
            {
                //1日前的空格
                for (int d = 1; d <= dayOfWeekInMonths1AD[c1]; d++)
                {
                    Console.Write("\t");
                }
                //跑日期
                for (int d = 1; d <= (daysCountAWeek - dayOfWeekInMonths1AD[c1]); d++)
                {
                    Console.Write($"{d}\t");
                }
            }
            Console.WriteLine("\r\n");

            //日期第二列(1~4月 5~8月 9~12月的日期)
            for (int c2 = columnStartMonth; c2 <= columnStartMonth + columnCount; c2++)
            {
                //跑日期
                for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c2] + 1); d < (daysCountAWeek - dayOfWeekInMonths1AD[c2] + 1 + daysCountAWeek); d++)
                {
                    Console.Write($"{d}\t");
                }
            }
            Console.WriteLine("\r\n");

            //日期第三列(1~4月 5~8月 9~12月的日期)
            for (int c3 = columnStartMonth; c3 <= columnStartMonth + columnCount; c3++)
            {
                //跑日期
                for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c3] + 1 + daysCountAWeek); d < (daysCountAWeek - dayOfWeekInMonths1AD[c3] + 1 + 2 * daysCountAWeek); d++)
                {
                    Console.Write($"{d}\t");
                }
            }
            Console.WriteLine("\r\n");

            //日期第四列(1~4月 5~8月 9~12月的日期)
            for (int c4 = columnStartMonth; c4 <= columnStartMonth + columnCount; c4++)
            {
                if (c4 == 2)
                {
                    //閏年2月+1天(預設為28天)
                    if (((c4 == 2) && (inputYear % 4 == 0) && (inputYear % 100 != 0)) || ((inputYear % 400 == 0) && (inputYear % 3200 != 0)))
                    {
                        monthsDays[c4]++;
                    }

                    //跑日期
                    for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c4] + 1 + 2 * daysCountAWeek); d < (daysCountAWeek - dayOfWeekInMonths1AD[c4] + 1 + 3 * daysCountAWeek); d++)
                    {
                        Console.Write($"{d}\t");
                    }
                }
                else
                {
                    for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c4] + 1 + 2 * daysCountAWeek); d < (daysCountAWeek - dayOfWeekInMonths1AD[c4] + 1 + 3 * daysCountAWeek); d++)
                    {
                        Console.Write($"{d}\t");
                    }
                }
            }
            Console.WriteLine("\r\n");

            //日期第五列(1~4月 5~8月 9~12月的日期)
            for (int c5 = columnStartMonth; c5 <= columnStartMonth + columnCount; c5++)
            {
                //紀錄星期日的日期
                int sunDate = (daysCountAWeek - dayOfWeekInMonths1AD[c5] + 1 + 4 * daysCountAWeek);

                //如果星期日的日期>月份最後一日
                if (sunDate > monthsDays[c5])
                {
                    //跑日期(只到最後一日)
                    for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c5] + 1 + columnCount * daysCountAWeek); d <= monthsDays[c5]; d++)
                    {
                        Console.Write($"{d}\t");
                    }
                    //最後一日後的空格
                    for (int d = 1; d <= (6 - ((monthsDays[c5] + (dayOfWeekInMonths1AD[c5] - 1)) % daysCountAWeek)); d++)
                    {
                        Console.Write("\t");
                    }
                }
                //星期日日期不>月份最後一日
                else
                {
                    //跑日期
                    for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c5] + 1 + columnCount * daysCountAWeek); d < sunDate; d++)
                    {
                        Console.Write($"{d}\t");
                    }
                }
            }
            Console.WriteLine("\r\n");

            //日期第六列
            //1~4月 5~8月 9~12月的日期
            for (int c6 = columnStartMonth; c6 <= columnStartMonth + columnCount; c6++)
            {
                //判斷星期日日期是否<=月份最後一日
                if ((daysCountAWeek - dayOfWeekInMonths1AD[c6] + 1 + 4 * daysCountAWeek) <= monthsDays[c6])
                {
                    //跑日期
                    for (int d = (daysCountAWeek - dayOfWeekInMonths1AD[c6] + 1 + 4 * daysCountAWeek); d <= monthsDays[c6]; d++)
                    {
                        Console.Write($"{d}\t");
                    }
                }

                //最後一日後的空格
                for (int d = 1; d <= (6 - ((monthsDays[c6] + (dayOfWeekInMonths1AD[c6] - 1)) % daysCountAWeek)); d++)
                {
                    Console.Write("\t");
                }

                //最後一日後的空格
                if ((daysCountAWeek - dayOfWeekInMonths1AD[c6] + 1 + 4 * daysCountAWeek) > monthsDays[c6])
                {
                    for (int d = 1; d <= (daysCountAWeek - (6 - (((monthsDays[c6] + (dayOfWeekInMonths1AD[c6] - 1)) % daysCountAWeek)))); d++)
                    {
                        Console.Write("\t");
                    }
                }
            }
            Console.WriteLine("\r\n");
        }
        #endregion
    }
}