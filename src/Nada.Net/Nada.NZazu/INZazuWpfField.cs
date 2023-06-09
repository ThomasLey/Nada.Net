﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Nada.NZazu.Contracts;
using Nada.NZazu.Contracts.Checks;

namespace Nada.NZazu;

public interface INZazuWpfField
    : IDisposable
{
    string Key { get; }
    FieldDefinition Definition { get; }

    Control LabelControl { get; }
    Control ValueControl { get; }
    bool IsEditable { get; }

    IEnumerable<KeyValuePair<string, string>> GetState();
    void SetValue(string value);
    string GetValue();

    ValueCheckResult Validate();
}

public interface INZazuWpfField<T> : INZazuWpfField
{
    T Value { get; set; }
}