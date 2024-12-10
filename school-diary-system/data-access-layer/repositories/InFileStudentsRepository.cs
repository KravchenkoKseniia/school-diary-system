namespace school_diary_system.data_access_layer.repositories;
using school_diary_system.data_access_layer.data_entity;
using System.Text.Json;

public class InFileStudentsRepository : IStudentsRepository
{
    private string _filePath = "students.json";
    private List<Student> GetAllStudents()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
        
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Student>>(jsonData) ?? [];
    }
    
    private void InitializeLastId()
    {
        var students = GetAllStudents();
        var lastId = students.Max(s => s.Id);
        Student.GetLastId(lastId);
    }
    
    private void SaveAllStudents(List<Student> students)
    {
        var jsonData = JsonSerializer.Serialize(students);
        File.WriteAllText(_filePath, jsonData);
    }
    
    public InFileStudentsRepository()
    {
        InitializeLastId();
    }
    
    public void AddStudent(string name, string surname, string @class, int age, string phone)
    {
        var newStudent = new Student(name, surname, @class, age, phone);
        
        var students = GetAllStudents();
        students.Add(newStudent);
        SaveAllStudents(students);
    }

    public Student? GetStudentById(int id)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        
        if (student == null)
        {
            Console.WriteLine($"Student with id {id} not found");
        }
        
        return student ?? null;
    }

    public Student? GetStudentByNameSurname(string name, string surname)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Name == name && s.Surname == surname);

        if (student == null)
        {
            Console.WriteLine($"Student with {name} {surname} not found");
        }

        return student ?? null;

    }

    public void UpdateStudent(int id)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        
        if (student == null)
        {
            Console.WriteLine($"Student with id:{id} not found");
            return;
        }
        
        Console.WriteLine("What do you want to update? Enter the numbers separated by space:");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Surname");
        Console.WriteLine("3. Class");
        Console.WriteLine("4. Age");
        Console.WriteLine("5. Phone");
        
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
                    student.Name = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                case 2:
                    Console.WriteLine("Enter new surname:");
                    student.Surname = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                case 3:
                    Console.WriteLine("Enter new class:");
                    student.Class = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                case 4:
                    Console.WriteLine("Enter new age:");
                    student.Age = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    break;
                case 5:
                    Console.WriteLine("Enter new phone:");
                    student.Phone = Console.ReadLine() ?? throw new InvalidOperationException();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
        SaveAllStudents(students);
        Console.WriteLine($"Student with id:{id} updated successfully");
        
    }

    public void DeleteStudentById(int id)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        
        if (student == null)
        {
            Console.WriteLine("Student not found");
            return;
        }
        
        students.Remove(student);
        SaveAllStudents(students);
    }
}