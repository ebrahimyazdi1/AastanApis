﻿namespace AastanApis.Models;

public class PgsbTokenResDto
{
    public string? AccessToken { get; init; }
    public string? TokenType { get; init; }
    public string? RefreshToken { get; init; }
    public int? ExpiresIn { get; init; }
    public string? Scope { get; init; }
}