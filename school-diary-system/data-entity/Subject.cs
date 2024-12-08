namespace school_diary_system.data_entity;
using System.Text.Json;
public class Subject(string name, string teacher, string @class) : ISubject
{
    public string Name { get; set; } = name;
    public string Teacher { get; set; } = teacher;
    public string Class { get; set; } = @class;

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public void FromJson(string jsonData)
    {
        var subject = JsonSerializer.Deserialize<Subject>(jsonData);

        if (subject == null) return;
        Name = subject.Name;
        Teacher = subject.Teacher;
        Class = subject.Class;
    }
}
