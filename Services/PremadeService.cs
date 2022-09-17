using System;
using System.Net.Http.Json;



namespace FireSignage.Services

{
	public class PremadeService
	{
		HttpClient httpClient;			

		public PremadeService()
		{
            

        }

		List<PreMadeSigns> premadeList = new();

		public async Task<List<PreMadeSigns>> GetPreMadeSigns()
        {
			if (premadeList?.Count > 0)
				return premadeList;

			using var stream = await FileSystem.OpenAppPackageFileAsync("premadedata.json");
			using var reader = new StreamReader(stream);
			var contents = await reader.ReadToEndAsync();
			premadeList = JsonSerializer.Deserialize<List<PreMadeSigns>>(contents);

			return premadeList;

        }
	}
}

