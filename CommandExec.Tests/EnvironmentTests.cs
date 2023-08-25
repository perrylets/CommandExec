namespace CommandExec.Tests;

public class EnvironmentTests
{
  [Fact(DisplayName = "Run shell command")]
  public void ShellCommandTest()
  {
    Command shell = Command.Shell("echo test");
    shell
      .RedirectStdOut()
      .RedirectStdErr()
      .Run();

    string STDOut = shell.STDOut.ReadToEnd().TrimEnd();
    Assert.Equal("test", STDOut);

    //! There can be errors with the shell itself, not the process.
    // string STDErr = shell.STDErr.ReadToEnd().TrimEnd();
    // Assert.Equal(string.Empty, STDErr);
  }

  [Fact(DisplayName = "Check if command exists")]
  public void CommandExistsTest()
  {
    Assert.True(CommandUtils.CommandExists("type"));
    Assert.False(CommandUtils.CommandExists("fake-test-command-that-is-not-real"));
  }
}
