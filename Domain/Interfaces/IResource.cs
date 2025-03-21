﻿namespace Domain.Entities;

public interface IResource
{
    public Guid Id { get;  }
    public string Name { get;  }
    public int CountTrainees { get; }

    public void IncreaseTraineeCount();
    public void DecreaseTraineeCount();
}