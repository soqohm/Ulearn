using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
	public class StackOperationsLogger
	{
		private StringBuilder stackLog = new StringBuilder();

		public void SubscribeOn<T>(ObservableStack<T> stack)
		{
			stack.StackEvent += (sender, data) => stackLog.Append(data);
		}

		public string GetLog() => stackLog.ToString();
	}

	public class ObservableStack<T>
	{
		private List<T> stackData = new List<T>();

		public event EventHandler<StackEventData<T>> StackEvent;

		public void Push(T obj)
		{
			stackData.Add(obj);

			if (StackEvent != null)
				StackEvent.Invoke(this,
					new StackEventData<T> { IsPushed = true, Value = obj });
		}

		public T Pop()
		{
			var result = stackData[stackData.Count - 1];

			if (StackEvent != null)
				StackEvent.Invoke(this,
					new StackEventData<T> { IsPushed = false, Value = result });

			return result;
		}
	}
}