using Panda;
public class mob_vampire : controller_mob
{
    protected bool _isAwake;
    public override void Load()
    {
        base.Load();
        _isAwake = false;
    }
    [Task]
    bool IsAwake
    {
        get { return _isAwake; }
    }
    [Task]
    void SetAwake(int value)
    {
        _isAwake = value == 1;
        Task.current.Succeed();
    }
}
