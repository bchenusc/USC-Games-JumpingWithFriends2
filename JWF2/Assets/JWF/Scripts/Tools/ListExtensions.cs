using System.Collections;
using System.Collections.Generic;

public static class ListExtensions {

	public static void RemoveAtSwap<T>(this List<T> list, int index) {
		if (list.Count == 0 || index < 0 || index >= list.Count) {
			return;
		}

		if (index == list.Count - 1) {
			list.RemoveAt(index);
			return;
		}

		list[index] = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
	}

}
