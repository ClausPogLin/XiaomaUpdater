using Newtonsoft.Json;
using RestSharp;
using XiaomaUpdater.Models;
using static XiaomaUpdater.Users.Users;

namespace XiaomaUpdater
{
    public class Apis
    {
        /// <summary>
        /// 获取运动列表
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="token"></param>
        /// <param name="req_sign"></param>
        /// <returns></returns>
        public string GetSportsList(long studentid,string token = "d69fae14b84caa4c7dc281a6c43cb03c")
        {
            ConsoleHelper c = new ConsoleHelper();
            SignBuilder signBuilder = new SignBuilder();

            var client = new RestClient("https://data-lb.tanmasports.com/v1/exercisewx/getStudentWxComboExerciseV2");
            var request = new RestRequest { Method = Method.Post };
            var body = @"{""childrenId"":" + studentid + "}";
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("appKey", "e67dc5bd45ff495c");
            request.AddHeader("Referer", "https://servicewechat.com/wxfdc7472438f38f8b/39/page-frame.html");
            request.AddHeader("sign", signBuilder.GetSign(body));

            c.Output(signBuilder.GetSign(body), ConsoleColor.Gray, 0);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content ?? "";
        }

        public string UpdateSport(long studentid,long comboid, long planid, long timesec, long groupcount, string videourl = "", string token = "d69fae14b84caa4c7dc281a6c43cb03c")
        {
            var client = new RestClient("https://data-lb.tanmasports.com/v1/exercisewx/submitChildrenExercise");
            var request = new RestRequest { Method = Method.Post };
            request.AddHeader("token", token);
            request.AddHeader("Content-Type", "application/json");
            
            var body = @"{" + "\n" +
            @"    ""innerSchool"": ""1""," + "\n" +
            @"    ""comboItemId"": """ + comboid + @"""," + "\n" +
            @"    ""planId"": " + planid + @"," + "\n" +
            @"    ""comboId"": " + comboid + @"," + "\n" +
            @"    ""times"": " + timesec + "," + "\n" +
            @"    ""groupCount"": """+ groupcount + @"""," + "\n" +
            @"    ""childrenId"": "+studentid+@"," + "\n" +
            @"    ""videoUrl"": """ + videourl + @"""," + "\n" +
            @"    ""haveVideo"": ""1""," + "\n" +
            @"    ""notVideoRemark"": """"," + "\n" +
            @"    ""pics"": []" + ",\n" +
            @"    ""inputType"": ""1""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content ?? "";
        }

        public void SendMessage(string title, string text,string bark_id)
        {
            var client = new RestClient("https://api.day.app/"+bark_id+"/" + title + "/" + text + "?sound=paymentsuccess&level=timeSensitive&icon=https://i.hd-r.cn/b0962629a4c3b50bfa96215a68878f10.png");
            var request = new RestRequest { Method = Method.Post };
            client.Execute(request);
            ConsoleHelper c = new ConsoleHelper();
            c.Output("已向" + bark_id + @"传送"""+title+@"""", ConsoleColor.Green);
            c.Output(@""""+text+@"""", ConsoleColor.DarkGreen,1);
        }

        private string ConvertJsonString(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}

