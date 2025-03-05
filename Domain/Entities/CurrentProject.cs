namespace Domain.Entities;

public class CurrentProject : Interface
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int CountTrainees { get; private set; }

    public CurrentProject(string projectName, int countTrainees=0)
    {
        Id = Guid.NewGuid();
        Name = projectName;
        CountTrainees = countTrainees;
    }

    public CurrentProject()
    {
        Id = Guid.NewGuid();
        CountTrainees = 0;
    }
}