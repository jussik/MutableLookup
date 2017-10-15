using System.Collections.Generic;
using MutableLookup;
using NUnit.Framework;

namespace MutableLookupTests
{
	[TestFixture]
    public class MutableLookupTests
	{
		private static IMutableLookup<int, string>[] LookupTypes => new IMutableLookup<int, string>[]
			{
				new ListLookup<int, string>(),
				new LinkedListLookup<int, string>(),
				new HashSetLookup<int, string>()
			};

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestNew(IMutableLookup<int, string> lookup)
	    {
		    Assert.That(lookup, Is.Empty);
	    }

		[TestCaseSource(nameof(LookupTypes))]
	    public void TestEmptyReturnsZeroLengthIEnumerable(IMutableLookup<int, string> lookup)
	    {
		    Assert.That(lookup[12], Is.Not.Null);
		    Assert.That(lookup[12], Is.Empty);
	    }

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestAddSingle(IMutableLookup<int, string> lookup)
		{
			lookup.Add(12, "foo");
			Assert.That(lookup[12], Is.EquivalentTo(new[] {"foo"}));
			Assert.That(lookup[23], Is.Empty);
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestAddMulti(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    lookup.Add(12, "bar");
		    lookup.Add(23, "baz");
		    Assert.That(lookup[12], Is.EquivalentTo(new[] { "foo", "bar" }));
		    Assert.That(lookup[23], Is.EquivalentTo(new[] { "baz" }));
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestRemoveKey(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    lookup.Add(12, "bar");
		    Assert.That(lookup[12], Is.EquivalentTo(new[] { "foo", "bar" }));
		    Assert.That(lookup.Remove(12), Is.EqualTo(true));
		    Assert.That(lookup[12], Is.Empty);
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestRemoveValue(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    lookup.Add(12, "bar");
		    Assert.That(lookup[12], Is.EquivalentTo(new[] { "foo", "bar" }));
		    Assert.That(lookup.Remove(12, "foo"), Is.EqualTo(true));
		    Assert.That(lookup[12], Is.EquivalentTo(new[] { "bar" }));
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestRemoveKeyNotExists(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    Assert.That(lookup.Remove(23), Is.EqualTo(false));
		    Assert.That(lookup[12], Is.Not.Empty);
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestRemoveValueNotExists(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    Assert.That(lookup.Remove(12, "bar"), Is.EqualTo(false));
		    Assert.That(lookup[12], Is.Not.Empty);
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestRemoveValueKeyNotExists(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    Assert.That(lookup.Remove(23, "bar"), Is.EqualTo(false));
		    Assert.That(lookup[12], Is.Not.Empty);
		}

	    [TestCaseSource(nameof(LookupTypes))]
	    public void TestClear(IMutableLookup<int, string> lookup)
	    {
		    lookup.Add(12, "foo");
		    lookup.Add(12, "bar");
		    lookup.Add(23, "baz");
		    Assert.That(lookup, Is.Not.Empty);
		    Assert.That(lookup[12], Is.Not.Empty);
		    Assert.That(lookup[23], Is.Not.Empty);
			lookup.Clear();
		    Assert.That(lookup, Is.Empty);
		    Assert.That(lookup[12], Is.Empty);
		    Assert.That(lookup[23], Is.Empty);
		}

		[TestCaseSource(nameof(LookupTypes))]
		public void TestClearEmpty(IMutableLookup<int, string> lookup)
		{
			Assert.That(lookup, Is.Empty);
			lookup.Clear();
			Assert.That(lookup, Is.Empty);
		}

		[TestCaseSource(nameof(LookupTypes))]
		public void TestTryGetValues(IMutableLookup<int, string> lookup)
		{
			lookup.Add(12, "foo");
			lookup.Add(12, "bar");
			lookup.Add(23, "baz");
			Assert.That(lookup.TryGetValues(12, out IEnumerable<string> result12), Is.True);
			Assert.That(result12, Is.EquivalentTo(new[] {"foo", "bar"}));
			Assert.That(lookup.TryGetValues(23, out IEnumerable<string> result23), Is.True);
			Assert.That(result23, Is.EquivalentTo(new[] {"baz"}));
		}

		[TestCaseSource(nameof(LookupTypes))]
		public void TestTryGetValuesFail(IMutableLookup<int, string> lookup)
		{
			lookup.Add(12, "foo");
			lookup.Add(12, "bar");
			Assert.That(lookup.TryGetValues(23, out IEnumerable<string> result), Is.False);
			Assert.That(result, Is.Null);
		}

		[TestCaseSource(nameof(LookupTypes))]
		public void TestTryGetValuesRemoved(IMutableLookup<int, string> lookup)
		{
			lookup.Add(12, "foo");
			lookup.Add(12, "bar");
			Assert.That(lookup.TryGetValues(12, out IEnumerable<string> resultSome), Is.True);
			Assert.That(resultSome, Is.Not.Empty);
			lookup.Remove(12);
			Assert.That(lookup.TryGetValues(12, out IEnumerable<string> resultNone), Is.False);
			Assert.That(resultNone, Is.Null);
		}
	}
}
