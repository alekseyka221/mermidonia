using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using DynamicData.Binding;
using ReactiveUI;

namespace Mermidonia.Views
{
    public partial class MainWindow : Window, IActivatableView
    {
        public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {

        var workingDirectory = "/Users/alex/repositories/bitrix/modules";

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
            var i = 0;
            foreach (var author in arrResult)
            {
                Console.WriteLine($"author {i}: {author}");
               i++;
            }

            await proc.WaitForExitAsync();
        }
    }

   
    }
    
    
}