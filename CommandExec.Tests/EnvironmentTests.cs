namespace CommandExec.Tests;

public class EnvironmentTests
{
  [Fact(DisplayName = "Run shell command")]
  public void ShellCommandTest()
  {
    Command shell = Command.Shell("echo", "test");
    shell
      .RedirectStdOut()
      .RedirectStdErr()
      .Run();

    string STDOut = shell.stdOut.ReadToEnd().TrimEnd();
    Assert.Equal("test", STDOut);
    Assert.Equal(0, shell.exitCode);
    Assert.False(shell.hasError);
  }

  [Fact(DisplayName = "Check if command exists")]
  public void CommandExistsTest()
  {
    Assert.True(CommandUtils.Exists("type"));
    Assert.False(CommandUtils.Exists("fake-test-command-that-is-not-real"));
  }
}
