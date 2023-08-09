namespace CommandExec.Tests;

public class EnvironmentTests
{
  [Fact(DisplayName = "Run shell command")]
  public void ShellCommandTest()
  {
    Command command = new Command("\"echo test\"");
    command.RedirectStdOut().RedirectStdErr();
    Command shellCommand = Command.Shell(command);

    #region Assertions
    string STDOut = shellCommand.STDOut.ReadToEnd().TrimEnd();
    string STDErr = shellCommand.STDErr.ReadToEnd();

    Assert.Equal("test", STDOut);
    Assert.Equal(string.Empty, STDErr);
    #endregion
  }
}
