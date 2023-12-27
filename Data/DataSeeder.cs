using Newtonsoft.Json;
using sacremap.Data;
using sacremap_web_api.Models;

public class DataSeeder
{
    public static async Task SeedData(DataContext context)
    {

        if (!context.Churches.Any())
        {
            string churchesJson = System.IO.File.ReadAllText(@"Data/churches_coded.json");

            List<Church> churches = JsonConvert.DeserializeObject<List<Church>>(churchesJson);
            await context.Churches.AddRangeAsync(churches);
            await context.SaveChangesAsync();
        }

    }
}