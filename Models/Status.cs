using System;
using System.Collections.Generic;

namespace CleaningRequests.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
