using System;
using System.IO;

namespace CommandExec
{
  public static class CommandUtils
  {
    public static bool CommandExists(string command)
    {
      Command cmd;
      if (Command.isUnix)
      {
        cmd = Command.Shell($"command -v {command} &> /dev/null && echo true").RedirectStdOut();
      }
      else
      {
        cmd = Command.Shell($"if (Get-Command -Name {command} -ErrorAction SilentlyContinue) {{echo true}}").RedirectStdOut();
      }

      cmd.Run();
      return cmd.STDOut.ReadToEnd().ReplaceLineEndings("") == "true";
    }
  }
}
