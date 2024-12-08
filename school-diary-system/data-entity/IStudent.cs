namespace school_diary_system.data_entity;
public interface IStudent
{
    int Id { get; set; }
    string Name { get; set; }
    string Surname { get; set; }
    string Class { get; set; }
    int Age { get; set; }
    string Phone { get; set; }
    
    string ToJson();
    void FromJson(string jsonData);
}