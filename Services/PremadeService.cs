using System;



namespace FireSignage.Services

{
	public class PremadeService
	{
				

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

