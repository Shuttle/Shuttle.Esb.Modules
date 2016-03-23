namespace Shuttle.Esb.Modules
{
	public interface IActiveTimeRangeConfiguration
	{
		string ActiveFromTime { get; }
		string ActiveToTime { get; }

		ActiveTimeRange CreateActiveTimeRange();
	}
}