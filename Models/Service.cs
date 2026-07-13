namespace CleaningRequests.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = [];
}
