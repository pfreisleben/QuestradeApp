using ScoreApi.Domain.SeedWork;

namespace ScoreApi.Domain.Entities;

public class Occurrence : Entity
{
    public Occurrence(int id, int value, string description)
    {
        Id = id;
        Value = value;
        Description = description;
    }

    public Occurrence(int value, string description)
    {
        Value = value;
        Description = description;
    }

    protected Occurrence()
    {
    }

    public int Value { get; private set; }
    public string Description { get; private set; } = null!;
}