﻿namespace Books.MinimalApi.Helpers;
public class ValidationResult
{
    public bool IsValid { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}