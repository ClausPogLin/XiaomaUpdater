﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using XiaomaUpdater.Models;
//
//    var updateSport = UpdateSport.FromJson(jsonString);

namespace XiaomaUpdater.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UpdateSport
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("notOverCount")]
        public long NotOverCount { get; set; }
    }

    public partial class UpdateSport
    {
        public static UpdateSport FromJson(string json) => JsonConvert.DeserializeObject<UpdateSport>(json, XiaomaUpdater.Models.Converter.Settings);
    }

}
