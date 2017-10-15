using System.Collections.Generic;
using System.Linq;

namespace MutableLookup
{
	public interface IMutableLookup<TKey, TElement> : ILookup<TKey, TElement>
	{
		void Add(TKey key, TElement value);
		bool Remove(TKey key);
		bool Remove(TKey key, TElement value);
		void Clear();
		bool TryGetValues(TKey key, out IEnumerable<TElement> values);
	}
}