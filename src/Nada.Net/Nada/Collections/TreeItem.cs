﻿namespace Nada.Core.Collections;

public class TreeItem<T>
{
    public T? Item { get; set; }
    public IEnumerable<TreeItem<T>>? Children { get; set; }
}