using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBot.config
{
    internal class JSONReader
    {
        public string token { get; set; }
        public string prefix { get; set; }
        
        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json")) // C#s version of Java Scanner
            {
                string json = await sr.ReadToEndAsync(); // reads the config.json file from start to finish
                JSONStructure readData = JsonConvert.DeserializeObject<JSONStructure>(json); // deserialising the json object and stores the extracted data as a variable
                this.token = readData.token;
                this.prefix = readData.prefix;
            }
        }
    }
    internal sealed class JSONStructure //sealed = disables class inheritance, this will store what the above class reads in and store it in a secured location
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}
