using school_diary_system.data_access_layer.data_entity;

namespace school_diary_system.data_access_layer.repositories;

public interface ISubjectsRepository
{
    void AddSubject(string name, string teacher, string @class);
    Subject? GetSubjectByName(string name);
    List<Subject?> GetSubjectsByTeacher(string teacher);
    void UpdateSubject(string name);
    void DeleteSubject(string name);
}