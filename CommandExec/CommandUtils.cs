namespace CommandExec
{
  public static class CommandUtils
  {
    /// <summary>
    /// Runs a command inside a shell. CMD on WIndows and Bash everywhere else.
    /// </summary>
    /// <param name="command">The command to run in the shell.</param>
    /// <param name="args">Additional arguments passed to the shell command.</param>
    /// <returns>The command used to run the shell.</returns>
    public static Command Shell(params string[] args)
    {
      (string shellCommand, string shellArg) = Command.isUnix ?
      ("/bin/sh", "-c") :
      ("powershell", "-Command");

      Command shell = new Command(shellCommand).AddArg(shellArg);
      return shell.AddArg($"\"{string.Join(" ", args).Replace("\"", "\\\"")}");
    }
    /// <summary>
    /// Checks if a command exists.
    /// </summary>
    /// <remarks>
    /// This method will check for both shell commands and executable files.
    /// There's a chance that the command exists, but needs to be executed through the shell.
    /// </remarks>
    /// <param name="command">The command to check</param>
    /// <returns>Whether the command exists or not.</returns>
    public static bool Exists(string command)
    {
      Command cmd;

      if (Command.isUnix)
      {
        cmd = Shell($"command", "-v", command).RedirectStdOut().RedirectStdErr()
        .RedirectStdOut().RedirectStdErr();
        cmd.Run();
        return !cmd.hasError;
      }

      cmd = Shell("Get-Command", "-Name", command, "-ErrorAction", "Stop")
        .RedirectStdOut().RedirectStdErr();
      cmd.Run();
      return !cmd.hasError;
    }
  }
}
