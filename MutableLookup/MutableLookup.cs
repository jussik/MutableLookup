using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MutableLookup
{
	public abstract class MutableLookup<TKey, TElement, TContainer> : IMutableLookup<TKey, TElement>
		where TContainer : ICollection<TElement>, new()
	{
		private readonly Dictionary<TKey, TContainer> lookup;

		protected MutableLookup() => lookup = new Dictionary<TKey, TContainer>();

		public void Add(TKey key, TElement value)
		{
			if (!lookup.TryGetValue(key, out TContainer container))
			{
				container = new TContainer();
				lookup.Add(key, container);
			}

			container.Add(value);
		}

		public bool Remove(TKey key) => lookup.Remove(key);

		public bool Remove(TKey key, TElement value)
		{
			bool removed = false;
			if (lookup.TryGetValue(key, out TContainer container))
			{
				removed = container.Remove(value);
				if (IsContainerEmpty(container))
					lookup.Remove(key);
			}
			return removed;
		}

		public void Clear() => lookup.Clear();

		public bool TryGetValues(TKey key, out IEnumerable<TElement> values)
		{
			bool success = lookup.TryGetValue(key, out TContainer container);
			values = container;
			return success;
		}

		protected virtual bool IsContainerEmpty(TContainer container) => container.Count == 0;

		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator() => lookup
			.SelectMany(p => p.Value.Select(v => (k: p.Key, v)))
			.GroupBy(p => p.k, p => p.v)
			.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool Contains(TKey key) => lookup.ContainsKey(key);
		public int Count => lookup.Count;
		public IEnumerable<TElement> this[TKey key] => lookup.TryGetValue(key, out TContainer values) ? values : Enumerable.Empty<TElement>();
	}

	public sealed class ListLookup<TKey, TElement> : MutableLookup<TKey, TElement, List<TElement>> { }
	public sealed class HashSetLookup<TKey, TElement> : MutableLookup<TKey, TElement, HashSet<TElement>> { }
	public sealed class LinkedListLookup<TKey, TElement> : MutableLookup<TKey, TElement, LinkedList<TElement>>
	{
		protected override bool IsContainerEmpty(LinkedList<TElement> container) => container.First == null;
	}
}