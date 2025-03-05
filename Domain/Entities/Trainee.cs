namespace Domain.Entities;

public class Trainee : IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string DateOfBirth { get; private set; }
    public Guid? InternshipDirectionId { get; private set; }
    public Guid? CurrentProjectId { get; private set; }

    public Trainee()
    {
        Id = Guid.NewGuid();
    }

    public Trainee(string name, string surname, string email, string phoneNumber, string dateOfBirth,
        Guid internshipDirectionId, Guid currentProjectId)
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        InternshipDirectionId = internshipDirectionId;
        CurrentProjectId = currentProjectId;
    }

    public void Edit(string name = null, string surname = null, string email = null, string phoneNumber = null,
        string dateOfBirth = null, Guid? internshipDirectionId = null, Guid? currentProjectId = null)
    {
        if (name is not null) Name = name;
        if (surname is not null) Surname = surname;
        if (email is not null) Email = email;
        if (phoneNumber is not null) PhoneNumber = phoneNumber;
        if (dateOfBirth is not null) DateOfBirth = dateOfBirth;
        if (phoneNumber is not null) PhoneNumber = phoneNumber;
        if (internshipDirectionId is not null) InternshipDirectionId = internshipDirectionId;
        if (currentProjectId is not null) CurrentProjectId = currentProjectId;
    }
}