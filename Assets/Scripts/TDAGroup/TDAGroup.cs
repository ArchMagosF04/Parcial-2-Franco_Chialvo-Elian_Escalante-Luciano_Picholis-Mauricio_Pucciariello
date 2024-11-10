using System;
using System.Collections;
using System.Collections.Generic;

public abstract class TDAGroup<T>
{
    public abstract bool Add(T item);

    public abstract bool Remove(T item);

    public abstract bool Contains(T item);

    public abstract string Show();

    public abstract int Cardinality();

    public abstract bool IsEmpty();

    public abstract T[] GetGroup();

    public abstract TDAGroup<T> Union(TDAGroup<T> otherSet);

    public abstract TDAGroup<T> Intersection(TDAGroup<T> otherSet);

    public abstract TDAGroup<T> Difference(TDAGroup<T> otherSet);

}
