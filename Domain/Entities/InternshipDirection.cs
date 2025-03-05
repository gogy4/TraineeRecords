namespace Domain.Entities;

public class InternshipDirection : IResource, IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int CountTrainees { get; private set; }

    public InternshipDirection(string direction, int countTrainees=0)
    {
        Id = Guid.NewGuid();
        CountTrainees = countTrainees;
        Name = direction;
    }

    public InternshipDirection()
    {
        Id = Guid.NewGuid();
        CountTrainees = 0;
    }
}