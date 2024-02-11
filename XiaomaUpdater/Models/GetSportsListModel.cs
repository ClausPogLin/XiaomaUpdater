﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using XiaomaUpdater.Models;
//
//    var getSportsList = GetSportsList.FromJson(jsonString);

namespace XiaomaUpdater.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using JsonConverter = Newtonsoft.Json.JsonConverter;
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;
    using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

    public partial class GetSportsList
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
        [JsonProperty("innerSchool")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long InnerSchool { get; set; }

        [JsonProperty("teacherExerciseStatus")]
        public object TeacherExerciseStatus { get; set; }

        [JsonProperty("exerciseMode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ExerciseMode { get; set; }

        [JsonProperty("simpleModelRemark")]
        public string SimpleModelRemark { get; set; }

        [JsonProperty("simpleFinishStatus")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SimpleFinishStatus { get; set; }

        [JsonProperty("completeCount")]
        public long CompleteCount { get; set; }

        [JsonProperty("needExerciseCount")]
        public long NeedExerciseCount { get; set; }

        [JsonProperty("chooseItem")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ChooseItem { get; set; }

        [JsonProperty("comboId")]
        public long ComboId { get; set; }

        [JsonProperty("list")]
        public List<List> List { get; set; }

        [JsonProperty("otherCityComboList")]
        public object OtherCityComboList { get; set; }

        [JsonProperty("tabooPeople")]
        public object TabooPeople { get; set; }

        [JsonProperty("exercisePre")]
        public object ExercisePre { get; set; }

        [JsonProperty("configList")]
        public object ConfigList { get; set; }
    }

    public partial class List
    {
        [JsonProperty("planId")]
        public long PlanId { get; set; }

        [JsonProperty("comboId")]
        public long? ComboId { get; set; }

        [JsonProperty("planName")]
        public string PlanName { get; set; }

        [JsonProperty("planPhoto")]
        public Uri PlanPhoto { get; set; }

        [JsonProperty("detailPresentation")]
        public string DetailPresentation { get; set; }

        [JsonProperty("teachingVideoPhoto")]
        public Uri TeachingVideoPhoto { get; set; }

        [JsonProperty("defaultPlan")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DefaultPlan { get; set; }

        [JsonProperty("itemLevelName")]
        public string ItemLevelName { get; set; }

        [JsonProperty("isNeedTool")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IsNeedTool { get; set; }

        [JsonProperty("itemOrientationName")]
        public string ItemOrientationName { get; set; }

        [JsonProperty("planDuration")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PlanDuration { get; set; }

        [JsonProperty("participationCount")]
        public long ParticipationCount { get; set; }

        [JsonProperty("isCompleted")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IsCompleted { get; set; }

        [JsonProperty("wasCompleted")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WasCompleted { get; set; }

        [JsonProperty("sex")]
        public object Sex { get; set; }

        [JsonProperty("openStatus")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long OpenStatus { get; set; }

        [JsonProperty("videoId")]
        public long VideoId { get; set; }

        [JsonProperty("videoUrl")]
        public Uri VideoUrl { get; set; }

        [JsonProperty("playGifTime")]
        public long PlayGifTime { get; set; }

        [JsonProperty("planGroupNum")]
        public long PlanGroupNum { get; set; }

        [JsonProperty("comboDescribe")]
        public object ComboDescribe { get; set; }

        [JsonProperty("detailPrepare")]
        public string DetailPrepare { get; set; }

        [JsonProperty("detailFitThrong")]
        public string DetailFitThrong { get; set; }

        [JsonProperty("detailTabooThrong")]
        public string DetailTabooThrong { get; set; }

        [JsonProperty("planMusic")]
        public object PlanMusic { get; set; }

        [JsonProperty("planGif")]
        public object PlanGif { get; set; }
    }

    public partial class GetSportsList
    {
        public static GetSportsList FromJson(string json) => JsonConvert.DeserializeObject<GetSportsList>(json, XiaomaUpdater.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetSportsList self) => JsonConvert.SerializeObject(self, XiaomaUpdater.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
