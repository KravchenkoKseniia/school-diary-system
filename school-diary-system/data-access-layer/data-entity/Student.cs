namespace school_diary_system.data_access_layer.data_entity;
using System.Text.Json;

public class Student
    : IStudent
{
    private static int _lastId = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Class { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
    
    public Student(string name, string surname, string @class, int age, string phone)
    {
        Id = ++_lastId;
        Name = name;
        Surname = surname;
        Class = @class;
        Age = age;
        Phone = phone;
    }
    
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public void FromJson(string jsonData)
    {
        var student = JsonSerializer.Deserialize<Student>(jsonData);

        if (student == null) return;
        Name = student.Name;
        Surname = student.Surname;
        Class = student.Class;
        Age = student.Age;
        Phone = student.Phone;

    }
    
    public static void GetLastId(int id)
    {
        _lastId = id;
    }
}