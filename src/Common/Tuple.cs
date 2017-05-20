using System;

namespace Common
{
    [Serializable]
    public class Tuple<T,U> : IEquatable<Tuple<T,U>>
    {
        protected T left;
        protected U right;

        public Tuple()
        {
        }

        public Tuple(T left, U right)
        {
            this.left = left;
            this.right = right;
        }

        public T Left
        {
            get { return left; }
            set { left = value; }
        }

        public U Right
        {
            get { return right; }
            set { right = value; }
        }

        public bool Equals(Tuple<T,U> tuple)
        {
            return Equals(left, tuple.left) && Equals(right, tuple.right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tuple<T,U>)) return false;
            return Equals((Tuple<T,U>) obj);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() + 29*right.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Left={0}, Right={1}", left, right);   
        }

    }
}
