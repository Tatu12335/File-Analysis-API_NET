namespace Toolkit_API.Application.Application_Services.FileOperations
{
    public class HandleFolder
    {

        public HandleFolder() { }

        public async Task Handler(string path)
        {
            if (!Directory.Exists(path))
                return;
            Stack<string> directories = new Stack<string>();
            
            foreach (var file in Directory.EnumerateFiles(path))
            {

            }
        }
    }
}
