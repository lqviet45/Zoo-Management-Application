using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServiceContracts.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GenderOptions
	{
		Male, Female, Other
	}
}
