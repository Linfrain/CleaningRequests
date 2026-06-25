using System;
using System.Collections.Generic;

namespace CleaningRequests.Models;

public partial class Request
{
    public int Id { get; set; }

    public string Full_name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Cabinets { get; set; } = null!;

    public DateOnly Request_date { get; set; }

    public TimeOnly Request_time { get; set; }

    public string? Comment { get; set; }

    public int Status_id { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
