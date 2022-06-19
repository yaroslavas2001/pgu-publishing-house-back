using System.Text.RegularExpressions;

namespace PublishingHouse.Interfaces.Exstensions;

public static class FileExtensions
{
	public static bool TryGetFromBase64String(this string input, out byte[]? output)
	{
		output = null;
		try
		{
			output = Convert.FromBase64String(input);
			return true;
		}
		catch (FormatException ex)
		{
			return false;
		}
	}

	/// <summary>
	/// Gets whether the specified path is a valid absolute file path.
	/// </summary>
	/// <param name="path">Any path. OK if null or empty.</param>
	public static bool IsValidPath(this string path)
	{
		var r = new Regex(@"^(([a-zA-Z]:)|(\))(\{1}|((\{1})[^\]([^/:*?<>""|]*))+)$");
		return string.IsNullOrWhiteSpace(path) && r.IsMatch(path);
	}

	public static string ConvertServerPathToUri(this string path)
	{
		var uri = new Uri(path);
		return uri.AbsoluteUri;
	}

	public static string ConvertToServerPath(this string path, string baseDir)
	{
		return Path.Combine(baseDir, path);
	}
}