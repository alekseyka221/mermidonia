using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mermidonia.Models;

public class Init
{
    public async Task<IEnumerable<CommitItem>> Hydrate()
    {
        var workingDirectory = "/Users/alex/repositories/bitrix/modules";
        var result = new List<CommitItem>();
        using (var proc = new Process())
        {
            proc.StartInfo.FileName = "hg";
            proc.StartInfo.Arguments = "log -l 2 --template {author}\\n";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var readResult = (await proc.StandardOutput.ReadToEndAsync()).Trim('\n');
            
            var arrResult = readResult.Split('\n');
            foreach (var author in arrResult)
            {
                Console.WriteLine(author);
                result.Add(new CommitItem(author));
            }

            await proc.WaitForExitAsync();
        }

        return result;
    }
        
   
}

public class CommitItem
{
    public string Author { get; set; }

    public CommitItem(String author)
    {
        Author = author;
    }
}