namespace Domain.Entities;

public class InternshipDirection : Interface
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public InternshipDirection(string direction)
    {
        Id = Guid.NewGuid();
        Name = direction;
    }

    public InternshipDirection()
    {
        Id = Guid.NewGuid();
    }
}