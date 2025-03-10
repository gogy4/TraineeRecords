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
        else throw new ArgumentException("Нельзя сделать количество стажеров меньше нуля.");
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
    
    public void ChangeName(string name)
    {
        if (name.Length is < 2 or > 50)
            throw new ArgumentException("Имя проекта должн быть в диапозоне от 2 до 50 символов");
        Name = name;
    }
}