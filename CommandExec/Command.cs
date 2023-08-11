using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CommandExec
{
  /// <summary>
  ///  Simple wrapper for the <see cref="Process"/> class.
  /// </summary>
  public class Command : IEnumerable
  {
    #region Fields
    string args;
    internal readonly Process process;
    static readonly bool isUnix = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    #endregion

    #region Standard Streams
    /// <summary>
    /// Gets the <see cref="StreamReader"/> for the Standard Output stream.
    /// </summary>
    public StreamReader STDOut => process.StandardOutput;
    /// <summary>
    /// Gets the <see cref="StreamWriter"/> for the Standard Input stream.
    /// </summary>
    public StreamWriter STDIn => process.StandardInput;
    /// <summary>
    /// Gets the <see cref="StreamReader"/> for the Standard Error stream.
    /// </summary>
    public StreamReader STDErr => process.StandardError;
    #endregion

    #region Functions
    /// <summary>
    /// Creates a new command instance.
    /// </summary>
    /// <param name="command">The file to run.</param>
    /// <param name="cwd">The working directory of the process. If null, defaults to the current working directory.</param>
    /// <param name="redirectSTDOut">Whether the textual output of an application is written to <see cref="STDOut"/>.</param>
    /// <param name="redirectSTDIn">Wether the input for an application is read from <see cref="STDIn"/>.</param>
    /// <param name="redirectSTDErr">Whether the error output of an application is written to <see cref="STDErr"/>.</param>
    /// <param name="args">The process arguments.</param>
    public Command(string command, string? cwd = null, bool redirectSTDOut = false, bool redirectSTDIn = false, bool redirectSTDErr = false, params string[] args)
    {
      process = new Process();
      process.StartInfo.FileName = command;

      if (cwd is not null)
        process.StartInfo.WorkingDirectory = cwd;
      else
        process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.RedirectStandardOutput = redirectSTDOut;
      process.StartInfo.RedirectStandardInput = redirectSTDIn;
      process.StartInfo.RedirectStandardError = redirectSTDErr;
      this.args = string.Join(" ", args);
    }

    /// <summary>
    /// Runs the process.
    /// </summary>
    /// <param name="args">Additional arguments to run the process.</param>
    public void Run(params string[] args)
    {
      string roughArgs = $"{string.Join(" ", this.args)} {string.Join(" ", args)}";
      process.StartInfo.Arguments = roughArgs.Trim();
      process.Start();
      process.WaitForExit();
    }

    /// <summary>
    /// Runs the process asynchronously.
    /// </summary>
    /// <param name="args">Additional arguments to run the process.</param>
    /// <returns>A task that finishes when the process exits.</returns>
    public Task RunAsync(params string[] args)
    {
      string roughArgs = $"{string.Join(" ", this.args)} {string.Join(" ", args)}";
      process.StartInfo.Arguments = roughArgs.Trim();
      process.Start();
      return process.WaitForExitAsync();
    }

    /// <summary>
    /// Runs a command inside a shell. CMD on WIndows and Bash everywhere else.
    /// </summary>
    /// <param name="command">The command to run in the shell.</param>
    /// <param name="args">Additional arguments passed to the shell command.</param>
    /// <returns>The command used to run the shell.</returns>
    public static Command Shell(params string[] args)
    {
      (string shellCommand, string shellArg) = isUnix ? ("bash", "-c") : ("cmd.exe", "/C");
      Command shell = new Command(shellCommand)
        .AddArg(shellArg);

      if (isUnix)
      {
        return shell.AddArg($"\"{string.Join(" ", args).Replace("\"", "\\\"")}");
      }
      return shell.AddArg(args);

    }

    /// <summary>
    /// Adds a argument to the command.
    /// </summary>
    /// <param name="str">The argument to add.</param>
    /// <returns>The command instance (for chaining).</returns>
    /// <remarks>
    /// <see cref="Add"/> is an alias for <see cref="AddArg"/>.
    /// </remarks>
    public Command AddArg(params string[] args)
    {
      string roughArgs = this.args + $" {string.Join(" ", args)}";
      this.args = roughArgs.Trim();
      return this;
    }

    /// <summary>
    /// Sets the working directory of the process.
    /// </summary>
    /// <param name="dir">The working directory of the process.</param>
    /// <returns>The command instance (for chaining).</returns>
    public Command CWD(string dir)
    {
      process.StartInfo.WorkingDirectory = dir;
      return this;
    }

    /// <summary>
    /// Sets whether the textual output of an application is written to <see cref="STDOut"/>. 
    /// </summary>
    /// <param name="redirect">Whether the textual output of an application is written to <see cref="STDOut"/>.</param>
    /// <returns>The command instance (for chaining).</returns>
    public Command RedirectStdOut(bool redirect = true)
    {
      process.StartInfo.RedirectStandardOutput = redirect;
      return this;
    }

    /// <summary>
    /// Sets whether the input for an application is read from <see cref="STDIn"/>.
    /// </summary>
    /// <param name="redirect">Whether the input for an application is read from <see cref="STDIn"/>.</param>
    /// <returns>The command instance (for chaining).</returns>
    public Command RedirectStdIn(bool redirect = true)
    {
      process.StartInfo.RedirectStandardInput = redirect;
      return this;
    }

    /// <summary>
    /// Sets whether the error output of an application is written to <see cref="STDErr"/>. 
    /// </summary>
    /// <param name="redirect">Whether the error output of an application is written to <see cref="STDErr"/>.</param>
    /// <returns>The command instance (for chaining).</returns>
    public Command RedirectStdErr(bool redirect = true)
    {
      process.StartInfo.RedirectStandardError = redirect;
      return this;
    }
    #endregion

    #region Required Functions
    /// <summary>
    /// Adds a argument to the command.
    /// </summary>
    /// <param name="str">The argument to add.</param>
    /// <remarks>
    /// <see cref="Add"/> is an alias for <see cref="AddArg"/>.
    /// </remarks>
    public Command Add(params string[] str)
    {
      return AddArg(str);
    }

    /// <summary>
    /// Enumerates the arguments of the command.
    /// </summary>
    /// <returns>The arguments for the process.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (process.StartInfo.FileName + " " + args).Split(" ").GetEnumerator();
    }
    #endregion

  }
}
