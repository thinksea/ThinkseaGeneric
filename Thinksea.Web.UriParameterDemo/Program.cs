using System;

namespace Thinksea.UriParameterDemo
{
    class Program
    {
        static int maxCount = 1000000;

        static void getParam(string url, string name)
        {
            var value = Thinksea.Web.GetUriParameter(url, name);
            if (value == null)
            {
                Console.WriteLine("< @" + name + " > **null**");
            }
            else
            {
                Console.WriteLine("< @" + name + " > " + value);
            }
        }
        static string setParam(string url, string name, string value)
        {
            url = Thinksea.Web.SetUriParameter(url, name, value);
            Console.WriteLine("< +" + name + " > " + url);
            return url;
        }
        static string removeParam(string url, string name)
        {
            url = Thinksea.Web.RemoveUriParameter(url, name);
            Console.WriteLine("< -" + name + " > " + url);
            return url;
        }

        static string clearParam(string url)
        {
            url = Thinksea.Web.ClearUriParameter(url, true);
            Console.WriteLine("< -* > " + url);
            return url;
        }

        static void call()
        {
            Console.WriteLine("正在进行功能测试……指令说明{@提取参数的值，+增加参数，-删除参数，-*删除全部参数}");
            System.DateTime begin = System.DateTime.Now;

            //string url = "http://www.thinksea.com/";
            string url = "http://www.thinksea.com/default.aspx?p3=#mark1";
            //string url = "http://www.thinksea.com/default.aspx?p3#mark1#mark2";
            //string url = "#mark1#mark2";
            //string url = "?#mark1#mark2";
            //string url = "?";
            //string url = "";
            Console.WriteLine(url);

            url = setParam(url, "p1", "value1");
            url = setParam(url, "p2", "value2");
            url = setParam(url, "p3", null);
            url = setParam(url, "p3", "value3");
            url = setParam(url, "p4", "https://www.163.com/show.aspx?id=1&p=zzl&林夕#mark3");
            url = setParam(url, "p5", null);

            getParam(url, "P1");
            getParam(url, "p2");
            getParam(url, "p3");
            getParam(url, "p4");
            getParam(url, "p5");
            getParam(url, "noparam");

            url = removeParam(url, "p1");
            url = removeParam(url, "p4");
            url = removeParam(url, "p5");
            url = removeParam(url, "noparam");

            url = clearParam(url);

            System.DateTime end = System.DateTime.Now;
            Console.WriteLine("------------------执行时间：" + (end - begin));
        }

        static void test()
        {
            Console.WriteLine("正在进行效率测试……");
            System.DateTime begin = System.DateTime.Now;

            for (int i = 0; i < maxCount; i++)
            {
                //string url = "http://www.thinksea.com/";
                string url = "http://www.thinksea.com/default.aspx?p3#mark1#mark2";

                url = Thinksea.Web.SetUriParameter(url, "p1", "value1");
                url = Thinksea.Web.SetUriParameter(url, "p2", "value2");
                url = Thinksea.Web.SetUriParameter(url, "p3", "");
                url = Thinksea.Web.SetUriParameter(url, "r", "https://www.163.com/show.aspx?id=1&p=zzl&林夕#mark3");
                url = Thinksea.Web.RemoveUriParameter(url, "noparam");

                Thinksea.Web.GetUriParameter(url, "p1");
                Thinksea.Web.GetUriParameter(url, "p2");
                Thinksea.Web.GetUriParameter(url, "p3");
                Thinksea.Web.GetUriParameter(url, "r");
                Thinksea.Web.GetUriParameter(url, "noparam");

                url = Thinksea.Web.RemoveUriParameter(url, "p1");
                url = Thinksea.Web.RemoveUriParameter(url, "r");

                url = Thinksea.Web.ClearUriParameter(url, true);

            }
            System.DateTime end = System.DateTime.Now;
            Console.WriteLine(string.Format("------------------测试 {0} 次，耗费时间 {1}，平均每秒 {2} 次", maxCount, end - begin, maxCount / (end - begin).TotalSeconds));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("说明：本程序仅对 URI 参数处理方法进行功能和效率测试");

            call();

            test();

            Console.WriteLine("全部完成，按任意键结束程序……");
            Console.ReadKey();
        }
    }
}
