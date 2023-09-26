namespace CommandExec
{
  public static class CommandUtils
  {
    public static bool Exists(string command)
    {
      Command cmd;
#if !WINDOWS
      cmd = Command.Shell($"command -v {command}").RedirectStdOut()
#else
      cmd = Command.Shell($"Get-Command -Name {command} -ErrorAction SilentlyContinue > $null")
#endif
      .RedirectStdOut().RedirectStdErr().RedirectStdIn();

      cmd.Run();
      return !cmd.hasError;
    }
  }
}
