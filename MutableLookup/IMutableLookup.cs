using System.Collections.Generic;
using System.Linq;

namespace MutableLookup
{
	public interface IMutableLookup<TKey, TValue> : ILookup<TKey, TValue>
	{
		void Add(TKey key, TValue value);
		void Remove(TKey key);
		void Remove(TKey key, TValue value);
		bool TryGetValues(TKey key, out IEnumerable<TValue> values);
	}
}