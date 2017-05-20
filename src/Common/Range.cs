using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class Range<T> : Tuple<T,T> where T : IComparable<T>
    {
        private readonly FunctionDelegate<T, T> incrementor;

        public Range() 
        {
        }

        public Range(T start, T end) : base(start, end)
        {
        }

        public Range(T start, T end, FunctionDelegate<T, T> incrementor)
            : base(start, end)
        {
            this.incrementor = incrementor;
        }

        public T Start
        {
            get { return left; }
            set { left = value; }
        }

        public T End
        {
            get { return right; }
            set { right = value; }
        }

        public bool Includes(T value)
        {
            return Start.CompareTo(value) <= 0 && End.CompareTo(value) >= 0;
        }

        public bool Includes(Range<T> value)
        {
            return Start.CompareTo(value.Start) <= 0 && End.CompareTo(value.End) >= 0;
        }

        public bool Overlaps(Range<T> value)
        {
            return Start.CompareTo(value.End) <= 0 && End.CompareTo(value.Start) >= 0;
        }

        public void ForEach(ActionDelegate<T> action)
        {
            ForEach(incrementor, action);
        }

        public void ForEach(FunctionDelegate<T,T> incrementorAction, ActionDelegate<T> action)
        {
            if(incrementorAction == null)
            {
                throw new NullReferenceException("An incrementor action is required.");
            }
            for (T current = Start; Includes(current); current=incrementorAction(current))
            {
                action(current);
            }
        }

        public void BackwardForEach(FunctionDelegate<T, T> decrementorAction, ActionDelegate<T> action)
        {
            if (decrementorAction == null)
            {
                throw new NullReferenceException("An decrementor action is required.");
            }
            for (T current = End; Includes(current); current = decrementorAction(current))
            {
                action(current);
            }
        }
        
        public static Range<int> Hours = Range<int>.IntRange(1,24);

        public static Range<int> Create(int start, int end)
        {
            return new Range<int>(start, end, inc=>inc+1);
        }

        public bool AssertIncludes(T value)
        {
            if (!Includes(value))
                throw new ArgumentException($"value is outside range {value}");
            return true;
        }

        public T[] ToArray()
        {
            var list = new List<T>();
            ForEach(list.Add);
            return list.ToArray();
        }

        public static Range<DateTime> DateRange(DateTime start, DateTime end)
        {
            return new Range<DateTime>(start, end, x => x.AddDays(1));
        }

        public static Range<int> IntRange(int start, int end)
        {
            return new Range<int>(start, end, inc => inc+1);
        }
    }
}