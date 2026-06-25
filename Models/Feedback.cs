using System;
using System.Collections.Generic;

namespace CleaningRequests.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }

    public int Request_id { get; set; }

    public virtual Request Request { get; set; } = null!;
}
