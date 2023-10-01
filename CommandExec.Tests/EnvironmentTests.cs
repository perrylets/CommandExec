using System.Runtime.InteropServices;

namespace CommandExec.Tests;

public class EnvironmentTests
{
  [Fact(DisplayName = "Run shell command")]
  public void ShellCommandTest()
  {
    Command shell = CommandUtils.Shell("echo", "test");
    shell
      .RedirectStdOut()
      .RedirectStdErr()
      .Run();

    Assert.Equal("test", shell.stdOut.ReadToEnd().TrimEnd());
    Assert.Equal(0, shell.exitCode);
    Assert.False(shell.hasError);
  }

  [Fact(DisplayName = "Check if command exists")]
  public void CommandExistsTest()
  {
    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      Command cmd = CommandUtils.Shell("file", "/bin/sh").RedirectStdOut();
      cmd.Run();
      Assert.Equal("/bin/sh: symbolic link to", cmd.stdOut.ReadToEnd()[..25]);
    }
    Assert.True(CommandUtils.Exists("type"));
    Assert.False(CommandUtils.Exists("fake-test-command-that-is-not-real"));
  }
}
