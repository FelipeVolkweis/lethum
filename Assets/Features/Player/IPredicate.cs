using System;
public interface IPredicate
{
    bool Evaluate();
}

public abstract class FuncPredicate : IPredicate
{
    readonly Func<bool> func;
    protected FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }
    public bool Evaluate()
    {
        return func.Invoke();
    }
}