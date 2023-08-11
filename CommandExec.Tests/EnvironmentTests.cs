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

    #region Assertions
    string STDOut = shell.STDOut.ReadToEnd().TrimEnd();
    string STDErr = shell.STDErr.ReadToEnd().TrimEnd();

    Assert.Equal("test", STDOut);
    Assert.Equal(string.Empty, STDErr);
    #endregion
  }
}
