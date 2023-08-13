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

Command shell = Command.Shell("dotnet", "--help"); // Creates the command for a shell. PowerShell on Windows, BASH on Linux/macOS.
shell.Run(); // Passing args with `Run` for shell command is not recommended.
```

Check more information about the `Command` class in the [Wiki (empty for now)](https://github.com/perrylets/CommandExec/wiki) or the doc comments.
