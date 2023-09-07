namespace CommandExec
{
  public static class CommandUtils
  {
    public static bool CommandExists(string command)
    {
      Command cmd;
      if (Command.isUnix)
      {
        cmd = Command.Shell($"command -v {command} &> /dev/null").RedirectStdOut()
          .RedirectStdOut()
          .RedirectStdErr()
          .RedirectStdIn();
      }
      else
      {
        cmd = Command.Shell($"Get-Command -Name {command} -ErrorAction SilentlyContinue > $null")
          .RedirectStdOut()
          .RedirectStdErr()
          .RedirectStdIn();
      }

      cmd.Run();
      return cmd.process.ExitCode == 0;
    }
  }
}
