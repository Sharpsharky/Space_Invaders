using System;

public interface IScorable
{
    public static event Action OnGetScore = delegate { };
    
}
