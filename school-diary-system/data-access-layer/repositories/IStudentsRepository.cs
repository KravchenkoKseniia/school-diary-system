using school_diary_system.data_access_layer.data_entity;

namespace school_diary_system.data_access_layer.repositories;

public interface IStudentsRepository
{
    void AddStudent(string name, string surname, string @class, int age, string phone);
    Student? GetStudentById(int id);
    Student? GetStudentByNameSurname(string name, string surname);
    void UpdateStudent(int id);
    void DeleteStudentById(int id);
}