﻿namespace Nada.Core.Collections;

/// <summary>
///     Extension methods for list to tree conversion -and vice vera.
/// </summary>
public static class TreeItemExtensions
{
    /// <summary>
    ///     Generates tree of items from item list
    /// </summary>
    /// <typeparam name="T">Type of item in collection</typeparam>
    /// <typeparam name="K">Type of parent_id</typeparam>
    /// <param name="collection">Collection of items</param>
    /// <param name="idSelector">Function extracting item's id</param>
    /// <param name="parentIdSelector">Function extracting item's parent_id</param>
    /// <param name="rootId">Root element id</param>
    /// <returns>Tree of items</returns>
    public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
        this IEnumerable<T> collection,
        Func<T, K> idSelector,
        Func<T, K> parentIdSelector,
        K rootId = default)
    {
        foreach (var c in collection.Where(c => EqualityComparer<K>.Default.Equals(parentIdSelector(c), rootId)))
            yield return new TreeItem<T>
            {
                Item = c,
                Children = collection.GenerateTree(idSelector, parentIdSelector, idSelector(c))
            };
    }
}