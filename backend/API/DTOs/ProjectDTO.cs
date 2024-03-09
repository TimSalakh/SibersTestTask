using System.ComponentModel.DataAnnotations;

namespace API.VM;

public class ProjectDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Customer { get; set; }

    [Required]
    public string Executor { get; set; }

    [Required]
    public Guid LeaderId { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    public int Priority { get; set; }
}
