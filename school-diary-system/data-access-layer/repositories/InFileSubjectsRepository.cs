using school_diary_system.data_access_layer.data_entity;

namespace school_diary_system.data_access_layer.repositories;
using System.Text.Json;

public class InFileSubjectsRepository : ISubjectsRepository
{
    private string _filePath = "subjects.json";
    
    private List<Subject> GetAllSubjects()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
        
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Subject>>(jsonData) ?? [];
    }

    private void SaveAllSubjects(List<Subject> subjects)
    {
        var jsonData = JsonSerializer.Serialize(subjects);
        File.WriteAllText(_filePath, jsonData);
    }

    public void AddSubject(string name, string teacher, string @class)
    {
        var newSubject = new Subject(name, teacher, @class);

        var subjects = GetAllSubjects();
        
        subjects.Add(newSubject);
        SaveAllSubjects(subjects);
    }

    public Subject? GetSubjectByName(string name)
    {
        var subjects = GetAllSubjects();
        var subject = subjects.FirstOrDefault(sub => sub.Name == name);

        if (subject == null)
        {
            Console.WriteLine($"Subject with name {name} is not found");
        }

        return subject ?? null;
    }

    public List<Subject?>  GetSubjectsByTeacher(string teacher)
    {
        var subjects = GetAllSubjects();
        var teachersSubjects = new List<Subject?>();

        foreach (var sub in subjects)
        {
            if (sub.Teacher == teacher)
            {
                teachersSubjects.Add(sub);
            }
        }

        return teachersSubjects;
    }

    public void UpdateSubject(string name)
    {
        var subjects = GetAllSubjects();
        var sub = subjects.FirstOrDefault(sub => sub.Name == name);

        if (sub == null)
        {
            Console.WriteLine($"Subject with name {name} is not found");
            return;
        }
        
        Console.WriteLine("What do you want to update? Enter the numbers separated by space:");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Teacher");
        Console.WriteLine("3. Class");

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        
        var propertiesToUpdate = input.Split(' ').Select(int.Parse).ToList();

        foreach (var updateInput in propertiesToUpdate)
        {
            switch (updateInput)
            {
                case 1:
                    Console.WriteLine("Enter new name:");
                    sub.Name = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                case 2:
                    Console.WriteLine("Enter new teacher`s name:");
                    sub.Teacher = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                case 3:
                    Console.WriteLine("Enter new class:");
                    sub.Class = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
        SaveAllSubjects(subjects);
        Console.WriteLine($"Subject with name {name} updated successfully");
    }

    public void DeleteSubject(string name)
    {
        var subjects = GetAllSubjects();
        var sub = subjects.FirstOrDefault(s => s.Name == name);

        if (sub == null)
        {
            Console.WriteLine($"Subject with name {name} is not found");
            return;
        }

        subjects.Remove(sub);
        SaveAllSubjects(subjects);

    }
    
}