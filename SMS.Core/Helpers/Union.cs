using System;

namespace SMS.Core.Helpers
{

    // Discriminated Union Type implementation
    // https://medium.com/kabbage-engineering/discriminated-unions-in-c-an-unexceptional-love-story-82abb7f260c2
    public abstract class Union<T1, T2> : IEquatable<Union<T1, T2>>
    {
        public abstract TResult Match<TResult>(Func<T1, TResult> f1, Func<T2, TResult> f2);
        public abstract void Match(Action<T1> a1, Action<T2> a2);

        public bool Equals(Union<T1, T2> other)
        {
            throw new NotImplementedException();
        }

        private Union() { }

        public static implicit operator Union<T1, T2>(T1 item)
        {
            return new Case1(item);
        }

        public static implicit operator Union<T1, T2>(T2 item)
        {
            return new Case2(item);
        }

        public sealed class Case1 : Union<T1, T2>
        {
            public T1 Item { get; }

            public Case1(T1 item)
            {
                Item = item;
            }

            public override TResult Match<TResult>(Func<T1, TResult> f1, Func<T2, TResult> f2)
            {
                return f1(Item);
            }

            public override void Match(Action<T1> a1, Action<T2> a2)
            {
                a1(Item);
            }
        }

        public sealed class Case2 : Union<T1, T2>
        {
            public T2 Item { get; }

            public Case2(T2 item)
            {
                Item = item;
            }

            public override TResult Match<TResult>(Func<T1, TResult> f1, Func<T2, TResult> f2)
            {
                return f2(Item);
            }

            public override void Match(Action<T1> a1, Action<T2> a2)
            {
                a2(Item);
            }
        }
    }
    
}