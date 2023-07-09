namespace Shared.Requests.Score;

public record ScoreResponse
{
    public string userId { get; set; }
    public int value { get; set; }
    public List<ScoreHistoryResponse> scoreHistories { get; set; }
    public int id { get; set; }
}

public record OccurrenceResponse
{
    public int value { get; set; }
    public string description { get; set; }
    public int id { get; set; }
}

public record ScoreHistoryResponse
{
    
    public int scoreId { get; set; }
    public int occurrenceId { get; set; }
    public OccurrenceResponse occurrence { get; set; }
    public int id { get; set; }
}