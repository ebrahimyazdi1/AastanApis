﻿namespace AastanApis.Models;

public class OutputModel
{
    public string? Content { get; set; }
    public string? RequestId { get; set; }
    public string? StatusCode { get; set; }
    public string? ReqLogId { get; set; }

    public class InternalOutputModel
    {
        public string? Content { get; set; }
        public string? RequestId { get; set; }
        public string? StatusCode { get; set; }
        public bool IsSuccess { get; set; }


    }
}

