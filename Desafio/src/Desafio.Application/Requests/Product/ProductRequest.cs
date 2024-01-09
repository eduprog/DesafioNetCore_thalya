﻿using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class ProductRequest
{
    private string _acronym = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; } = Guid.Empty;
    [Required(ErrorMessage = "The field {0} is required")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "The field {0} is required")]
    public string ShortDescription { get; set; } = string.Empty;
    [Required(ErrorMessage = "The field {0} is required")]
    public string Acronym
    {
        get => _acronym;
        set => _acronym = value;
    }
    [Required(ErrorMessage = "The field {0} is required")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public decimal StoredQuantity { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public bool Enable { get; set; } = true;
    [Required(ErrorMessage = "The field {0} is required")]
    public bool Sellable { get; set; } = false;
    [Required(ErrorMessage = "The field {0} is required")]
    public string BarCode { get; set; } = string.Empty;


}