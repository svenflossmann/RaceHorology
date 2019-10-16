﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSVAlpin2Lib
{

  public class RaceParticipantConverter : JsonConverter<RaceParticipant>
  {
    public override void WriteJson(JsonWriter writer, RaceParticipant value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("Id");
      writer.WriteValue(value.Id);
      writer.WritePropertyName("StartNumber");
      writer.WriteValue(value.StartNumber);
      writer.WritePropertyName("Name");
      writer.WriteValue(value.Name);
      writer.WritePropertyName("Firstname");
      writer.WriteValue(value.Firstname);
      writer.WritePropertyName("Sex");
      writer.WriteValue(value.Sex);
      writer.WritePropertyName("Year");
      writer.WriteValue(value.Year);
      writer.WritePropertyName("Club");
      writer.WriteValue(value.Club);
      writer.WritePropertyName("Nation");
      writer.WriteValue(value.Nation);
      writer.WritePropertyName("Class");
      writer.WriteValue(value.Class.ToString());
      writer.WritePropertyName("Group");
      writer.WriteValue(value.Class.Group.ToString());
      writer.WriteEndObject();
    }

    public override RaceParticipant ReadJson(JsonReader reader, Type objectType, RaceParticipant existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }

  public class RunResultWPConverter : JsonConverter<RunResultWithPosition>
  {
    public override void WriteJson(JsonWriter writer, RunResultWithPosition value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("Id");
      writer.WriteValue(value.Id);
      writer.WritePropertyName("Position");
      writer.WriteValue(value.Position);
      writer.WritePropertyName("StartNumber");
      writer.WriteValue(value.StartNumber);
      writer.WritePropertyName("Name");
      writer.WriteValue(value.Name);
      writer.WritePropertyName("Firstname");
      writer.WriteValue(value.Firstname);
      writer.WritePropertyName("Sex");
      writer.WriteValue(value.Sex);
      writer.WritePropertyName("Year");
      writer.WriteValue(value.Year);
      writer.WritePropertyName("Club");
      writer.WriteValue(value.Club);
      writer.WritePropertyName("Nation");
      writer.WriteValue(value.Nation);
      writer.WritePropertyName("Class");
      writer.WriteValue(value.Class.ToString());
      writer.WritePropertyName("Group");
      writer.WriteValue(value.Class.Group.ToString());
      writer.WritePropertyName("Runtime");
      writer.WriteValue(value.Runtime?.ToString(@"mm\:ss\,ff"));
      writer.WritePropertyName("DisqualText");
      writer.WriteValue(value.DisqualText);
      writer.WritePropertyName("JustModified");
      writer.WriteValue(value.JustModified);
      writer.WriteEndObject();
    }

    public override RunResultWithPosition ReadJson(JsonReader reader, Type objectType, RunResultWithPosition existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }


  public class RunResultConverter : JsonConverter<RunResult>
  {
    bool _precisionIn100seconds;

    public RunResultConverter(bool precisionIn100seconds)
    {
      _precisionIn100seconds = precisionIn100seconds;
    }
    public override void WriteJson(JsonWriter writer, RunResult value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("Id");
      writer.WriteValue(value.Id);
      writer.WritePropertyName("StartNumber");
      writer.WriteValue(value.StartNumber);
      writer.WritePropertyName("Name");
      writer.WriteValue(value.Name);
      writer.WritePropertyName("Firstname");
      writer.WriteValue(value.Firstname);
      writer.WritePropertyName("Sex");
      writer.WriteValue(value.Sex);
      writer.WritePropertyName("Year");
      writer.WriteValue(value.Year);
      writer.WritePropertyName("Club");
      writer.WriteValue(value.Club);
      writer.WritePropertyName("Nation");
      writer.WriteValue(value.Nation);
      writer.WritePropertyName("Class");
      writer.WriteValue(value.Class.ToString());
      writer.WritePropertyName("Group");
      writer.WriteValue(value.Class.Group.ToString());
      writer.WritePropertyName("Runtime");
      if (_precisionIn100seconds)
        writer.WriteValue(value.Runtime?.ToString(@"mm\:ss\,ff"));
      else
        writer.WriteValue(value.Runtime?.ToString(@"mm\:ss"));
      writer.WritePropertyName("DisqualText");
      writer.WriteValue(value.DisqualText);
      writer.WriteEndObject();
    }

    public override RunResult ReadJson(JsonReader reader, Type objectType, RunResult existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }


  public class RaceResultConverter : JsonConverter<RaceResultItem>
  {
    public override void WriteJson(JsonWriter writer, RaceResultItem value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("Id");
      writer.WriteValue(value.Participant.Id);
      writer.WritePropertyName("Position");
      writer.WriteValue(value.Position);
      writer.WritePropertyName("StartNumber");
      writer.WriteValue(value.Participant.StartNumber);
      writer.WritePropertyName("Name");
      writer.WriteValue(value.Participant.Name);
      writer.WritePropertyName("Firstname");
      writer.WriteValue(value.Participant.Firstname);
      writer.WritePropertyName("Sex");
      writer.WriteValue(value.Participant.Sex);
      writer.WritePropertyName("Year");
      writer.WriteValue(value.Participant.Year);
      writer.WritePropertyName("Club");
      writer.WriteValue(value.Participant.Club);
      writer.WritePropertyName("Nation");
      writer.WriteValue(value.Participant.Nation);
      writer.WritePropertyName("Class");
      writer.WriteValue(value.Participant.Class.ToString());
      writer.WritePropertyName("Group");
      writer.WriteValue(value.Participant.Class.Group.ToString());
      writer.WritePropertyName("Totaltime");
      writer.WriteValue(value.TotalTime?.ToString(@"mm\:ss\,ff"));

      int i = 0;
      foreach (var r in value.RunTimes)
      {
        i++;
        writer.WritePropertyName(string.Format("Runtime{0}", r.Key));
        writer.WriteValue(r.Value?.ToString(@"mm\:ss\,ff"));
      }
      writer.WritePropertyName("JustModified");
      writer.WriteValue(value.JustModified);
      writer.WriteEndObject();
    }

    public override RaceResultItem ReadJson(JsonReader reader, Type objectType, RaceResultItem existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }



  public class StartListEntryConverter : JsonConverter<StartListEntry>
  {
    public override void WriteJson(JsonWriter writer, StartListEntry value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("Id");
      writer.WriteValue(value.Id);
      writer.WritePropertyName("StartNumber");
      writer.WriteValue(value.StartNumber);
      writer.WritePropertyName("Name");
      writer.WriteValue(value.Name);
      writer.WritePropertyName("Firstname");
      writer.WriteValue(value.Firstname);
      writer.WritePropertyName("Sex");
      writer.WriteValue(value.Sex);
      writer.WritePropertyName("Year");
      writer.WriteValue(value.Year);
      writer.WritePropertyName("Club");
      writer.WriteValue(value.Club);
      writer.WritePropertyName("Nation");
      writer.WriteValue(value.Nation);
      writer.WritePropertyName("Class");
      writer.WriteValue(value.Class.ToString());
      writer.WritePropertyName("Group");
      writer.WriteValue(value.Class.Group.ToString());
      writer.WriteEndObject();
    }

    public override StartListEntry ReadJson(JsonReader reader, Type objectType, StartListEntry existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }


  public static class JsonConversion
  {
    public static string ConvertStartList(IEnumerable startList)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "startlist" },
        {"data",  startList}
      };

      JsonSerializer serializer = new JsonSerializer();

      serializer.Converters.Add(new RaceParticipantConverter());
      serializer.Converters.Add(new StartListEntryConverter());

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }

    public static string ConvertOnStartList(IEnumerable startList)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "onstart" },
        {"data",  startList}
      };

      JsonSerializer serializer = new JsonSerializer();

      serializer.Converters.Add(new RaceParticipantConverter());
      serializer.Converters.Add(new StartListEntryConverter());

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }


    public static string ConvertOnTrack(IEnumerable resultList)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "ontrack" },
        {"data",  resultList}
      };

      JsonSerializer serializer = new JsonSerializer();

      serializer.Converters.Add(new RunResultConverter(false));

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }



    public static string ConvertRunResults(IEnumerable resultList)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "racerunresult" },
        {"data",  resultList}
      };

      JsonSerializer serializer = new JsonSerializer();

      serializer.Converters.Add(new RunResultWPConverter());

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }

    public static string ConvertRaceResults(IEnumerable resultList)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "raceresult" },
        {"data",  resultList}
      };

      JsonSerializer serializer = new JsonSerializer();

      serializer.Converters.Add(new RaceResultConverter());

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }


    public static string ConvertCurrrentRaceRun(IEnumerable data)
    {
      var wrappedData = new Dictionary<string, object>
      {
        {"type", "currentracerun" },
        {"data",  data}
      };

      JsonSerializer serializer = new JsonSerializer();

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }


    public static string ConvertWrappedData(IEnumerable wrappedData)
    {
      JsonSerializer serializer = new JsonSerializer();

      StringWriter sw = new StringWriter();
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, wrappedData);
      }

      return sw.ToString();
    }


  }
}
