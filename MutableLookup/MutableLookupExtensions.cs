using System;
using System.Collections.Generic;

namespace MutableLookup
{
	public static class MutableLookupExtensions
	{
		private static TLookup FillLookup<TLookup, TKey, TElement>(TLookup lookup, IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
			where TLookup : IMutableLookup<TKey, TElement>
		{
			foreach (TElement element in source)
			{
				lookup.Add(keySelector(element), element);
			}
			return lookup;
		}

		private static TLookup FillLookup<TLookup, TSource, TKey, TElement>(TLookup lookup, IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
			where TLookup : IMutableLookup<TKey, TElement>
		{
			foreach (TSource element in source)
			{
				lookup.Add(keySelector(element), elementSelector(element));
			}
			return lookup;
		}

		public static ListLookup<TKey, TElement> ToListLookup<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
			=> FillLookup(new ListLookup<TKey, TElement>(), source, keySelector);

		public static ListLookup<TKey, TElement> ToListLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
			=> FillLookup(new ListLookup<TKey, TElement>(), source, keySelector, elementSelector);

		public static HashSetLookup<TKey, TElement> ToHashSetLookup<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
			=> FillLookup(new HashSetLookup<TKey, TElement>(), source, keySelector);

		public static HashSetLookup<TKey, TElement> ToHashSetLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
			=> FillLookup(new HashSetLookup<TKey, TElement>(), source, keySelector, elementSelector);

		public static LinkedListLookup<TKey, TElement> ToLinkedListLookup<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
			=> FillLookup(new LinkedListLookup<TKey, TElement>(), source, keySelector);

		public static LinkedListLookup<TKey, TElement> ToLinkedListLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
			=> FillLookup(new LinkedListLookup<TKey, TElement>(), source, keySelector, elementSelector);
	}
}