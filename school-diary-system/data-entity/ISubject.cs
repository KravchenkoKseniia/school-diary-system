namespace school_diary_system.data_entity;
using System.Text.Json;
public interface ISubject
{
   string Name { get; set; }
   string Teacher { get; set; }
   string Class { get; set; }
   
   string ToJson();
   void FromJson(string jsonData);
}