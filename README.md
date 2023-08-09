# CommandExec

A simple wrapper for `System.Diagnostics.Process` that makes it easy to run normal processes
and console commands in C#. (Yes, CliWrap exists, I just can't use it without messing up
for some reason.)

## Examples

```cs
using CommandExec;
// Initializing the Command class. Check the doc comments for more options.
Command dotnet = new Command("dotnet") { "--help" };
dotnet.Run(); // Runs "dotnet --help".
Task dotnetTask = dotnet.RunAsync(); // Runs asynchronously.
Console.WriteLine("Dotnet help running...");
await dotnetTask;

_ = Command.Shell(dotnet); // Runs the command in a shell. cmd.exe on Windows, bash on Linux/macOS.
(Task otherTask, Command shellCommand) = Command.ShellAsync(dotnet); // Runs asynchronously in a shell.
Console.WriteLine("Dotnet help running...");
await task;
```

Check more information about the `Command` class in the docs (no docs yet) or the doc comments.
