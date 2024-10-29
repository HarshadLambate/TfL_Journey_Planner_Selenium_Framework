using Newtonsoft.Json;
using STA_Coding_Challenge.Config;

namespace STA_Coding_Challenge.Utilities
{
    // Helper class for reading configuration files and obtaining environment-specific data.
    public class TestHelper
    {      
        // Reads the contents of a JSON file asynchronously.
        public async Task<string> ReadJsonFileAsync(string fileName)
        {//asynchronous file reading to improve responsiveness.
            try
            {
                string filePath = Path.Combine(TestHelper.GetProjectPath(), fileName);
                using (StreamReader reader = new StreamReader(filePath))
                {//Use "using" statements for StreamReader and JsonReader to ensure resources are properly disposed.
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error reading JSON file: {ex.Message}");
            }
        }
        // Retrieves environment data based on the environment specified in AppConfig.
        public async Task<EnvironmentData> GetEnvironmentDataAsync()
        {
            try
            {
                // Read and parse AppConfig.json to determine the environment on which script will be executed.
                string appData = await ReadJsonFileAsync(@"Config\AppConfig.json");
                var jsonObj = JsonConvert.DeserializeObject<AppConfig>(appData);
                string environmentName = jsonObj?.Environment;

                // Read and parse EnvironmentConfig.json to retrieve details of target environment specified in AppConfig.json
                string envData = await ReadJsonFileAsync(@"Config\EnvironmentConfig.json");
                var root = JsonConvert.DeserializeObject<EnvironmentConfig>(envData);
                // Find and return the specified environment data.
                EnvironmentData targetEnvironment = root?.Environments?.Find(env => env.Name.Equals(environmentName, StringComparison.OrdinalIgnoreCase));
                return targetEnvironment;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error getting environment data: {ex.Message}");
            }
        }

        // Retrieves the application's configuration settings from AppConfig.json.
        public static AppConfig GetAppConfig()
        {
            string projectPath = GetProjectPath();
            
            string fileName = @"Config\AppConfig.json";
            string filePath = Path.Combine(projectPath, fileName);
            // Deserialize and return AppConfig settings
            return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(filePath));
        }

        // Gets the project base path.
        public static string GetProjectPath()
        {
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            // Rectify path if running in .NET 6.0
            if (assemblyPath.Contains("net6.0"))
            {
                assemblyPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\"));
            }
            // Remove bin\Debug\ folder from path if present
            return assemblyPath.Replace(@"bin\Debug\", "");
        }
    }
}
