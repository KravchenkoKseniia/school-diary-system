namespace school_diary_system.data_entity;
using System.Text.Json;

public class Student(int id, string name, string surname, string @class, int age, string phone)
    : IStudent
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Surname { get; set; } = surname;
    public string Class { get; set; } = @class;
    public int Age { get; set; } = age;
    public string Phone { get; set; } = phone;

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public void FromJson(string jsonData)
    {
        var student = JsonSerializer.Deserialize<Student>(jsonData);

        if (student == null) return;
        Id = student.Id;
        Name = student.Name;
        Surname = student.Surname;
        Class = student.Class;
        Age = student.Age;
        Phone = student.Phone;

    }
}