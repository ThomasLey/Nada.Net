﻿using System.Collections.Generic;

namespace Nada.NZazu.Contracts;

public class CheckDefinition
{
    public string Type { get; set; }
    public IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
}