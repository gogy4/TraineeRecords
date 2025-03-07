namespace Domain.Entities;

public class CurrentProject : IResource, IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int CountTrainees { get; private set; }
    public void IncreaseTraineeCount()
    {
        CountTrainees++;
    }

    public void DecreaseTraineeCount()
    {
        if (CountTrainees > 0) CountTrainees--;
        else throw new ArgumentException("Cannot decrease the trainee count");
    }

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