# CommandExec

A simple wrapper for `System.Diagnostics.Process` that makes it easy to run normal processes
and console commands in C#. (Yes, CliWrap exists, I just can't use it without messing up
for some reason.)

## Examples

```cs
using CommandExec;
// Initializing the Command class. Check the wiki comments for more options.
Command dotnet = new Command("dotnet") { "--help" };
dotnet.Run(); // Runs "dotnet --help".
Task dotnetTask = dotnet.RunAsync(); // Runs asynchronously.
Console.WriteLine("Dotnet help running...");
await dotnetTask;

Command = Command.Shell("dotnet", "--help"); // Runs the command in a shell. cmd.exe on Windows, bash on Linux/macOS.
```

Check more information about the `Command` class in the [Wiki (empty for now)](https://github.com/perrylets/CommandExec/wiki) or the doc comments.
