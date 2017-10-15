[![NuGet](https://img.shields.io/nuget/v/MutableLookup.svg)](https://www.nuget.org/packages/MutableLookup)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/jussik/MutableLookup?svg=true)](https://ci.appveyor.com/project/jussik/mutablelookup)

# MutableLookup

A .NET Standard 2.0 library for mutable `ILookup` collections.

Defines an interface `IMutableLookup` for defining mutable `ILookup` containers:

```c#
public interface IMutableLookup<TKey, TElement> : ILookup<TKey, TElement>
{
    void Add(TKey key, TElement value);
    bool Remove(TKey key);
    bool Remove(TKey key, TElement value);
    void Clear();
    bool TryGetValues(TKey key, out IEnumerable<TElement> values);
}
```

Comes with a few predefined types that utilise .NET's own generic collection types:

* `ListLookup<TKey, TElement>` equivalent to `Dictionary<TKey, List<TElement>>`
* `LinkedListLookup<TKey, TElement>` equivalent to `Dictionary<TKey, LinkedList<TElement>>`
* `HashSetLookup<TKey, TElement>` equivalent to `Dictionary<TKey, HashSet<TElement>>`

Each type has its own advantages and disadvantages based on use case requirements.

## Installation

Install `MutableLookup` through NuGet:

`NuGet install MutableLookup`

NuGet package page: https://www.nuget.org/packages/MutableLookup

## Implementing your own IMutableLookup

The library comes with the `MutableLookup` abstract class which allows trivial definition of a new IMutableLookup. Given a type `MyCollection<TElement>` that implements `ICollection<TElement>`, you can define a new IMutableLookup type like this:

```c#
public sealed class MyCollectionLookup<TKey, TElement>
    : MutableLookup<TKey, TElement, MyCollection<TElement>> { }
```

If your collection does not implement a constant time `.Count` property, you should override the `bool IsContainerEmpty(TContainer container)` method to efficiently see if the container is empty.
