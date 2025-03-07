namespace Domain.Entities;

public class InternshipDirection : IResource, IEntity
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