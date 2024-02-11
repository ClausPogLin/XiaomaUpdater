using System.CommandLine;
using System.Threading;
using System.Windows;
using XiaomaUpdater.Users;

namespace XiaomaUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 打印标题
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(" __  ___                               _   _           _       _            \r\n \\ \\/ (_) __ _  ___  _ __ ___   __ _  | | | |_ __   __| | __ _| |_ ___ _ __ \r\n  \\  /| |/ _` |/ _ \\| '_ ` _ \\ / _` | | | | | '_ \\ / _` |/ _` | __/ _ \\ '__|\r\n  /  \\| | (_| | (_) | | | | | | (_| | | |_| | |_) | (_| | (_| | ||  __/ |   \r\n /_/\\_\\_|\\__,_|\\___/|_| |_| |_|\\__,_|  \\___/| .__/ \\__,_|\\__,_|\\__\\___|_|   \r\n                                            |_|                             ");
            Console.WriteLine("\nBy:Lzy in 2023 ,Last built :2023年8月27日15点31分\n");
            #endregion

            #region 初始化
            ConsoleHelper c = new ConsoleHelper();//console helper
            List<Users.Users.User> users = new List<Users.Users.User>();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);//目前看来是毛线用都没有
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            #region 读取本地User文件
            try
            {
                users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users.Users.User>>(File.ReadAllText(System.Environment.CurrentDirectory + @"\users.json"));
                c.Output("读取" + System.Environment.CurrentDirectory + @"\users.json" + " :", ConsoleColor.Gray);
                c.Output(File.ReadAllText(System.Environment.CurrentDirectory + @"\users.json"), ConsoleColor.DarkGray, 1);
            }
            catch (Exception ex)
            {
                c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                c.Output(ex.Message, ConsoleColor.DarkGray, 1);
            }
            #endregion

            #region Debug处理
            if (args.Length != 0)
            {
                if (args[0] == "UserEditMode=true")
                {
                    c.Output("", ConsoleColor.Yellow);
                    c.Output("UserEditMode On", ConsoleColor.White);
                    c.Output("", ConsoleColor.Yellow);
                    for (var i = 0; i < users.Count; i++)
                    {
                        c.Output("[" + i + "] : " + users[i].name, ConsoleColor.Yellow, 1);
                        c.Output("# studentid : " + users[i].studentid, ConsoleColor.DarkYellow, 2);
                        c.Output("# token : " + users[i].token, ConsoleColor.DarkYellow, 2);
                        c.Output("# req_sign : " + users[i].req_sign, ConsoleColor.DarkYellow, 2);
                        c.Output("# bark_id : " + users[i].bark_id, ConsoleColor.DarkYellow, 2);
                        c.Output("# is_expired : " + users[i].is_expired, ConsoleColor.DarkYellow, 2);
                        /*if (users[i].vid_urls != null)
                        {
                            c.Output("# videos : " + users[i].vid_urls.Count + " video(s)", ConsoleColor.DarkYellow, 2);
                            for (var j = 0; j < users[i].vid_urls.Count; j++)
                            {
                                c.Output("[" + j + "] planid:" + users[i].vid_urls[j].planid +" url:"+ users[i].vid_urls[j].url, ConsoleColor.DarkYellow, 3);
                            }
                        }*/
                    }
                read:
                    c.Output("-edit -", ConsoleColor.Yellow, 0, false);
                    string input = Console.ReadLine();
                    if (input != null)
                    {
                        switch (input)
                        {
                            #region list
                            case "list":
                                try
                                {
                                    List<Users.Users.User> d_users = new List<Users.Users.User>();
                                    d_users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users.Users.User>>(File.ReadAllText(System.Environment.CurrentDirectory + @"\users.json"));
                                    for (var i = 0; i < d_users.Count; i++)
                                    {
                                        c.Output("[" + i + "] : " + d_users[i].name, ConsoleColor.Yellow, 1);
                                        c.Output("# studentid : " + d_users[i].studentid, ConsoleColor.DarkYellow, 2);
                                        c.Output("# token : " + d_users[i].token, ConsoleColor.DarkYellow, 2);
                                        c.Output("# req_sign : " + d_users[i].req_sign, ConsoleColor.DarkYellow, 2);
                                        c.Output("# bark_id : " + d_users[i].bark_id, ConsoleColor.DarkYellow, 2);
                                        c.Output("# is_expired : " + d_users[i].is_expired, ConsoleColor.DarkYellow, 2);
                                        /*if (d_users[i].vid_urls != null)
                                        {
                                            c.Output("# videos : " + d_users[i].vid_urls.Count + " video(s)", ConsoleColor.DarkYellow, 2);
                                            for (var j = 0; j < d_users[i].vid_urls.Count; j++)
                                            {
                                                c.Output("[" + j + "] planid:" + d_users[i].vid_urls[j].planid+" url:" + users[i].vid_urls[j].url, ConsoleColor.DarkYellow, 3);
                                            }
                                        }*/
                                    }
                                }
                                catch (Exception ex)
                                {
                                    c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                                    c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                                }
                                break;
                            #endregion
                            #region remove
                            case "remove":
                                c.Output("输入编号: ", ConsoleColor.Yellow, 0, false);
                                try
                                {
                                    int i_input = Convert.ToInt32(Console.ReadLine());
                                    users.RemoveAt(i_input);
                                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                                    File.WriteAllText(System.Environment.CurrentDirectory + @"\users.json", json);
                                    c.Output("已删除", ConsoleColor.Green);
                                }
                                catch (Exception ex)
                                {
                                    c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                                    c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                                }
                                break;
                            #endregion
                            #region add
                            case "add":
                                Users.Users.User newuser = new Users.Users.User();

                                c.Output("输入姓名: ", ConsoleColor.Yellow, 0, false);
                                string i_name = Console.ReadLine();
                                newuser.name = i_name;

                                c.Output("输入studentid: ", ConsoleColor.Yellow, 0, false);
                                string i_stuid = Console.ReadLine();
                                newuser.studentid = Convert.ToInt32(i_stuid);

                                c.Output("输入token: ", ConsoleColor.Yellow, 0, false);
                                string i_token = Console.ReadLine();
                                newuser.token = i_token;

                                c.Output("输入req_sign: ", ConsoleColor.Yellow, 0, false);
                                string i_req_sign = Console.ReadLine();
                                newuser.req_sign = i_req_sign;

                                c.Output("输入bark_id: ", ConsoleColor.Yellow, 0, false);
                                string i_bark_id = Console.ReadLine();
                                newuser.bark_id = i_bark_id;

                                users.Add(newuser);

                                try
                                {
                                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                                    File.WriteAllText(System.Environment.CurrentDirectory + @"\users.json", json);
                                    c.Output("已添加", ConsoleColor.Green);
                                }
                                catch (Exception ex)
                                {
                                    c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                                    c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                                }

                                break;
                            #endregion
                            #region update
                            case "update":
                                c.Output("输入“-”则为不变", ConsoleColor.Yellow, 0);
                                c.Output("输入编号: ", ConsoleColor.Yellow, 0, false);
                                Users.Users.User up_user = new Users.Users.User();
                                int i_updateindex;
                                try
                                {
                                    i_updateindex = Convert.ToInt32(Console.ReadLine());
                                    up_user = users[i_updateindex];
                                }
                                catch (Exception ex)
                                {
                                    c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                                    c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                                    goto read;
                                }

                                c.Output("输入姓名: ", ConsoleColor.Yellow, 0, false);
                                string i_up_name = Console.ReadLine();
                                if (i_up_name != "-")
                                    up_user.name = i_up_name;

                                c.Output("输入studentid: ", ConsoleColor.Yellow, 0, false);
                                string i_up_stuid = Console.ReadLine();
                                if (i_up_stuid != "-")
                                    up_user.studentid = Convert.ToInt32(i_up_stuid);

                                c.Output("输入token: ", ConsoleColor.Yellow, 0, false);
                                string i_up_token = Console.ReadLine();
                                if (i_up_token != "-")
                                    up_user.token = i_up_token;

                                c.Output("输入req_sign: ", ConsoleColor.Yellow, 0, false);
                                string i_up_req_sign = Console.ReadLine();
                                if (i_up_req_sign != "-")
                                    up_user.req_sign = i_up_req_sign;

                                c.Output("输入bark_id: ", ConsoleColor.Yellow, 0, false);
                                string i_up_bark_id = Console.ReadLine();
                                if (i_up_bark_id != "-")
                                    up_user.bark_id = i_up_bark_id;

                                /*c.Output("输入vid_urls: ", ConsoleColor.Yellow, 0, false);
                                 string i_up_vid_urls = Console.ReadLine();
                                 if (i_up_vid_urls != "-")
                                 {

                                 }
                                 List<string> vid_input = new List<string>();
                                 int vid_input_len = 0;
                                 while (true)
                                 {
                                     string item_input = Console.ReadLine();
                                     if (item_input.Equals("q") == false) //如果输入的不是q(区分大小写)，则增加记录
                                         vid_input.Insert(vid_input_len++, item_input);
                                     else
                                         break;
                                 }*/

                                users[i_updateindex] = up_user;

                                try
                                {
                                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                                    File.WriteAllText(System.Environment.CurrentDirectory + @"\users.json", json);
                                    c.Output("已更新", ConsoleColor.Green);
                                }
                                catch (Exception ex)
                                {
                                    c.Output("内部错误[" + ex.Message + "]已抛出->" + ex.ToString(), ConsoleColor.Red);
                                    c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                                }

                                break;
                            #endregion
                            case "exit":
                                goto run;
                                break;
                        }
                        goto read;
                    }
                    else
                        goto read;
                }
            }
            #endregion

            run:

            c.Output("初始化完成,写入" + users.Count + "个用户:", ConsoleColor.Blue);
            foreach (Users.Users.User user in users)
            {
                c.Output("[" + user.name + "] :", ConsoleColor.DarkBlue, 1);
                c.Output("# studentid : " + user.studentid, ConsoleColor.DarkBlue, 2);
                c.Output("# token : " + user.token, ConsoleColor.DarkBlue, 2);
                c.Output("# req_sign : " + user.req_sign, ConsoleColor.DarkBlue, 2);
                c.Output("# bark_id : " + user.bark_id, ConsoleColor.DarkBlue, 2);
            }
            #endregion

            for (var i = 0; i < users.Count; i++)
            {
                users[i] = c.SubBlock(users[i]);
                if (users[i].is_expired == true)
                    c.Output("正在写文件到 " + System.Environment.CurrentDirectory + @"\users.json" + " ," + users[i].name + "的token已过期", ConsoleColor.Red);
                else
                    c.Output("正在写文件到 " + System.Environment.CurrentDirectory + @"\users.json" + " ," + users[i].name + "的token正常", ConsoleColor.Green);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                File.WriteAllText(System.Environment.CurrentDirectory + @"\users.json", json);
                c.Output("已写入", ConsoleColor.Green);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ConsoleHelper c = new ConsoleHelper();//console helper
            c.Output("内部错误已抛出->", ConsoleColor.Red);
            c.Output(e.ToString(), ConsoleColor.DarkGray, 1);
        }
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            
        }
    }

    public class ConsoleHelper
    {
        public Users.Users.User SubBlock(Users.Users.User user)
        {
            #region 初始化
            Apis a = new Apis();
            if (user.is_expired==true)
            {
                a.SendMessage("已被覆盖登录，请联系更新token", "只要不被获取token的设备覆盖登录，token就不会失效，请联系以更新token。", user.bark_id);
                return user;
            }
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleHelper c = new ConsoleHelper();
            c.Output("初始化完成", ConsoleColor.Blue);
            #endregion

            #region 项目列表
            var getSportsList = Models.GetSportsList.FromJson(a.GetSportsList(user.studentid,user.token));
            try
            {
                c.Output("Post到" + getSportsList.Response.List.Count + "个项目:", ConsoleColor.Green);
                foreach (var sport in getSportsList.Response.List)
                {
                    c.Output("(type:" + sport.ItemOrientationName + ") " + sport.PlanName, ConsoleColor.White, 1);
                    c.Output("PlanId:" + sport.PlanId + " comboId:" + getSportsList.Response.ComboId, ConsoleColor.DarkGray, 2);
                    c.Output("标准:" + sport.PlayGifTime + "毫秒一组,共" + sport.PlanGroupNum + "组", ConsoleColor.DarkGray, 2);
                    c.Output(sport.DetailPresentation, ConsoleColor.DarkGray, 2);
                }
            }
            catch (Exception ex)
            {
                c.Output("错误[" + getSportsList.Code + "]已抛出->" + getSportsList.Msg, ConsoleColor.Red);
                c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                a.SendMessage("错误[" + getSportsList.Code + "]已抛出->" + getSportsList.Msg, "获取项目列表时发生错误", user.bark_id);
                switch (getSportsList.Code)
                {
                    case 40012://验证签名失败，签名结果有误
                        user.is_expired = true; break;
                    case 40011://验证签名失败，签名字符串缺失
                        user.is_expired = true; break;
                    case 30005://请求摘要认证失败
                        user.is_expired = true;
                        a.SendMessage("已被覆盖登录，请联系更新token", "只要不被获取token的设备覆盖登录，token就不会失效，请联系以更新token。", user.bark_id);
                        break;
                }
                return user;
            }
            #endregion

            #region 完成项目
            int errorcount = 0;
            c.Output("所有项目将被标记为有视频，时间为每个项目建议的标准时间", ConsoleColor.Yellow);
            c.Output("开始提交" + getSportsList.Response.List.Count + "个项目", ConsoleColor.Blue);
            foreach (var sport in getSportsList.Response.List)
            {
                c.Output(@"正提交 """ + sport.PlanName + @"""", ConsoleColor.White, 1);
                //上传的视频url指向空,永远加载不出来:https://tanma-teacher-video-online.tanmasports.com/parent/video/2023/02/25/hnitsP_1677294439305.mp4
                var updateSport = Models.UpdateSport.FromJson(a.UpdateSport(user.studentid,getSportsList.Response.ComboId, sport.PlanId, (sport.PlanGroupNum * sport.PlayGifTime),sport.PlanGroupNum, "https://tanma-teacher-video-online.tanmasports.com/parent/video/2023/02/25/hnitsP_1677294439305.mp4", user.token));
                c.Output(@"提交 """ + sport.PlanName + @""":共" + sport.PlanGroupNum + "组,每组耗时" + sport.PlayGifTime + "ms.", ConsoleColor.Green, 1);
                if (updateSport.Msg != "成功")
                {
                    errorcount++;
                    c.Output(@"->" + updateSport.Msg, ConsoleColor.Yellow, 2);
                    a.SendMessage("错误[" + updateSport.Code + "]已抛出->" + updateSport.Msg, @"提交项目""" + sport.PlanName + @"""时发生错误", user.bark_id);
                }
                c.Output(@"->" + updateSport.Msg, ConsoleColor.Green, 2);
                Thread.Sleep(2000);
            }
            #endregion

            #region 复查项目列表
            c.Output("回溯检查...", ConsoleColor.DarkGreen);
            var checkSportsList = Models.GetSportsList.FromJson(a.GetSportsList(user.studentid, user.token));
            try
            {
                c.Output("Post到" + checkSportsList.Response.List.Count + "个项目:", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                c.Output("错误[" + checkSportsList.Code + "]已抛出->" + checkSportsList.Msg, ConsoleColor.Red);
                c.Output(ex.Message, ConsoleColor.DarkGray, 1);
                a.SendMessage("错误[" + checkSportsList.Code + "]已抛出->" + checkSportsList.Msg, "获取项目列表时发生错误", user.bark_id);
                switch (checkSportsList.Code)
                {
                    case 40012://验证签名失败，签名结果有误
                        user.is_expired = true; break;
                    case 40011://验证签名失败，签名字符串缺失
                        user.is_expired = true; break;
                    case 30005://请求摘要认证失败
                        user.is_expired = true; break;
                }
                return user;
            }
            #endregion

            c.Output("已完成提交" + user.name + "的任务,共" + getSportsList.Response.List.Count + "个，错误" + errorcount + "个", ConsoleColor.White);
            c.Output("", ConsoleColor.White);
            c.Output("本项已完成", ConsoleColor.Green);
            c.Output("", ConsoleColor.White);
            a.SendMessage("“Updater”集中上载数据情報", "已完成同歩共" + getSportsList.Response.List.Count + "項目，錯誤" + errorcount + "項目。" + "%0a回溯情報：同歩番号%23" + checkSportsList.Response.CompleteCount+ "，総計" + checkSportsList.Response.NeedExerciseCount+ "次。", user.bark_id);
            return user;
        }
        public void Output(string message, ConsoleColor color = ConsoleColor.White, int treeLevel = 0,bool newline = true)
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
            if (newline == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(DateTime.Now);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("] ");
                Console.ForegroundColor = color;
                Console.WriteLine( treeLevelTab + message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(DateTime.Now);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("] ");
                Console.ForegroundColor = color;
                Console.Write(treeLevelTab + message);
            }
        }
    }
}