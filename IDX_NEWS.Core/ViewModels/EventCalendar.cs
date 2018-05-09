using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDX_NEWS.Core.ViewModels
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using IDX_NEWS.Core.ViewModels;
    //
    //    var eventCalendar = EventCalendar.FromJson(jsonString);


    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EventCalendar
    {
        [JsonProperty("request")]
        public Request Request { get; set; }

        [JsonProperty("ResultCount")]
        public long ResultCount { get; set; }

        [JsonProperty("Results")]
        public Result[] Results { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("AgendaTahun")]
        public string AgendaTahun { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("FinalID")]
        public long FinalId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("Jenis")]
        public Jenis Jenis { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("Step")]
        public Step Step { get; set; }

        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

        [JsonProperty("TglWaktuRups")]
        public DateTimeOffset TglWaktuRups { get; set; }

        [JsonProperty("TglWaktuPE")]
        public DateTimeOffset TglWaktuPe { get; set; }

        [JsonProperty("MonthName")]
        public MonthName MonthName { get; set; }

        [JsonProperty("MonthNumber")]
        public long MonthNumber { get; set; }

        [JsonProperty("Year")]
        public long Year { get; set; }
    }

    public enum Jenis { DireDeviden, Empty, EtfDeviden, Gen, JenisTahunan, LuarBiasa, ObligasiJatuhTempo, PencatatanAwal, Rupo, Tahunan, TahunanDanLuarBiasa };

    public enum MonthName { May };

    public enum Step { Empty, PbrRcn, Pgl, Rcn };

    public partial class EventCalendar
    {
        public static EventCalendar FromJson(string json) => JsonConvert.DeserializeObject<EventCalendar>(json, IDX_NEWS.Core.ViewModels.EventCalendarConverter.Settings);
    }

    public static class EventCalendarSerialize
    {
        public static string ToJson(this EventCalendar self) => JsonConvert.SerializeObject(self, IDX_NEWS.Core.ViewModels.EventCalendarConverter.Settings);
    }

    internal static class EventCalendarConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new JenisConverter(),
                new MonthNameConverter(),
                new StepConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class JenisConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Jenis) || t == typeof(Jenis?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Jenis.Empty;
                case "Luar Biasa":
                    return Jenis.LuarBiasa;
                case "Tahunan":
                    return Jenis.Tahunan;
                case "Tahunan dan Luar Biasa":
                    return Jenis.TahunanDanLuarBiasa;
                case "direDeviden":
                    return Jenis.DireDeviden;
                case "etfDeviden":
                    return Jenis.EtfDeviden;
                case "gen":
                    return Jenis.Gen;
                case "obligasiJatuhTempo":
                    return Jenis.ObligasiJatuhTempo;
                case "pencatatanAwal":
                    return Jenis.PencatatanAwal;
                case "rupo":
                    return Jenis.Rupo;
                case "tahunan":
                    return Jenis.JenisTahunan;
            }
            throw new Exception("Cannot unmarshal type Jenis");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Jenis)untypedValue;
            switch (value)
            {
                case Jenis.Empty:
                    serializer.Serialize(writer, ""); return;
                case Jenis.LuarBiasa:
                    serializer.Serialize(writer, "Luar Biasa"); return;
                case Jenis.Tahunan:
                    serializer.Serialize(writer, "Tahunan"); return;
                case Jenis.TahunanDanLuarBiasa:
                    serializer.Serialize(writer, "Tahunan dan Luar Biasa"); return;
                case Jenis.DireDeviden:
                    serializer.Serialize(writer, "direDeviden"); return;
                case Jenis.EtfDeviden:
                    serializer.Serialize(writer, "etfDeviden"); return;
                case Jenis.Gen:
                    serializer.Serialize(writer, "gen"); return;
                case Jenis.ObligasiJatuhTempo:
                    serializer.Serialize(writer, "obligasiJatuhTempo"); return;
                case Jenis.PencatatanAwal:
                    serializer.Serialize(writer, "pencatatanAwal"); return;
                case Jenis.Rupo:
                    serializer.Serialize(writer, "rupo"); return;
                case Jenis.JenisTahunan:
                    serializer.Serialize(writer, "tahunan"); return;
            }
            throw new Exception("Cannot marshal type Jenis");
        }
    }

    internal class MonthNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MonthName) || t == typeof(MonthName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "May")
            {
                return MonthName.May;
            }
            throw new Exception("Cannot unmarshal type MonthName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (MonthName)untypedValue;
            if (value == MonthName.May)
            {
                serializer.Serialize(writer, "May"); return;
            }
            throw new Exception("Cannot marshal type MonthName");
        }
    }

    internal class StepConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Step) || t == typeof(Step?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Step.Empty;
                case "pbrRcn":
                    return Step.PbrRcn;
                case "pgl":
                    return Step.Pgl;
                case "rcn":
                    return Step.Rcn;
            }
            throw new Exception("Cannot unmarshal type Step");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Step)untypedValue;
            switch (value)
            {
                case Step.Empty:
                    serializer.Serialize(writer, ""); return;
                case Step.PbrRcn:
                    serializer.Serialize(writer, "pbrRcn"); return;
                case Step.Pgl:
                    serializer.Serialize(writer, "pgl"); return;
                case Step.Rcn:
                    serializer.Serialize(writer, "rcn"); return;
            }
            throw new Exception("Cannot marshal type Step");
        }
    }


}
