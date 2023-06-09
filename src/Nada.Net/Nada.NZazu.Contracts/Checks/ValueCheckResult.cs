﻿using System;

namespace Nada.NZazu.Contracts.Checks;

public class ValueCheckResult
{
    public static readonly ValueCheckResult Success = new(true);

    public ValueCheckResult(bool isValid, Exception exception = null)
    {
        IsValid = isValid;
        Exception = exception;
    }

    public ValueCheckResult(Exception error = null) : this(error == null, error)
    {
    }

    public bool IsValid { get; protected set; }
    public Exception Exception { get; protected set; }
}