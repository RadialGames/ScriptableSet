using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Originally inspired by Unite Austin 2017 talk by Ryan Hipple
// https://www.youtube.com/watch?v=raQ3iHhE_Kk
// https://github.com/roboryantron/Unite2017

// Modifications, revisions, or complete rewrites made since initial inclusion

namespace Radial.ScriptableSet
{
	public abstract class ScriptableSet<T> : ScriptableObject
	{
		public enum SelectionType
		{
			Sequential,
			Random,
			RandomNoRepeats,
		}
	
		public SelectionType selectionType = SelectionType.Sequential;

		public List<T> Items = new List<T>();
		protected int index = -1;

		private List<int> indices;

		public void Add(T thing)
		{
			if (!Items.Contains(thing))
			{
				Items.Add(thing);
			}
		}

		public void Remove(T thing)
		{
			if (Items.Contains(thing))
			{
				Items.Remove(thing);
			}
		}

		public void SetIndex(int to)
		{
			index = to;
		}

		public T GetNext()
		{
			if (selectionType == SelectionType.RandomNoRepeats)
			{
				if (indices == null)
				{
					indices = new List<int>(Items.Count);
				}

				while (indices.Count < Items.Count)
				{
					indices.Insert(Random.Range(0, indices.Count), indices.Count);
				}

				while (indices.Count > Items.Count)
				{
					indices.Remove(indices.Count - 1);
				}

				index++;

				// If we've looped back to the beginning,
				// reshuffle all the indices by swapping each
				// element with another random one
				if (index >= Items.Count)
				{
					index %= Items.Count;

					// need to get the return value before randomizing the indices
					T returnValue = Items[indices[index]];

					for (int i = 0; i < indices.Count; i++)
					{
						int otherIndex = Random.Range(0, indices.Count);
						int otherValue = indices[otherIndex];
						indices[otherIndex] = indices[i];
						indices[i] = otherValue;
					}

					return returnValue;
				}
				else
					return Items[indices[index]];
			}
			else if (selectionType == SelectionType.Random)
			{
				index = Random.Range(0, Items.Count);
			}
			else
			{
				index++;
			}

			if (index >= Items.Count)
			{
				index = 0;
			}

			return Items[index];
		}

		private void OnDisable()
		{
			index = -1; // Reset between playmodes.
		}
	}
}