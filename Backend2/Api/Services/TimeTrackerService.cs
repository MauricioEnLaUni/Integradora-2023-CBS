namespace Fictichos.Constructora.Repository;

public class TimeTrackerService
{
    public DateTime Now { get; set; } = DateTime.Now;
    public DateTime SixteenYearsAgo { get; private set; }
    public DateTime EighteenYearsAgo { get; private set; }
    public TimeTrackerService()
    {
        SixteenYearsAgo = Now.Subtract(TimeSpan.FromDays(5844));
        EighteenYearsAgo = Now.Subtract(TimeSpan.FromDays(6574));
    }

    public bool Over(int prop, DateTime value)
    {
        bool result = false;
        switch (prop)
        {
            case 0:
                if (DateTime.Compare(SixteenYearsAgo, value) == -1) result = true;
                break;
            case 1:
                if (DateTime.Compare(EighteenYearsAgo, value) == -1) result = true;
                break;
            default:
                throw new IndexOutOfRangeException();
        }
        return result;
    }
}