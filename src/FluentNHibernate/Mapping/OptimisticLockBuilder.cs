using System;

namespace FluentNHibernate.Mapping;

public class OptimisticLockBuilder
{
    private readonly Action<string> setter;

    protected OptimisticLockBuilder(Action<string> setter)
    {
        this.setter = setter;
    }

    /// <summary>
    /// No optimistic locking
    /// </summary>
    public void None()
    {
        setter("none");
    }

    /// <summary>
    /// Version locking
    /// </summary>
    public void Version()
    {
        setter("version");
    }

    /// <summary>
    /// Dirty locking
    /// </summary>
    public void Dirty()
    {
        setter("dirty");
    }

    /// <summary>
    /// Lock on everything
    /// </summary>
    public void All()
    {
        setter("all");
    }
}

public class OptimisticLockBuilder<TParent> : OptimisticLockBuilder
{
    private readonly TParent parent;

    public OptimisticLockBuilder(TParent parent, Action<string> setter)
        : base(setter)
    {
        this.parent = parent;
    }

    /// <summary>
    /// Use no locking strategy
    /// </summary>
    public new TParent None()
    {
        base.None();
        return parent;
    }

    /// <summary>
    /// Use version locking
    /// </summary>
    public new TParent Version()
    {
        base.Version();
        return parent;
    }

    /// <summary>
    /// Use dirty locking
    /// </summary>
    public new TParent Dirty()
    {
        base.Dirty();
        return parent;
    }

    /// <summary>
    /// Use all locking
    /// </summary>
    public new TParent All()
    {
        base.All();
        return parent;
    }
}
