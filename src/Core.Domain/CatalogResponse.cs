﻿namespace Core.Domain;

public class CatalogResponse
{
    public int OrderId { get; set; }
    public int CatalogId { get; set; }
    public bool IsSuccess { get; set; }
}
