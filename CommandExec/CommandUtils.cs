using System;
using System.IO;

namespace CommandExec
{
  public static class CommandUtils
  {
    public static bool CommandExists(string command)
    {
      if (File.Exists(command))
        return true;

      string values = Environment.GetEnvironmentVariable("PATH") ?? throw new Exception("PATH variable not set");
      foreach (var path in values.Split(Path.PathSeparator))
      {
        string fullPath = Path.Combine(path, command);
        if (File.Exists(fullPath))
          return true;
      }

      return false;
    }
  }
}
