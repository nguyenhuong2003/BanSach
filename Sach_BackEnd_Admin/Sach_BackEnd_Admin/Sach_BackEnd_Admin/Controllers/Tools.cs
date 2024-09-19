namespace API.BanSach.Controllers
{
    public interface ITools
    {
        string CreatePathFile(string RelativePathFileName);
    }
    public class Tools : ITools
    {
        private IConfiguration _configuration;
        public Tools(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                Console.WriteLine(RelativePathFileName);

                string serverRootPathFolder = _configuration["AppSettings:WEB_SERVER_FULL_PATH"].ToString();
                Console.WriteLine(serverRootPathFolder);
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                Console.WriteLine(fullPathFile);
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
