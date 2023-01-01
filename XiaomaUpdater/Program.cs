using XiaomaUpdater.Models;

namespace XiaomaUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 打印标题
            Console.WriteLine(" __  ___                               _   _           _       _            \r\n \\ \\/ (_) __ _  ___  _ __ ___   __ _  | | | |_ __   __| | __ _| |_ ___ _ __ \r\n  \\  /| |/ _` |/ _ \\| '_ ` _ \\ / _` | | | | | '_ \\ / _` |/ _` | __/ _ \\ '__|\r\n  /  \\| | (_| | (_) | | | | | | (_| | | |_| | |_) | (_| | (_| | ||  __/ |   \r\n /_/\\_\\_|\\__,_|\\___/|_| |_| |_|\\__,_|  \\___/| .__/ \\__,_|\\__,_|\\__\\___|_|   \r\n                                            |_|                             ");
            Console.WriteLine("\nBy:Lzy\n");
            #endregion

            #region 初始化
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleHelper c = new ConsoleHelper();
            Apis a = new Apis();
            c.Output("初始化完成", ConsoleColor.Blue);
            #endregion

            #region 项目列表
            var getSportsList = Models.GetSportsList.FromJson(a.GetSportsList());
            try
            {
                c.Output("Post到" + getSportsList.Response.List.Count + "个项目:", ConsoleColor.Green);
                foreach (var sport in getSportsList.Response.List)
                {
                    c.Output("(type:" + sport.ItemOrientationName + ") " + sport.PlanName, ConsoleColor.White, 1);
                    c.Output("PlanId:" + sport.PlanId + " comboId:" + sport.ComboId, ConsoleColor.DarkGray, 2);
                    c.Output("标准:" + sport.PlayGifTime + "毫秒一组,共" + sport.PlanGroupNum + "组", ConsoleColor.DarkGray, 2);
                    c.Output(sport.DetailPresentation, ConsoleColor.DarkGray, 2);
                }
            }
            catch (Exception ex) 
            {
                c.Output("错误[" + getSportsList.Code + "]已抛出->"+getSportsList.Msg, ConsoleColor.Red);
                c.Output(ex.Message, ConsoleColor.DarkGray,1);
                a.SendMessage("错误[" + getSportsList.Code + "]已抛出->" + getSportsList.Msg, "获取项目列表时发生错误");
                return;
            }
            #endregion

            #region 完成项目
            int errorcount = 0;
            c.Output("所有项目将被标记为有视频，时间为每个项目建议的标准时间", ConsoleColor.Yellow);
            c.Output("开始提交"+getSportsList.Response.List.Count+"个项目", ConsoleColor.Blue);
            foreach (var sport in getSportsList.Response.List)
            {
                updateItem:
                c.Output(@"正提交 """ + sport.PlanName + @"""", ConsoleColor.White, 1);
                var updateSport = Models.UpdateSport.FromJson(a.UpdateSport(sport.ComboId, sport.PlanId, (sport.PlanGroupNum * sport.PlayGifTime)));
                c.Output(@"提交 """ + sport.PlanName + @""":共"+sport.PlanGroupNum+"组,每组耗时"+sport.PlayGifTime+"ms.", ConsoleColor.Green, 1);
                if(updateSport.Msg!="成功")
                {
                    errorcount++;
                    c.Output(@"->" + updateSport.Msg, ConsoleColor.Yellow, 2);
                    a.SendMessage("错误[" + updateSport.Code + "]已抛出->" + updateSport.Msg, @"提交项目"""+sport.PlanName+@"""时发生错误");
                }
                c.Output(@"->"+updateSport.Msg, ConsoleColor.Green, 2);
                Thread.Sleep(2000);
            }
            #endregion
            c.Output("已完成提交,共"+ getSportsList.Response.List.Count+"个项目，错误"+errorcount+"个", ConsoleColor.White);
            a.SendMessage("已完成提交,共" + getSportsList.Response.List.Count + "个项目，错误" + errorcount + "个", DateTime.Now.ToString("s"));
        }

    }

    public class ConsoleHelper
    {
        public void Output(string message, ConsoleColor color = ConsoleColor.White, int treeLevel = 0)
        {
            Console.ForegroundColor = color;
            string treeLevelTab = "";
            if (treeLevel > 0)
            {
                for (int i = 0; i < treeLevel; i++)
                {
                    treeLevelTab += "   ";
                }
            }

            Console.WriteLine("[ " + DateTime.Now + " ] " + treeLevelTab + message );
        }
    }
}