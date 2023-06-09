﻿using System;

namespace Nada.NZazu.Contracts.Checks;

public interface IValueCheck
{
    ValueCheckResult Validate(string value, object parsedValue, IFormatProvider formatProvider = null!);
}