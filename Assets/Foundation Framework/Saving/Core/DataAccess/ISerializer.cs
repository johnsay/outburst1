namespace FoundationFramework
{
	public interface ISerializer
	{
		string Serialize<T>(T data) where T : class;
		T Unserialize<T>(string serializedData) where T : class;
	}
}