using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaomaUpdater
{
    public class Apis
    {
        public string GetSportsList()
        {
            var client = new RestClient("https://data-lb.tanmasports.com/v1/exercisewx/getStudentWxComboExerciseV2");
            var request = new RestRequest { Method = Method.Post };
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("token", "d69fae14b84caa4c7dc281a6c43cb03c");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("appKey", "e67dc5bd45ff495c");
            request.AddHeader("Referer", "https://servicewechat.com/wxfdc7472438f38f8b/39/page-frame.html");
            request.AddHeader("sign", "AE932C1BD55AB551FD4DA7A36E20B462encodeutf8");
            var body = @"{""childrenId"":1490403}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content ?? "";
        }

        public string UpdateSport(long comboid,long planid,long timesec,string videourl="")
        {
            var client = new RestClient("https://data-lb.tanmasports.com/v1/exercisewx/submitChildrenExercise");
            var request = new RestRequest { Method = Method.Post };
            request.AddHeader("token", "d69fae14b84caa4c7dc281a6c43cb03c");
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
            @"    ""innerSchool"": ""1""," + "\n" +
            @"    ""comboItemId"": "" "+comboid+@" ""," + "\n" +
            @"    ""planId"": "" "+ planid + @" ""," + "\n" +
            @"    ""comboId"": "" "+ comboid + @" ""," + "\n" +
            @"    ""times"": "+ timesec + "," + "\n" +
            @"    ""groupCount"": ""6""," + "\n" +
            @"    ""childrenId"": ""1490403""," + "\n" +
            @"    ""videoUrl"": "" "+videourl+@" ""," + "\n" +
            @"    ""haveVideo"": 1," + "\n" +
            @"    ""notVideoRemark"": """"," + "\n" +
            @"    ""pics"": []" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content ?? "";
        }

        public void SendMessage(string title,string text)
        {
            var client = new RestClient("https://api.day.app/WHkMpzrC5mTnuagU48Cvnm/"+title+"/"+text+"?level=timeSensitive");
            var request = new RestRequest { Method = Method.Post };
            client.Execute(request);
        }
    }
}
