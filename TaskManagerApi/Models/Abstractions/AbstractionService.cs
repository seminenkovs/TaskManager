namespace TaskManagerApi.Models;

public abstract class AbstractionService
{
    public bool DoAction(Action action)
    {
        try
        {
            action.Invoke();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}