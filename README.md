# CommandExec

A simple wrapper for `System.Diagnostics.Process` that makes it easy to run normal processes
and console commands in C#. (Yes, CliWrap exists, I just can't use it without messing up
for some reason.)

## Examples

```cs
using CommandExec;

// Initializing the Command class. Check the wiki (TBD) for more options.
Command dotnet = new Command("dotnet") { "build", "--help" };
dotnet.Run(); // Runs "dotnet build --help" synchronously.
Task dotnetTask = dotnet.RunAsync(); // Runs asynchronously.
Console.WriteLine("Dotnet help running...");
await dotnetTask;

Command shell = CommandUtils.Shell("echo", "Hello world!"); // Creates the shell command. PowerShell on Windows, /bin/sh on UNIX-like systems.
shell.Run(); // Passing args with `Run` for shell commands is not recommended.
```

Check more information about the `Command` class in the [Wiki (TBD)](https://github.com/perrylets/CommandExec/wiki) or the doc comments.
