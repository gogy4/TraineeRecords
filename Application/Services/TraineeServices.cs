using Domain.Entities;

namespace Application.Services;

public class TraineeServices(ITraineeRepository repository)
{
    public async Task<Trainee> Create(string name, string surname, string gender, string email, string phoneNumber,
        DateTime dateOfBirth, Guid internshipDirectionId, Guid currentProjectId)
    {
        var trainee = new Trainee(name, surname, gender, email, phoneNumber, dateOfBirth, internshipDirectionId,
            currentProjectId);
        await repository.AddAsync(trainee);
        return trainee;
    }
}