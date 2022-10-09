﻿global using System.ComponentModel.DataAnnotations;

namespace Employee.Core.Entities;
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } =DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
