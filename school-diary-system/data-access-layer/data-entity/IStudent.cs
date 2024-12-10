namespace school_diary_system.data_access_layer.data_entity;
public interface IStudent
{
    string Name { get; set; }
    string Surname { get; set; }
    string Class { get; set; }
    int Age { get; set; }
    string Phone { get; set; }
    
    string ToJson();
    void FromJson(string jsonData);
}