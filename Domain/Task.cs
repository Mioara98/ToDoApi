using MongoDB.Bson;

namespace Domain
{
  public class Task : BaseEntity
  {
    public string Description { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }

    public DateTime DueData { get; set; }

    public bool IsCompleted { get; set; }

    public string UserId { get; set; } = string.Empty;
  }
}
