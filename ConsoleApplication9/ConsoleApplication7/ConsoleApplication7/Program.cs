using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {

            // در این قسمت اطلاعات اولیه دریافت می شود بر اساس راهنما 3 پارامتر تعداد اشیا، جعبه ها و سایز جعبه ها همگی در این قسمت وارد می شوند
            int m, n, k;
            string str = Console.ReadLine();
            n = int.Parse(str.Split(' ')[0]);
            m = Int32.Parse(str.Split(' ')[1]);
            k = Int32.Parse(str.Split(' ')[2]);
            int [] a = new int[n];
            // در این قسمت اندازه هر کدام از اشیا نیز دریافت می شود
            for (int i = 0; i < n; i++)
            {
                Console.Write("enter {0}th number:",i+1);
                a[i] = int.Parse(Console.ReadLine());
            }

            // ـابع  فوق با دریافت اطلاعات اولیه حداکثر تعداد اشیا را به صورت بازگشتی بر می گرداند
            int result = findMaxBox(0, n , n, m, k, a);
            Console.WriteLine(result);
            Console.ReadKey();

        }


        static int findMaxBox(int first,int last,int n ,int m,int k, int [] a)
        {
            // اگر ابتدا و انتهای بررسی از هم رد شدند تابع بازگشتی به پایان می رسد
            if (first > last)
                return int.MinValue;
            // وسط لیست مورد بررسی را پیدا کرده
            int mid = (first + last) / 2;
            // آیا با توجه به عدد پیدا شده می توان جواب مناسبی پیدا کرد؟
            int midMax = isOk(mid,n,m,k,a);
            int max1 = -1;
            int max2= -1;
            // تنها در صورتی الگوریتم به صورت بازگشتی ادامه می یابد که هنوز امکان شکستن لیست اشیا به دو لیست وجود داشته باشد
            if (first < last)
            {
                // اگر وسط لیست انتخاب درستی نبوده باشد لذا قبل از آن هم انتخاب درستی مطمئنا نیست بنابراین با یک مقایسه مشخص می کنیم آیا سمت چپ لیست برای فراخوانی تابع بازگشتی مناسب است یا خیر؟
                if (midMax > 0)
                    max1 = findMaxBox(first, mid - 1, n, m, k, a);
                //اما اگر وسط لیست جواب داده باشد مطمئنا سمت راست لیست مقدار کمتری باز خواهد گرداند لذا تنها در صورتی سمت راست لیست را ادامه می دهیم که وسط لیست جواب نداده باشد
                else
                    max2 = findMaxBox(mid + 1, last, n, m, k, a);
            }
            return Math.Max(Math.Max(max1, max2),midMax);
        }

        static int isOk(int first, int last,int m,int k,int [] a)
        {
            int remSize = k;
            int remBox = m;
            int filledBox = 0;
            int max = -1;
            //یک حلقه وجود دارد از عدد وارد شده به عنوان وسط لیست تا انتهای لیست
            //در این حلقه هر بار سایز اشیا با باقیمانده باکس ها چک شده در صورتی که اندازه باشد از سایز باقیمانده کم در غیر اینصورت 
            //باکس دیگری انتخاب و مراحل تا اخرین شی ادامه می یابد
            for (int j = first; j < last; j++)
            {
                if (a[j] <= remSize)
                {
                    remSize -= a[j];
                    filledBox++;
                    if (j == last - 1)
                    {
                        if (filledBox > max)
                        {
                            max = filledBox;
                            break;
                        }
                    }

                }
                else if (remBox > 1)
                {
                    remBox--;
                    remSize = k;
                    j--;
                    continue;
                }
                else
                {
                    //if (j== n-1 && filledBox > max )
                    //    max = filledBox;
                    break;
                }
            }
            return max;
        }
    }
}
