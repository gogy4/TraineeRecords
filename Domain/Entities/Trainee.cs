namespace Domain.Entities;

public class Trainee
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string DateOfBirth { get; private set; }
    public Guid InternshipDirection { get; private set; }
    public Guid CurrentProject { get; private set; }
    
    public Trainee()
    {
        Id = Guid.NewGuid();
    }

    public Trainee(string name, string surname, string email, string phoneNumber, string dateOfBirth, Guid internshipDirection, Guid currentProject)
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        InternshipDirection = internshipDirection;
        CurrentProject = currentProject;
    }
}