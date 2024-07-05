using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.config
{
    internal class JSONReader
    {
        public string token { get; set; }
        public string prefix { get; set; }
        
        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {

            }
        }
    }
    internal sealed class JSONSTructure //sealed = disables class inheritance, this will store what the above class reads in and store it in a secured location
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}
