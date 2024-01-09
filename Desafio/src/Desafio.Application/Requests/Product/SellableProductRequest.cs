﻿using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class SellableProductRequest
{
    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public bool Sellable { get; set; } = false;
    
}