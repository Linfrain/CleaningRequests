using CleaningRequests.Components.Validation;
using System.ComponentModel.DataAnnotations;

namespace CleaningRequests.Models;
public partial class Request
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите ФИО.")]
    [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z\s\-]+$", ErrorMessage = "ФИО может содержать только буквы, пробелы и дефис.")]
    public string Full_name { get; set; } = null!;

    [Required(ErrorMessage = "Укажите номер телефона.")]
    [Phone(ErrorMessage = "Неверно указан номер телефона.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Укажите адрес.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Укажите кабинеты для уборки.")]
    public string Cabinets { get; set; } = null!;

    [Required(ErrorMessage = "Выберите дату уборки.")]
    private DateOnly request_date = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly Request_date
    {
        get => request_date;
        set
        {
            request_date = value;
            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = TimeOnly.FromDateTime(DateTime.Now);
            if (value == today && Request_time < now.AddHours(1))
                Request_time = now.AddHours(1);

        }
    }

    [Required(ErrorMessage = "Выберите время уборки.")]
    [CorrectTime("Request_date")]
    public TimeOnly Request_time { get; set;  } = TimeOnly.FromDateTime(DateTime.Now.AddHours(1));

    public string? Comment { get; set; }

    public int Status_id { get; set; } = 1;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Status Status { get; set; } = null!;

    [MinLength(1, ErrorMessage = "Выберите хотя бы 1 услугу.")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

}
